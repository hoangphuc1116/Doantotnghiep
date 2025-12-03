using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_WatchShop.Models
{
    public partial class FooterLink
    {
        [Key]
        public int Ma { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? TieuDe { get; set; }

        public string? Url { get; set; }

        public int MaNhom { get; set; } // 1: Thông tin, 2: Tài khoản, 3: Danh mục

        public int ThuTuHienThi { get; set; }

        public bool TrangThai { get; set; }
    }
}
