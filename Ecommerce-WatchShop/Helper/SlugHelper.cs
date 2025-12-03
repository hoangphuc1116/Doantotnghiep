using Ecommerce_WatchShop.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Ecommerce_WatchShop.Helper
{
    public class SlugHelper
    {
        private static readonly Dictionary<char, char> CharacterMap = new()
        {
            // Lowercase
            {'à', 'a'}, {'á', 'a'}, {'ạ', 'a'}, {'ả', 'a'}, {'ã', 'a'},
            {'â', 'a'}, {'ấ', 'a'}, {'ầ', 'a'}, {'ậ', 'a'}, {'ẩ', 'a'}, {'ẫ', 'a'},
            {'ă', 'a'}, {'ắ', 'a'}, {'ằ', 'a'}, {'ặ', 'a'}, {'ẳ', 'a'}, {'ẵ', 'a'},
            {'è', 'e'}, {'é', 'e'}, {'ẹ', 'e'}, {'ẻ', 'e'}, {'ẽ', 'e'},
            {'ê', 'e'}, {'ế', 'e'}, {'ề', 'e'}, {'ệ', 'e'}, {'ể', 'e'}, {'ễ', 'e'},
            {'ì', 'i'}, {'í', 'i'}, {'ị', 'i'}, {'ỉ', 'i'}, {'ĩ', 'i'},
            {'ò', 'o'}, {'ó', 'o'}, {'ọ', 'o'}, {'ỏ', 'o'}, {'õ', 'o'},
            {'ô', 'o'}, {'ố', 'o'}, {'ồ', 'o'}, {'ộ', 'o'}, {'ổ', 'o'}, {'ỗ', 'o'},
            {'ơ', 'o'}, {'ớ', 'o'}, {'ờ', 'o'}, {'ợ', 'o'}, {'ở', 'o'}, {'ỡ', 'o'},
            {'ù', 'u'}, {'ú', 'u'}, {'ụ', 'u'}, {'ủ', 'u'}, {'ũ', 'u'},
            {'ư', 'u'}, {'ứ', 'u'}, {'ừ', 'u'}, {'ự', 'u'}, {'ử', 'u'}, {'ữ', 'u'},
            {'ỳ', 'y'}, {'ý', 'y'}, {'ỵ', 'y'}, {'ỷ', 'y'}, {'ỹ', 'y'},
            {'đ', 'd'},
            
            // Uppercase
            {'À', 'A'}, {'Á', 'A'}, {'Ạ', 'A'}, {'Ả', 'A'}, {'Ã', 'A'},
            {'Â', 'A'}, {'Ấ', 'A'}, {'Ầ', 'A'}, {'Ậ', 'A'}, {'Ẩ', 'A'}, {'Ẫ', 'A'},
            {'Ă', 'A'}, {'Ắ', 'A'}, {'Ằ', 'A'}, {'Ặ', 'A'}, {'Ẳ', 'A'}, {'Ẵ', 'A'},
            {'È', 'E'}, {'É', 'E'}, {'Ẹ', 'E'}, {'Ẻ', 'E'}, {'Ẽ', 'E'},
            {'Ê', 'E'}, {'Ế', 'E'}, {'Ề', 'E'}, {'Ệ', 'E'}, {'Ể', 'E'}, {'Ễ', 'E'},
            {'Ì', 'I'}, {'Í', 'I'}, {'Ị', 'I'}, {'Ỉ', 'I'}, {'Ĩ', 'I'},
            {'Ò', 'O'}, {'Ó', 'O'}, {'Ọ', 'O'}, {'Ỏ', 'O'}, {'Õ', 'O'},
            {'Ô', 'O'}, {'Ố', 'O'}, {'Ồ', 'O'}, {'Ộ', 'O'}, {'Ổ', 'O'}, {'Ỗ', 'O'},
            {'Ơ', 'O'}, {'Ớ', 'O'}, {'Ờ', 'O'}, {'Ợ', 'O'}, {'Ở', 'O'}, {'Ỡ', 'O'},
            {'Ù', 'U'}, {'Ú', 'U'}, {'Ụ', 'U'}, {'Ủ', 'U'}, {'Ũ', 'U'},
            {'Ư', 'U'}, {'Ứ', 'U'}, {'Ừ', 'U'}, {'Ự', 'U'}, {'Ử', 'U'}, {'Ữ', 'U'},
            {'Ỳ', 'Y'}, {'Ý', 'Y'}, {'Ỵ', 'Y'}, {'Ỷ', 'Y'}, {'Ỹ', 'Y'},
            {'Đ', 'D'}
        };

        public static string GenerateSlug(string phrase)
        {
            if (string.IsNullOrEmpty(phrase))
                return string.Empty;

            // Convert Vietnamese characters to Latin
            string str = ConvertToLatinChars(phrase);

            // Convert to lowercase
            str = str.ToLower();

            // Remove invalid chars
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");

            // Remove multiple spaces
            str = Regex.Replace(str, @"\s+", " ").Trim();

            // Cut and trim
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();

            // Replace spaces with hyphens
            str = Regex.Replace(str, @"\s", "-");

            return str;
        }

        private static string ConvertToLatinChars(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;

            var characterMap = new Dictionary<char, char>(CharacterMap);
            char[] array = text.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (characterMap.ContainsKey(array[i]))
                {
                    array[i] = characterMap[array[i]];
                }
            }
            return new string(array);
        }

        public enum EntityType
        {
            Product,
            Category,
            Brand
        }

        public static async Task<string> GenerateUniqueSlug(DongHoContext context, string name, EntityType entityType, int? entityId = null)
        {
            if (string.IsNullOrEmpty(name))
                return string.Empty;

            // Tạo slug từ tên
            string slug = GenerateSlug(name);
            string originalSlug = slug;
            int counter = 1;

            // Kiểm tra trùng lặp dựa trên entityType
            while (true)
            {
                bool exists = false;
                switch (entityType)
                {
                    case EntityType.Product:
                        exists = await context.SanPhams
                            .AsNoTracking()
                            .AnyAsync(p => p.Slug == slug && (entityId == null || p.MaSanPham != entityId));
                        break;
                    case EntityType.Category:
                        exists = await context.DanhMucs
                            .AsNoTracking()
                            .AnyAsync(c => c.Slug == slug && (entityId == null || c.MaDanhMuc != entityId));
                        break;
                    case EntityType.Brand:
                        exists = await context.ThuongHieus
                            .AsNoTracking()
                            .AnyAsync(b => b.Slug == slug && (entityId == null || b.MaThuongHieu != entityId));
                        break;
                }

                if (!exists) break;
                slug = $"{originalSlug}-{counter++}";
            }

            return slug;
        }
    }
}