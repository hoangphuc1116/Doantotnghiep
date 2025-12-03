using Ecommerce_WatchShop.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_WatchShop
{
    public class SeedData
    {
        public static async Task SeedingData(DongHoContext _context)
        {
            await _context.Database.MigrateAsync();
            if (!_context.ThuongHieus.Any())
            {
                ThuongHieu dell = new ThuongHieu { TenThuongHieu = "Dell", Slug = "dell" };
                ThuongHieu hp = new ThuongHieu { TenThuongHieu = "HP", Slug = "hp" };
                ThuongHieu lenovo = new ThuongHieu { TenThuongHieu = "Lenovo", Slug = "lenovo" };
                ThuongHieu asus = new ThuongHieu { TenThuongHieu = "Asus",  Slug = "asus" };

                await _context.ThuongHieus.AddRangeAsync(dell, hp, lenovo, asus);
                await _context.SaveChangesAsync();

            }
            if (!_context.DanhMucs.Any())
            {

                DanhMuc laptopGaming = new DanhMuc { TenDanhMuc = "Laptop Gaming", MaDanhMucCha = null, Slug = "laptop-gaming" };
                DanhMuc laptopVanPhong = new DanhMuc { TenDanhMuc = "Laptop VÄƒn PhÃ²ng", MaDanhMucCha = null, Slug = "laptop-van-phong" };
                DanhMuc laptopDoHoa = new DanhMuc { TenDanhMuc = "Laptop Äá»“ Há»a", MaDanhMucCha = null, Slug = "laptop-do-hoa" };

                await _context.DanhMucs.AddRangeAsync(laptopGaming, laptopVanPhong, laptopDoHoa);
                await _context.SaveChangesAsync();
            }
            //if (!_context.Suppliers.Any())
            //{
            //    Supplier citizen_supplier = new Supplier { Name = "CÃ´ng ty Citizen Watch", SoDienThoai = "(800) 321-1023", Information = "CÃ”NG TY CITIZEN WATCH lÃ  má»™t nhÃ  sáº£n xuáº¥t thá»±c sá»± vá»›i má»™t quy trÃ¬nh sáº£n xuáº¥t toÃ n diá»‡n", DiaChi = "6-1-12, Tanashi-cho, Nishi-Tokyo-shi, Tokyo 188-8511, Japan" };
            //    Supplier doxa_supplier = new Supplier { Name = "CÃ´ng ty Doxa", SoDienThoai = "1-520-369 -872", Information = "ThÆ°Æ¡ng hiá»‡u Ä‘á»“ng há»“ Doxa ná»•i tiáº¿ng cá»§a Thuá»µ SÄ© Ä‘Æ°á»£c ra máº¯t vá»›i cÃ´ng chÃºng vÃ o nÄƒm 1889 bá»Ÿi má»™t nghá»‡ nhÃ¢n cháº¿ tÃ¡c Ä‘á»“ng há»“ tráº» tuá»•i", DiaChi = "Rue de Zurich 23A, 2500 Biel/Bienne, Switzerland" };
            //    Supplier curnon_supplier = new Supplier { Name = "CÃ´ng ty Curnon", SoDienThoai = "0868889103", Information = "Vá»›i nhá»¯ng sáº£n pháº©m Ä‘Æ°á»£c thiáº¿t káº¿ báº±ng nhiá»‡t huyáº¿t, khÃ¡t khao vÃ  khá»‘i Ã³c Ä‘áº§y sÃ¡ng táº¡o cá»§a Ä‘á»™i ngÅ© chÃ­nh nhá»¯ng ngÆ°á»i tráº» Viá»‡t Nam.", DiaChi = "25 Nguyá»…n TrÃ£i, P.Báº¿n ThÃ nh, Quáº­n 1." };
            //    Supplier seiko_supplier = new Supplier { Name = "CÃ´ng ty Seiko", SoDienThoai = "81-3-3563-2111", Information = "CÃ´ng ty Nháº­t Báº£n thÃ nh láº­p vÃ o nÄƒm 1881; ná»•i tiáº¿ng trong lÄ©nh vá»±c sáº£n xuáº¥t vÃ  mua bÃ¡n Ä‘á»“ng há»“, thiáº¿t bá»‹ Ä‘iá»‡n tá»­", DiaChi = "1-8 Nakase, Mihama-ku, Chiba-shi, Chiba 261-8507, Japan" };

            //    await _context.Suppliers.AddRangeAsync(citizen_supplier, doxa_supplier, curnon_supplier, seiko_supplier);
            //    await _context.SaveChangesAsync();
            //}
            if (!_context.SanPhams.Any())
            {
                var laptopGaming = _context.DanhMucs.FirstOrDefault(c => c.TenDanhMuc == "Laptop Gaming");
                var laptopVanPhong = _context.DanhMucs.FirstOrDefault(c => c.TenDanhMuc == "Laptop VÄƒn PhÃ²ng");
                var laptopDoHoa = _context.DanhMucs.FirstOrDefault(c => c.TenDanhMuc == "Laptop Äá»“ Há»a");

                var dell = _context.ThuongHieus.FirstOrDefault(b => b.TenThuongHieu == "Dell");
                var hp = _context.ThuongHieus.FirstOrDefault(b => b.TenThuongHieu == "HP");
                var lenovo = _context.ThuongHieus.FirstOrDefault(b => b.TenThuongHieu == "Lenovo");
                var asus = _context.ThuongHieus.FirstOrDefault(b => b.TenThuongHieu == "Asus");

                //var citizen_supplier = _context.Suppliers.FirstOrDefault(s => s.Name == "CÃ´ng ty Citizen Watch");
                //var doxa_supplier = _context.Suppliers.FirstOrDefault(s => s.Name == "CÃ´ng ty Doxa");
                //var curnon_supplier = _context.Suppliers.FirstOrDefault(s => s.Name == "CÃ´ng ty Curnon");
                //var seiko_supplier = _context.Suppliers.FirstOrDefault(s => s.Name == "CÃ´ng ty Seiko");

                await _context.SanPhams.AddRangeAsync(
                    new SanPham
                    {
                        HinhAnh = "Lenovo-Legion-5.png",
                        TenSanPham = "Lenovo Legion 5",
                        MaDanhMuc = laptopGaming.MaDanhMuc,
                        MaThuongHieu = lenovo.MaThuongHieu,
                        GioiTinh = "Unisex",
                        Gia = 25990000,
                        MoTaNgan = "Laptop gaming máº¡nh máº½ vá»›i hiá»‡u nÄƒng vÆ°á»£t trá»™i",
                        MoTa = "Lenovo Legion 5 lÃ  laptop gaming cao cáº¥p vá»›i thiáº¿t káº¿ hiá»‡n Ä‘áº¡i, trang bá»‹ chip AMD Ryzen 7, card Ä‘á»“ há»a RTX 3060, mÃ n hÃ¬nh 15.6 inch 165Hz mang Ä‘áº¿n tráº£i nghiá»‡m gaming mÆ°á»£t mÃ .",
                        ThongSoKyThuat = "CPU: AMD Ryzen 7 5800H<br>RAM: 16GB DDR4<br>á»” cá»©ng: 512GB SSD NVMe<br>VGA: NVIDIA RTX 3060 6GB<br>MÃ n hÃ¬nh: 15.6 inch FHD 165Hz<br>Pin: 80Wh<br>Trá»ng lÆ°á»£ng: 2.4kg",
                        SoLuong = 10,
                        TrangThai = 1,
                        LuotXem = 1000,
                        NgayTao = DateTime.Now,
                        NgayCapNhat = null,
                        DaXoa = 0,
                        Slug = "lenovo-legion-5"
                    },
                    new SanPham
                    {
                        HinhAnh = "Dell-Inspiron-15.png",
                        TenSanPham = "Dell Inspiron 15 3520",
                        MaDanhMuc = laptopVanPhong.MaDanhMuc,
                        MaThuongHieu = dell.MaThuongHieu,
                        GioiTinh = "Unisex",
                        Gia = 15990000,
                        MoTaNgan = "Dell Inspiron 15 3520 â€“ Laptop vÄƒn phÃ²ng hiá»‡u suáº¥t cao, thiáº¿t káº¿ thanh lá»‹ch",
                        MoTa = "Dell Inspiron 15 3520 lÃ  laptop vÄƒn phÃ²ng lÃ½ tÆ°á»Ÿng vá»›i thiáº¿t káº¿ má»ng nháº¹, hiá»‡u nÄƒng á»•n Ä‘á»‹nh tá»« Intel Core i5 tháº¿ há»‡ 12, phÃ¹ há»£p cho cÃ´ng viá»‡c vÄƒn phÃ²ng vÃ  há»c táº­p.",
                        ThongSoKyThuat = "CPU: Intel Core i5-1235U<br>RAM: 8GB DDR4<br>á»” cá»©ng: 256GB SSD<br>VGA: Intel UHD Graphics<br>MÃ n hÃ¬nh: 15.6 inch FHD<br>Pin: 41Wh<br>Trá»ng lÆ°á»£ng: 1.85kg",
                        SoLuong = 12,
                        TrangThai = 1,
                        LuotXem = 50,
                        NgayTao = DateTime.Now,
                        NgayCapNhat = null,
                        DaXoa = 0,
                        Slug = "dell-inspiron-15-3520"
                    },
                    new SanPham
                    {
                        HinhAnh = "Asus-ROG-Strix-G15.png",
                        TenSanPham = "Asus ROG Strix G15",
                        MaDanhMuc = laptopGaming.MaDanhMuc,
                        MaThuongHieu = asus.MaThuongHieu,
                        GioiTinh = "Unisex",
                        Gia = 32990000,
                        MoTaNgan = "Asus ROG Strix G15 â€“ Laptop gaming cao cáº¥p vá»›i hiá»‡u nÄƒng Ä‘á»‰nh cao",
                        MoTa = "Asus ROG Strix G15 lÃ  laptop gaming cao cáº¥p vá»›i thiáº¿t káº¿ ROG Ä‘áº·c trÆ°ng, trang bá»‹ Intel Core i7 tháº¿ há»‡ 12, RTX 3070 Ti, mÃ n hÃ¬nh 300Hz mang Ä‘áº¿n tráº£i nghiá»‡m gaming Ä‘á»‰nh cao.",
                        ThongSoKyThuat = "CPU: Intel Core i7-12700H<br>RAM: 16GB DDR5<br>á»” cá»©ng: 1TB SSD NVMe<br>VGA: NVIDIA RTX 3070 Ti 8GB<br>MÃ n hÃ¬nh: 15.6 inch FHD 300Hz<br>Pin: 90Wh<br>Trá»ng lÆ°á»£ng: 2.3kg",
                        SoLuong = 6,
                        TrangThai = 1,
                        LuotXem = 100,
                        NgayTao = DateTime.Now,
                        NgayCapNhat = null,
                        DaXoa = 0,
                        Slug = "asus-rog-strix-g15"
                    },
                    new SanPham
                    {
                        HinhAnh = "HP-Pavilion-15.png",
                        TenSanPham = "HP Pavilion 15",
                        MaDanhMuc = laptopVanPhong.MaDanhMuc,
                        MaThuongHieu = hp.MaThuongHieu,
                        GioiTinh = "Unisex",
                        Gia = 18990000,
                        MoTaNgan = "HP Pavilion 15 â€“ Laptop vÄƒn phÃ²ng Ä‘a nÄƒng, thiáº¿t káº¿ sang trá»ng",
                        MoTa = "HP Pavilion 15 lÃ  laptop vÄƒn phÃ²ng Ä‘a nÄƒng vá»›i thiáº¿t káº¿ sang trá»ng, hiá»‡u nÄƒng á»•n Ä‘á»‹nh tá»« Intel Core i5, phÃ¹ há»£p cho cÃ´ng viá»‡c vÄƒn phÃ²ng, giáº£i trÃ­ vÃ  há»c táº­p.",
                        ThongSoKyThuat = "CPU: Intel Core i5-1235U<br>RAM: 8GB DDR4<br>á»” cá»©ng: 512GB SSD<br>VGA: Intel Iris Xe Graphics<br>MÃ n hÃ¬nh: 15.6 inch FHD<br>Pin: 41Wh<br>Trá»ng lÆ°á»£ng: 1.75kg",
                        SoLuong = 8,
                        TrangThai = 1,
                        LuotXem = 2000,
                        NgayTao = DateTime.Now,
                        NgayCapNhat = null,
                        DaXoa = 0,
                        Slug = "hp-pavilion-15"
                    },
                    new SanPham
                    {
                        HinhAnh = "Dell-XPS-15.png",
                        TenSanPham = "Dell XPS 15",
                        MaDanhMuc = laptopDoHoa.MaDanhMuc,
                        MaThuongHieu = dell.MaThuongHieu,
                        GioiTinh = "Unisex",
                        Gia = 42990000,
                        MoTaNgan = "Dell XPS 15 â€“ Laptop Ä‘á»“ há»a cao cáº¥p vá»›i mÃ n hÃ¬nh OLED 4K tuyá»‡t Ä‘áº¹p",
                        MoTa = "Dell XPS 15 lÃ  laptop cao cáº¥p dÃ nh cho Ä‘á»“ há»a vÃ  sÃ¡ng táº¡o ná»™i dung vá»›i mÃ n hÃ¬nh OLED 4K, Intel Core i7 tháº¿ há»‡ 12, RTX 3050 Ti, thiáº¿t káº¿ kim loáº¡i nguyÃªn khá»‘i sang trá»ng.",
                        ThongSoKyThuat = "CPU: Intel Core i7-12700H<br>RAM: 16GB DDR5<br>á»” cá»©ng: 512GB SSD NVMe<br>VGA: NVIDIA RTX 3050 Ti 4GB<br>MÃ n hÃ¬nh: 15.6 inch OLED 4K<br>Pin: 86Wh<br>Trá»ng lÆ°á»£ng: 1.96kg",
                        SoLuong = 11,
                        TrangThai = 2,
                        LuotXem = 500,
                        NgayTao = DateTime.Now,
                        NgayCapNhat = null,
                        DaXoa = 0,
                        Slug = "dell-xps-15"
                    },
                    new SanPham
                    {
                        HinhAnh = "Lenovo-ThinkPad-X1.png",
                        TenSanPham = "Lenovo ThinkPad X1 Carbon",
                        MaDanhMuc = laptopVanPhong.MaDanhMuc,
                        MaThuongHieu = lenovo.MaThuongHieu,
                        GioiTinh = "Unisex",
                        Gia = 38990000,
                        MoTaNgan = "Lenovo ThinkPad X1 Carbon â€“ Laptop doanh nhÃ¢n cao cáº¥p, siÃªu má»ng nháº¹",
                        MoTa = "Lenovo ThinkPad X1 Carbon lÃ  laptop doanh nhÃ¢n cao cáº¥p vá»›i thiáº¿t káº¿ siÃªu má»ng nháº¹, Ä‘á»™ bá»n quÃ¢n Ä‘á»™i MIL-STD-810H, Intel Core i7 tháº¿ há»‡ 12, bÃ n phÃ­m ThinkPad huyá»n thoáº¡i.",
                        ThongSoKyThuat = "CPU: Intel Core i7-1260P<br>RAM: 16GB LPDDR5<br>á»” cá»©ng: 512GB SSD NVMe<br>VGA: Intel Iris Xe Graphics<br>MÃ n hÃ¬nh: 14 inch 2.8K OLED<br>Pin: 57Wh<br>Trá»ng lÆ°á»£ng: 1.12kg",
                        SoLuong = 17,
                        TrangThai = 2,
                        LuotXem = 250,
                        NgayTao = DateTime.Now,
                        NgayCapNhat = null,
                        DaXoa = 0,
                        Slug = "lenovo-thinkpad-x1-carbon"
                    },
                    new SanPham
                    {
                        HinhAnh = "HP-Envy-14.png",
                        TenSanPham = "HP Envy 14",
                        MaDanhMuc = laptopDoHoa.MaDanhMuc,
                        MaThuongHieu = hp.MaThuongHieu,
                        GioiTinh = "Unisex",
                        Gia = 35990000,
                        MoTaNgan = "HP Envy 14 â€“ Laptop sÃ¡ng táº¡o ná»™i dung vá»›i mÃ n hÃ¬nh 2.8K OLED",
                        MoTa = "HP Envy 14 lÃ  laptop cao cáº¥p dÃ nh cho sÃ¡ng táº¡o ná»™i dung vá»›i mÃ n hÃ¬nh OLED 2.8K, Intel Core i7 tháº¿ há»‡ 12, RTX 3050, thiáº¿t káº¿ kim loáº¡i sang trá»ng vÃ  hiá»‡u nÄƒng máº¡nh máº½.",
                        ThongSoKyThuat = "CPU: Intel Core i7-1260P<br>RAM: 16GB DDR4<br>á»” cá»©ng: 512GB SSD NVMe<br>VGA: NVIDIA RTX 3050 4GB<br>MÃ n hÃ¬nh: 14 inch 2.8K OLED<br>Pin: 68Wh<br>Trá»ng lÆ°á»£ng: 1.49kg",
                        SoLuong = 20,
                        TrangThai = 2,
                        LuotXem = 5000,
                        NgayTao = DateTime.Now,
                        NgayCapNhat = null,
                        DaXoa = 0,
                        Slug = "hp-envy-14"
                    },
                    new SanPham
                    {
                        HinhAnh = "Asus-Vivobook-15.png",
                        TenSanPham = "Asus Vivobook 15",
                        MaDanhMuc = laptopVanPhong.MaDanhMuc,
                        MaThuongHieu = asus.MaThuongHieu,
                        GioiTinh = "Unisex",
                        Gia = 13990000,
                        MoTaNgan = "Asus Vivobook 15 â€“ Laptop vÄƒn phÃ²ng giÃ¡ tá»‘t, thiáº¿t káº¿ tráº» trung",
                        MoTa = "Asus Vivobook 15 lÃ  laptop vÄƒn phÃ²ng vá»›i thiáº¿t káº¿ tráº» trung, nhiá»u mÃ u sáº¯c, hiá»‡u nÄƒng á»•n Ä‘á»‹nh tá»« Intel Core i3 tháº¿ há»‡ 12, phÃ¹ há»£p cho há»c sinh, sinh viÃªn vÃ  cÃ´ng viá»‡c vÄƒn phÃ²ng cÆ¡ báº£n.",
                        ThongSoKyThuat = "CPU: Intel Core i3-1215U<br>RAM: 8GB DDR4<br>á»” cá»©ng: 256GB SSD<br>VGA: Intel UHD Graphics<br>MÃ n hÃ¬nh: 15.6 inch FHD<br>Pin: 42Wh<br>Trá»ng lÆ°á»£ng: 1.7kg",
                        SoLuong = 9,
                        TrangThai = 3,
                        LuotXem = 4000,
                        NgayTao = DateTime.Now,
                        NgayCapNhat = null,
                        DaXoa = 0,
                        Slug = "asus-vivobook-15"
                    },
                    new SanPham
                    {
                        HinhAnh = "Dell-G15.png",
                        TenSanPham = "Dell G15 Gaming",
                        MaDanhMuc = laptopGaming.MaDanhMuc,
                        MaThuongHieu = dell.MaThuongHieu,
                        GioiTinh = "Unisex",
                        Gia = 28990000,
                        MoTaNgan = "Dell G15 Gaming â€“ Laptop gaming giÃ¡ tá»‘t vá»›i hiá»‡u nÄƒng máº¡nh máº½",
                        MoTa = "Dell G15 Gaming lÃ  laptop gaming vá»›i thiáº¿t káº¿ máº¡nh máº½, trang bá»‹ Intel Core i7 tháº¿ há»‡ 12, RTX 3060, mÃ n hÃ¬nh 165Hz, há»‡ thá»‘ng táº£n nhiá»‡t hiá»‡u quáº£ cho tráº£i nghiá»‡m gaming tuyá»‡t vá»i.",
                        ThongSoKyThuat = "CPU: Intel Core i7-12700H<br>RAM: 16GB DDR5<br>á»” cá»©ng: 512GB SSD NVMe<br>VGA: NVIDIA RTX 3060 6GB<br>MÃ n hÃ¬nh: 15.6 inch FHD 165Hz<br>Pin: 56Wh<br>Trá»ng lÆ°á»£ng: 2.65kg",
                        SoLuong = 22,
                        TrangThai = 3,
                        LuotXem = 150,
                        NgayTao = DateTime.Now,
                        NgayCapNhat = null,
                        DaXoa = 0,
                        Slug = "dell-g15-gaming"
                    },
                    new SanPham
                    {
                        HinhAnh = "Asus-Zenbook-14.png",
                        TenSanPham = "Asus Zenbook 14 OLED",
                        MaDanhMuc = laptopDoHoa.MaDanhMuc,
                        MaThuongHieu = asus.MaThuongHieu,
                        GioiTinh = "Unisex",
                        Gia = 29990000,
                        MoTaNgan = "Asus Zenbook 14 OLED â€“ Laptop cao cáº¥p vá»›i mÃ n hÃ¬nh OLED tuyá»‡t Ä‘áº¹p",
                        MoTa = "Asus Zenbook 14 OLED lÃ  laptop cao cáº¥p vá»›i thiáº¿t káº¿ má»ng nháº¹, mÃ n hÃ¬nh OLED 2.8K sá»‘ng Ä‘á»™ng, Intel Core i5 tháº¿ há»‡ 12, phÃ¹ há»£p cho cÃ´ng viá»‡c sÃ¡ng táº¡o vÃ  di Ä‘á»™ng.",
                        ThongSoKyThuat = @"
                            <p><strong>ThÆ°Æ¡ng Hiá»‡u:</strong> Asus</p>
                            <p><strong>DÃ²ng Sáº£n Pháº©m:</strong> Zenbook 14 OLED</p>
                            <p><strong>CPU:</strong> Intel Core i5-1240P</p>
                            <p><strong>RAM:</strong> 8GB LPDDR5</p>
                            <p><strong>á»” cá»©ng:</strong> 512GB SSD NVMe</p>
                            <p><strong>VGA:</strong> Intel Iris Xe Graphics</p>
                            <p><strong>MÃ n hÃ¬nh:</strong> 14 inch 2.8K OLED</p>
                            <p><strong>Pin:</strong> 75Wh</p>
                            <p><strong>Trá»ng lÆ°á»£ng:</strong> 1.39kg</p>
                            <p><strong>Báº£o HÃ nh:</strong> 24 thÃ¡ng</p>
                        ",
                        SoLuong = 10,
                        TrangThai = 3,
                        LuotXem = 999,
                        NgayTao = DateTime.Now,
                        NgayCapNhat = null,
                        DaXoa = 0,
                        Slug = "asus-zenbook-14-oled"

                    }
                );
                await _context.SaveChangesAsync();

                await _context.HinhAnhSanPhams.AddRangeAsync(
                    new HinhAnhSanPham { MaSanPham = 1, HinhAnh = "Curnon_Kashmir_Silver___Abyss-removebg-preview.jpg" },
                    new HinhAnhSanPham { MaSanPham = 1, HinhAnh = "Curnon Kashmir.png" },
                    new HinhAnhSanPham { MaSanPham = 2, HinhAnh = "Citizen-BI5104-57E-2.png" },
                    new HinhAnhSanPham { MaSanPham = 2, HinhAnh = "Citizen-BI5104-57E-3.png" },
                    new HinhAnhSanPham { MaSanPham = 2, HinhAnh = "Citizen-Box1-2.png" },
                    new HinhAnhSanPham { MaSanPham = 3, HinhAnh = "Citizen-Tsuyosa-3.png" },
                    new HinhAnhSanPham { MaSanPham = 3, HinhAnh = "Citizen-Tsuyosa-2.png" },
                    new HinhAnhSanPham { MaSanPham = 4, HinhAnh = "Citizen-NH9130-84L-2.png" },
                    new HinhAnhSanPham { MaSanPham = 4, HinhAnh = "Citizen-NH9130-84L-3.png" },
                    new HinhAnhSanPham { MaSanPham = 5, HinhAnh = "Citizen-Eco-Drive-2.png" },
                    new HinhAnhSanPham { MaSanPham = 5, HinhAnh = "Citizen-Eco-Drive-3.png" },
                    new HinhAnhSanPham { MaSanPham = 5, HinhAnh = "Citizen-Eco-Drive-4.png" },
                    new HinhAnhSanPham { MaSanPham = 6, HinhAnh = "Citizen-EM0863-53D-2.png" },
                    new HinhAnhSanPham { MaSanPham = 6, HinhAnh = "Citizen-EM0863-53D-3.png" },
                    new HinhAnhSanPham { MaSanPham = 6, HinhAnh = "Citizen-EM0863-53D-4.png" },
                    new HinhAnhSanPham { MaSanPham = 7, HinhAnh = "Doxa-Executive-Slim-2.png" },
                    new HinhAnhSanPham { MaSanPham = 7, HinhAnh = "Doxa-Executive-Slim-3.png" },
                    new HinhAnhSanPham { MaSanPham = 8, HinhAnh = "Doxa-x-Dorian-Ho-Earlymoon.png" },
                    new HinhAnhSanPham { MaSanPham = 8, HinhAnh = "Doxa-x-Dorian-Ho-Earlymoon-2.png" },
                    new HinhAnhSanPham { MaSanPham = 9, HinhAnh = "Doxa-Noble.png" },
                    new HinhAnhSanPham { MaSanPham = 9, HinhAnh = "Doxa-Box-2.png" },
                    new HinhAnhSanPham { MaSanPham = 10, HinhAnh = "Hop-Seiko.png" },
                    new HinhAnhSanPham { MaSanPham = 10, HinhAnh = "Seiko-SSC943P1-2.png" },
                    new HinhAnhSanPham { MaSanPham = 10, HinhAnh = "Seiko-SSC943P1-3.png" }
                );
                await _context.SaveChangesAsync();
            }
            if (!_context.BaiViets.Any())
            {
                await _context.BaiViets.AddRangeAsync(
                new BaiViet
                {
                    HinhAnh = "Blog_1.jpg",
                    TieuDe = "HÆ°á»›ng Dáº«n Chá»n Laptop PhÃ¹ Há»£p Vá»›i Nhu Cáº§u",
                    NoiDung = "KhÃ¡m phÃ¡ cÃ¡ch chá»n laptop phÃ¹ há»£p vá»›i nhu cáº§u sá»­ dá»¥ng cá»§a báº¡n, tá»« gaming, vÄƒn phÃ²ng Ä‘áº¿n Ä‘á»“ há»a chuyÃªn nghiá»‡p."
                },
                new BaiViet
                {
                    HinhAnh = "Blog_Meo.jpg",
                    TieuDe = "Máº¹o Báº£o Quáº£n VÃ  Vá»‡ Sinh Laptop",
                    NoiDung = "Há»c cÃ¡ch báº£o quáº£n vÃ  vá»‡ sinh laptop Ä‘Ãºng cÃ¡ch Ä‘á»ƒ mÃ¡y luÃ´n hoáº¡t Ä‘á»™ng tá»‘t vÃ  bá»n lÃ¢u theo thá»i gian."
                },
                new BaiViet
                {
                    HinhAnh = "Blog_YN.jpg",
                    TieuDe = "Táº§m Quan Trá»ng Cá»§a Laptop Trong Cuá»™c Sá»‘ng Hiá»‡n Äáº¡i",
                    NoiDung = "Laptop Ä‘Ã£ trá»Ÿ thÃ nh cÃ´ng cá»¥ khÃ´ng thá»ƒ thiáº¿u trong cuá»™c sá»‘ng hiá»‡n Ä‘áº¡i, phá»¥c vá»¥ cho cÃ´ng viá»‡c, há»c táº­p vÃ  giáº£i trÃ­."
                },
                new BaiViet
                {
                    HinhAnh = "Blog_PC.jpg",
                    TieuDe = "Chá»n Laptop Gaming Hay Laptop VÄƒn PhÃ²ng?",
                    NoiDung = "Báº¡n Ä‘ang phÃ¢n vÃ¢n giá»¯a laptop gaming vÃ  laptop vÄƒn phÃ²ng? HÃ£y tham kháº£o bÃ i viáº¿t nÃ y Ä‘á»ƒ Ä‘Æ°a ra quyáº¿t Ä‘á»‹nh Ä‘Ãºng Ä‘áº¯n."
                },
                new BaiViet
                {
                    HinhAnh = "Blog_Hublot.jpg",
                    TieuDe = "So SÃ¡nh CÃ¡c ThÆ°Æ¡ng Hiá»‡u Laptop HÃ ng Äáº§u",
                    NoiDung = "TÃ¬m hiá»ƒu vá» cÃ¡c thÆ°Æ¡ng hiá»‡u laptop hÃ ng Ä‘áº§u nhÆ° Dell, HP, Lenovo, Asus vÃ  Ä‘iá»ƒm máº¡nh cá»§a tá»«ng thÆ°Æ¡ng hiá»‡u."
                },
                new BaiViet
                {
                    HinhAnh = "Blog_Co.jpg",
                    TieuDe = "Top 10 Laptop ÄÃ¡ng Mua Nháº¥t NÄƒm 2025",
                    NoiDung = "KhÃ¡m phÃ¡ danh sÃ¡ch top 10 máº«u laptop Ä‘Ã¡ng mua nháº¥t trong nÄƒm 2025 vá»›i hiá»‡u nÄƒng vÆ°á»£t trá»™i vÃ  giÃ¡ cáº£ há»£p lÃ½."
                }
                );
                await _context.SaveChangesAsync();
            }
            if (!_context.HinhAnhBaiViets.Any())
            {
                await _context.HinhAnhBaiViets.AddRangeAsync
                (
                new  HinhAnhBaiViet { MaBaiViet = 1, NoiDung = "HÃ¬nh áº£nh chi tiáº¿t vá» cÃ¡c loáº¡i laptop", HinhAnh = "Blog_1_detail.jpg" },
                new HinhAnhBaiViet { MaBaiViet = 1, NoiDung = "HÃ¬nh áº£nh so sÃ¡nh cáº¥u hÃ¬nh laptop", HinhAnh = "Blog_1_mechanism.jpg" },
                new HinhAnhBaiViet { MaBaiViet = 2, NoiDung = "HÃ¬nh áº£nh cÃ¡c dá»¥ng cá»¥ vá»‡ sinh laptop", HinhAnh = "Blog_Meo_tools.jpg" },
                new HinhAnhBaiViet { MaBaiViet = 2, NoiDung = "HÃ¬nh áº£nh quy trÃ¬nh báº£o quáº£n laptop", HinhAnh = "Blog_Meo_process.jpg" },
                new HinhAnhBaiViet { MaBaiViet = 3, NoiDung = "HÃ¬nh áº£nh laptop trong cÃ´ng viá»‡c", HinhAnh = "Blog_YN_watch.jpg" },
                new HinhAnhBaiViet { MaBaiViet = 3, NoiDung = "HÃ¬nh áº£nh lá»‹ch sá»­ phÃ¡t triá»ƒn laptop", HinhAnh = "Blog_YN_history.jpg" },
                new HinhAnhBaiViet { MaBaiViet = 4, NoiDung = "HÃ¬nh áº£nh cÃ¡c máº«u laptop gaming", HinhAnh = "Blog_PC_style.jpg" },
                new HinhAnhBaiViet { MaBaiViet = 4, NoiDung = "HÃ¬nh áº£nh laptop vÄƒn phÃ²ng", HinhAnh = "Blog_PC_special.jpg" },
                new HinhAnhBaiViet { MaBaiViet = 5, NoiDung = "HÃ¬nh áº£nh cÃ¡c thÆ°Æ¡ng hiá»‡u laptop", HinhAnh = "Blog_Hublot_types.jpg" },
                new HinhAnhBaiViet { MaBaiViet = 5, NoiDung = "HÃ¬nh áº£nh chi tiáº¿t cáº¥u hÃ¬nh laptop", HinhAnh = "Blog_Hublot_parts.jpg" },
                new HinhAnhBaiViet { MaBaiViet = 6, NoiDung = "HÃ¬nh áº£nh cÃ¡c máº«u laptop cao cáº¥p", HinhAnh = "Blog_Co_men_watch.jpg" },
                new HinhAnhBaiViet { MaBaiViet = 6, NoiDung = "HÃ¬nh áº£nh laptop ná»•i báº­t nÄƒm 2025", HinhAnh = "Blog_Co_2025.jpg" }
                );
                await _context.SaveChangesAsync();
            }

            if (!_context.Footers.Any())
            {
                await _context.Footers.AddRangeAsync(
                    new Footer
                    {
                        Logo = "Logo.png",
                        MoTa = "ZZZ khÃ´ng chá»‰ lÃ  nÆ¡i Ä‘á»ƒ mua sáº¯m, mÃ  cÃ²n lÃ  má»™t nÆ¡i Ä‘á»ƒ khÃ¡m phÃ¡, tÃ¬m hiá»ƒu vÃ  Ä‘áº¯m mÃ¬nh trong tháº¿ giá»›i laptop cÃ´ng nghá»‡.",
                        DiaChi = "65 Ä. Huá»³nh ThÃºc KhÃ¡ng, Báº¿n NghÃ©, Quáº­n 1, Há»“ ChÃ­ Minh",
                        Email = "contact@zzz.com",
                        SoDienThoai = "0123456789",
                        FacebookUrl = "https://www.facebook.com/ZZZLAPTOPS/",
                        TrangThai = true
                    }
                );
                await _context.SaveChangesAsync();
            }
            if (!_context.FooterLinks.Any())
            {
                await _context.FooterLinks.AddRangeAsync(
                    new FooterLink { TieuDe = "Giá»›i Thiá»‡u", Url = "/Home/Introduction", MaNhom = 1, ThuTuHienThi = 1, TrangThai = true },
                    new FooterLink { TieuDe = "LiÃªn Há»‡", Url = "/Home/Contact", MaNhom = 1, ThuTuHienThi = 2, TrangThai = true },
                    // NhÃ³m TÃ i Khoáº£n (MaNhom = 2)
                    new FooterLink { TieuDe = "TÃ i Khoáº£n Cá»§a TÃ´i", Url = "/Account/Index", MaNhom = 2, ThuTuHienThi = 1, TrangThai = true },
                    new FooterLink { TieuDe = "YÃªu ThÃ­ch", Url = "/Account/Favorite", MaNhom = 2, ThuTuHienThi = 2, TrangThai = true },
                    new FooterLink { TieuDe = "Lá»‹ch Sá»­ ÄÆ¡n HÃ ng", Url = "/Account/Order", MaNhom = 2, ThuTuHienThi = 3, TrangThai = true },
                    // NhÃ³m Danh Má»¥c (MaNhom = 3)
                    new FooterLink { TieuDe = "Laptop Gaming", Url = "/laptop-gaming", MaNhom = 3, ThuTuHienThi = 1, TrangThai = true },
                    new FooterLink { TieuDe = "Laptop VÄƒn PhÃ²ng", Url = "/laptop-van-phong", MaNhom = 3, ThuTuHienThi = 2, TrangThai = true },
                    new FooterLink { TieuDe = "Laptop Äá»“ Há»a", Url = "/laptop-do-hoa", MaNhom = 3, ThuTuHienThi = 3, TrangThai = true },
                    new FooterLink { TieuDe = "Laptop Dell", Url = "/dell", MaNhom = 3, ThuTuHienThi = 4, TrangThai = true },
                    new FooterLink { TieuDe = "Laptop Asus", Url = "/asus", MaNhom = 3, ThuTuHienThi = 5, TrangThai = true }
                );
                await _context.SaveChangesAsync();
            }

            if (!_context.Sliders.Any())
            {
                await _context.Sliders.AddRangeAsync
                (
                    new Slider { TieuDe = "Laptop Dell Inspiron", MoTa = "Sáº£n Pháº©m Ná»•i Báº­t", HinhAnh = "/HinhAnhs/laptop-banner-1.jpg", Link = "/Product/ProductDetail/2", ThuTuHienThi = 1, TrangThai = true },
                    new Slider { TieuDe = "Laptop Gaming", MoTa = "Giáº£m giÃ¡ Ä‘áº¿n 15%", HinhAnh = "/HinhAnhs/laptop-banner-2.jpg", Link = "/Product/ProductDetail/1", ThuTuHienThi = 2, TrangThai = true },
                    new Slider { TieuDe = "Laptop Cao Cáº¥p", MoTa = "Biá»ƒu tÆ°á»£ng cá»§a cÃ´ng nghá»‡ vÃ  hiá»‡u nÄƒng", HinhAnh = "/HinhAnhs/laptop-banner-3.jpg", Link = "/Product/ProductDetail/5", ThuTuHienThi = 3, TrangThai = true }
                );
                await _context.SaveChangesAsync();
            }

            if (!_context.GioiThieus.Any())
            {
                await _context.GioiThieus.AddRangeAsync
                (
                    new GioiThieu
                    {
                        NoiDung = @"
                        ZZZ LAPTOP khÃ´ng chá»‰ lÃ  nÆ¡i Ä‘á»ƒ mua sáº¯m, mÃ  cÃ²n lÃ  má»™t nÆ¡i Ä‘á»ƒ khÃ¡m phÃ¡, tÃ¬m hiá»ƒu vÃ  Ä‘áº¯m mÃ¬nh trong tháº¿ giá»›i Ä‘á»“ng há»“.
                        <br />
                        ZZZ LAPTOP Ä‘Æ°á»£c xÃ¢y dá»±ng nháº±m cung cáº¥p cho khÃ¡ch hÃ ng nhá»¯ng sáº£n pháº©m Ä‘á»“ng há»“ Ä‘eo tay cao cáº¥p, cháº¥t lÆ°á»£ng, 
                        chÃ­nh hÃ£ng cam káº¿t mang Ä‘áº¿n cho khÃ¡ch hÃ ng nhá»¯ng máº«u Ä‘á»“ng há»“ hoÃ n háº£o vá» cáº£ thiáº¿t káº¿ láº«n tÃ­nh nÄƒng 
                        vÃ  hoÃ n thÃ nh sá»© má»‡nh â€œNÆ¡i An TÃ¢m Mua Äá»“ng Há»“ ChÃ­nh HÃ£ngâ€. Äá»“ng thá»i chÃºng tÃ´i cÅ©ng hÆ°á»›ng Ä‘áº¿n  nhá»¯ng tráº£i nghiá»‡m dá»… dÃ ng, 
                        an toÃ n vÃ  nhanh chÃ³ng khi mua sáº¯m trá»±c tuyáº¿n thÃ´ng qua há»‡ thá»‘ng há»— trá»£ thanh toÃ¡n vÃ  váº­n hÃ nh vá»¯ng máº¡nh.
                        ",
                        DiaChi = "65 Ä. Huá»³nh ThÃºc KhÃ¡ng, Báº¿n NghÃ©, Quáº­n 1, Há»“ ChÃ­ Minh",
                        SoDienThoai = "0306221377",
                        Email = "0306221377@caothang.edu.vn"
                    }
                );
                await _context.SaveChangesAsync();
            }
            if (!_context.ChinhSachs.Any())
            {
                await _context.ChinhSachs.AddRangeAsync
                (
                    new ChinhSach
                    {
                        TieuDe = "Giao hÃ ng nhanh",
                        NoiDung = @"ChÃºng tÃ´i cam káº¿t cung cáº¥p dá»‹ch vá»¥ giao hÃ ng nhanh chÃ³ng vÃ  Ä‘Ã¡ng tin cáº­y. ÄÆ¡n hÃ ng cá»§a báº¡n sáº½ Ä‘Æ°á»£c xá»­ lÃ½ vÃ  giao trong vÃ²ng 1-2 ngÃ y lÃ m viá»‡c, tÃ¹y thuá»™c vÃ o Ä‘á»‹a chá»‰ giao hÃ ng. 
                                Äáº·c biá»‡t, Ä‘á»‘i vá»›i cÃ¡c Ä‘Æ¡n hÃ ng trong khu vá»±c ná»™i thÃ nh, chÃºng tÃ´i sáº½ giao trong ngÃ y náº¿u Ä‘Æ¡n hÃ ng Ä‘Æ°á»£c Ä‘áº·t trÆ°á»›c 12h00. 
                                Má»i chi phÃ­ giao hÃ ng sáº½ Ä‘Æ°á»£c hiá»ƒn thá»‹ rÃµ rÃ ng khi báº¡n thanh toÃ¡n, vÃ  miá»…n phÃ­ váº­n chuyá»ƒn cho Ä‘Æ¡n hÃ ng cÃ³ giÃ¡ trá»‹ tá»« [sá»‘ tiá»n cá»¥ thá»ƒ] trá»Ÿ lÃªn. 
                                ChÃºng tÃ´i luÃ´n ná»— lá»±c mang Ä‘áº¿n tráº£i nghiá»‡m giao hÃ ng nhanh chÃ³ng, tiá»‡n lá»£i vÃ  khÃ´ng gÃ¢y phiá»n phá»©c cho khÃ¡ch hÃ ng."
                    },
                    new ChinhSach
                    {
                        TieuDe = "Miá»…n phÃ­ giao hÃ ng",
                        NoiDung = @"Cá»­a hÃ ng sáº½ miá»…n phÃ­ giao hÃ ng cho táº¥t cáº£ cÃ¡c Ä‘Æ¡n hÃ ng trong pháº¡m vi ná»™i thÃ nh.
                                Äá»‘i vá»›i cÃ¡c Ä‘Æ¡n hÃ ng á»Ÿ pháº¡m vi ngoÃ i thÃ nh phá»‘ thÃ¬ sáº½ Ä‘Æ°á»£c tÃ­nh phÃ­ váº­n chuyá»ƒn.
                                Thá»i gian nháº­n hÃ ng sáº½ tá»« 1-5 ngÃ y tÃ¹y vÃ o Ä‘á»‹a Ä‘iá»ƒm nháº­n hÃ ng.
                                Cá»­a hÃ ng sáº½ lá»±a chá»n Ä‘á»‘i tÃ¡c váº­n chuyá»ƒn uy tÃ­n Ä‘á»ƒ Ä‘áº£m báº£o laptop Ä‘Æ°á»£c giao Ä‘áº¿n khÃ¡ch hÃ ng má»™t cÃ¡ch an toÃ n vÃ  Ä‘Ãºng thá»i gian.
                                Trong quÃ¡ trÃ¬nh váº­n chuyá»ƒn, náº¿u sáº£n pháº©m bá»‹ hÆ° há»ng hoáº·c tháº¥t láº¡c, cá»­a hÃ ng sáº½ chá»‹u trÃ¡ch nhiá»‡m hoÃ n toÃ n vÃ  cÃ³ thá»ƒ gá»­i láº¡i sáº£n pháº©m má»›i hoáº·c hoÃ n tiá»n cho khÃ¡ch hÃ ng.
                                ChÃ­nh sÃ¡ch miá»…n phÃ­ giao hÃ ng cÃ³ thá»ƒ khÃ´ng Ã¡p dá»¥ng cho cÃ¡c khu vá»±c vÃ¹ng sÃ¢u, vÃ¹ng xa hoáº·c quá»‘c táº¿, vÃ  trong trÆ°á»ng há»£p nÃ y, khÃ¡ch hÃ ng sáº½ Ä‘Æ°á»£c thÃ´ng bÃ¡o rÃµ rÃ ng vá» cÃ¡c chi phÃ­ phÃ¡t sinh."
                    },
                    new ChinhSach
                    {
                        TieuDe = "Cam káº¿t chÃ­nh hÃ£ng",
                        NoiDung = @"Cá»­a hÃ ng cam káº¿t táº¥t cáº£ Ä‘á»“ng há»“ bÃ¡n ra Ä‘á»u lÃ  hÃ ng chÃ­nh hÃ£ng, Ä‘Æ°á»£c nháº­p kháº©u hoáº·c phÃ¢n phá»‘i trá»±c tiáº¿p tá»« nhÃ  sáº£n xuáº¥t hoáº·c Ä‘áº¡i lÃ½ á»§y quyá»n.
                                Má»—i sáº£n pháº©m sáº½ Ä‘i kÃ¨m vá»›i cÃ¡c giáº¥y tá» chá»©ng nháº­n chÃ­nh hÃ£ng, bao gá»“m sá»• báº£o hÃ nh, hÃ³a Ä‘Æ¡n mua hÃ ng, vÃ  cÃ¡c giáº¥y tá» liÃªn quan khÃ¡c.
                                Äá»“ng há»“ mua táº¡i cá»­a hÃ ng sáº½ Ä‘Æ°á»£c báº£o hÃ nh theo tiÃªu chuáº©n cá»§a nhÃ  sáº£n xuáº¥t. Thá»i gian báº£o hÃ nh vÃ  cÃ¡c dá»‹ch vá»¥ Ä‘i kÃ¨m sáº½ Ä‘Æ°á»£c thá»±c hiá»‡n táº¡i cÃ¡c trung tÃ¢m báº£o hÃ nh á»§y quyá»n.
                                Náº¿u khÃ¡ch hÃ ng chá»©ng minh Ä‘Æ°á»£c sáº£n pháº©m lÃ  hÃ ng giáº£, cá»­a hÃ ng cam káº¿t hoÃ n tráº£ toÃ n bá»™ sá»‘ tiá»n Ä‘Ã£ thanh toÃ¡n vÃ  cÃ³ thá»ƒ bá»“i thÆ°á»ng thÃªm tÃ¹y theo chÃ­nh sÃ¡ch cá»¥ thá»ƒ.
                                Cá»­a hÃ ng sáº½ cung cáº¥p dá»‹ch vá»¥ háº­u mÃ£i, bao gá»“m sá»­a chá»¯a vÃ  báº£o trÃ¬ Ä‘á»“ng há»“, vá»›i cam káº¿t sá»­ dá»¥ng linh kiá»‡n chÃ­nh hÃ£ng.
                                Cá»­a hÃ ng cÃ³ thá»ƒ Ã¡p dá»¥ng chÃ­nh sÃ¡ch Ä‘á»•i tráº£ linh hoáº¡t náº¿u khÃ¡ch hÃ ng phÃ¡t hiá»‡n sáº£n pháº©m cÃ³ lá»—i sáº£n xuáº¥t hoáº·c khÃ´ng Ä‘Ãºng vá»›i mÃ´ táº£ ban Ä‘áº§u."
                    }
                );
                await _context.SaveChangesAsync();
            }
            if (!_context.VaiTros.Any())
            {
                await _context.VaiTros.AddRangeAsync
                (
                    new VaiTro { Loai = "User" },
                    new VaiTro { Loai = "Admin" }
                );
                await _context.SaveChangesAsync();
            }
            if (!_context.TaiKhoans.Any())
            {
                await _context.TaiKhoans.AddRangeAsync
                (
                    new TaiKhoan { TenDangNhap = "admin", MatKhau = "admin", MaVaiTro = 2, },
                    new TaiKhoan { TenDangNhap = "user1", MatKhau = "user1", MaVaiTro = 1 },
                    new TaiKhoan { TenDangNhap = "user2", MatKhau = "user2", MaVaiTro = 1, },
                    new TaiKhoan { TenDangNhap = "user3", MatKhau = "user3", MaVaiTro = 1, },
                    new TaiKhoan { TenDangNhap = "user4", MatKhau = "user4", MaVaiTro = 1 },
                    new TaiKhoan { TenDangNhap = "user5", MatKhau = "user5", MaVaiTro = 1, },
                    new TaiKhoan { TenDangNhap = "user6", MatKhau = "user6", MaVaiTro = 1 },
                    new TaiKhoan { TenDangNhap = "user7", MatKhau = "user7", MaVaiTro = 1, },
                    new TaiKhoan { TenDangNhap = "user8", MatKhau = "user8", MaVaiTro = 1 },
                    new TaiKhoan { TenDangNhap = "user9", MatKhau = "user9", MaVaiTro = 1, },
                    new TaiKhoan { TenDangNhap = "user10", MatKhau = "user10", MaVaiTro = 1 }
                );
                await _context.SaveChangesAsync();
            }
            if (!_context.KhachHangs.Any())
            {
                await _context.KhachHangs.AddRangeAsync
                (
                    new KhachHang { HoTen = "Nguyá»…n VÄƒn A", SoDienThoai = "0123456789", DiaChi = "123 ÄÆ°á»ng ABC, Quáº­n 1", Email = "vana@gmail.com", NgaySinh = DateOnly.ParseExact("1990-01-01", "yyyy-MM-dd"), GioiTinh = true, MaTaiKhoan = 2, TenHienThi = "user1" },

                    new KhachHang { HoTen = "Tráº§n Thá»‹ B", SoDienThoai = "0987654321", DiaChi = "456 ÄÆ°á»ng DEF, Quáº­n 2", Email = "btran@gmail.com", NgaySinh = DateOnly.ParseExact("1992-02-02", "yyyy-MM-dd"), GioiTinh = false, MaTaiKhoan = 3, TenHienThi = "user2" },

                    new KhachHang { HoTen = "LÃª VÄƒn C", SoDienThoai = "0123456780", DiaChi = "789 ÄÆ°á»ng GHI, Quáº­n 3", Email = "cle@gmail.com", NgaySinh = DateOnly.ParseExact("1988-03-03", "yyyy-MM-dd"), GioiTinh = true, MaTaiKhoan = 4, TenHienThi = "user3" },

                    new KhachHang { HoTen = "Pháº¡m Thá»‹ D", SoDienThoai = "0987654310", DiaChi = "321 ÄÆ°á»ng JKL, Quáº­n 4", Email = "dpham@gmail.com", NgaySinh = DateOnly.ParseExact("1985-04-04", "yyyy-MM-dd"), GioiTinh = false, MaTaiKhoan = 5, TenHienThi = "user4" },

                    new KhachHang { HoTen = "Nguyá»…n VÄƒn E", SoDienThoai = "0123456790", DiaChi = "654 ÄÆ°á»ng MNO, Quáº­n 5", Email = "evan@gmail.com", NgaySinh = DateOnly.ParseExact("1995-05-05", "yyyy-MM-dd"), GioiTinh = true, MaTaiKhoan = 6, TenHienThi = "user5" },

                    new KhachHang { HoTen = "Tráº§n Thá»‹ F", SoDienThoai = "0987654322", DiaChi = "987 ÄÆ°á»ng PQR, Quáº­n 6", Email = "ftran@gmail.com", NgaySinh = DateOnly.ParseExact("1990-06-06", "yyyy-MM-dd"), GioiTinh = false, MaTaiKhoan = 7, TenHienThi = "user6" },

                    new KhachHang { HoTen = "LÃª VÄƒn G", SoDienThoai = "0123456781", DiaChi = "135 ÄÆ°á»ng STU, Quáº­n 7", Email = "gle@gmail.com", NgaySinh = DateOnly.ParseExact("1982-07-07", "yyyy-MM-dd"), GioiTinh = true, MaTaiKhoan = 8, TenHienThi = "user7" },

                    new KhachHang { HoTen = "Pháº¡m Thá»‹ H", SoDienThoai = "0987654311", DiaChi = "246 ÄÆ°á»ng VWX, Quáº­n 8", Email = "hpham@gmail.com", NgaySinh = DateOnly.ParseExact("2000-07-07", "yyyy-MM-dd"), GioiTinh = true, MaTaiKhoan = 9, TenHienThi = "user8" },

                    new KhachHang { HoTen = "Nguyá»…n VÄƒn I", SoDienThoai = "0123456791", DiaChi = "357 ÄÆ°á»ng YZ, Quáº­n 9", Email = "ivan@gmail.com", NgaySinh = DateOnly.ParseExact("2002-08-30", "yyyy-MM-dd"), GioiTinh = true, MaTaiKhoan = 10, TenHienThi = "user9" },

                    new KhachHang { HoTen = "Tráº§n Thá»‹ J", SoDienThoai = "0987654323", DiaChi = "468 ÄÆ°á»ng ABCD, Quáº­n 10", Email = "jtran@gmail.com", NgaySinh = DateOnly.ParseExact("1996-01-11", "yyyy-MM-dd"), GioiTinh = true, MaTaiKhoan = 11, TenHienThi = "user10" }
                );
                await _context.SaveChangesAsync();
            }
            if (!_context.HoaDons.Any())
            {
                await _context.HoaDons.AddRangeAsync
                (
                    new HoaDon { MaKhachHang = 1, NgayDatHang = new DateTime(2021, 5, 15), HoTen = "Nguyá»…n VÄƒn A", SoDienThoai = "0123456789", Email = "vana@gmail.com", DiaChi = "123 ÄÆ°á»ng ABC, Quáº­n 1", Tinh = "TPHCM", Huyen = "Quáº­n 1", Xa = "PhÆ°á»ng 1", PhuongThucThanhToan = "Momo", TongTien = 15118000, TrangThai = 2 },

                    new HoaDon { MaKhachHang = 2, NgayDatHang = new DateTime(2021, 6, 20), HoTen = "Tráº§n Thá»‹ B", SoDienThoai = "0987654321", Email = "btran@gmail.com", DiaChi = "456 ÄÆ°á»ng DEF, Quáº­n 2", Tinh = "TPHCM", Huyen = "Quáº­n 2", Xa = "PhÆ°á»ng 2", PhuongThucThanhToan = "COD", TongTien = 32655000, TrangThai = 2 },

                    new HoaDon { MaKhachHang = 3, NgayDatHang = new DateTime(2022, 1, 10), HoTen = "LÃª VÄƒn C", SoDienThoai = "0123456780", Email = "cle@gmail.com", DiaChi = "789 ÄÆ°á»ng GHI, Quáº­n 3", Tinh = "HÃ  Ná»™i", Huyen = "Quáº­n 3", Xa = "PhÆ°á»ng 3", PhuongThucThanhToan = "Momo", TongTien = 32955000, TrangThai = 2 },

                    new HoaDon { MaKhachHang = 4, NgayDatHang = new DateTime(2022, 3, 15), HoTen = "Pháº¡m Thá»‹ D", SoDienThoai = "0987654310", Email = "dpham@gmail.com", DiaChi = "321 ÄÆ°á»ng JKL, Quáº­n 4", Tinh = "ÄÃ  Náºµng", Huyen = "Quáº­n 4", Xa = "PhÆ°á»ng 4", PhuongThucThanhToan = "COD", TongTien = 27830000, TrangThai = 2 },

                    new HoaDon { MaKhachHang = 5, NgayDatHang = new DateTime(2023, 2, 25), HoTen = "Nguyá»…n VÄƒn E", SoDienThoai = "0123456790", Email = "evan@gmail.com", DiaChi = "654 ÄÆ°á»ng MNO, Quáº­n 5", Tinh = "Háº£i PhÃ²ng", Huyen = "Quáº­n 5", Xa = "PhÆ°á»ng 5", PhuongThucThanhToan = "Momo", TongTien = 75090000, TrangThai = 2 },

                    new HoaDon { MaKhachHang = 6, NgayDatHang = new DateTime(2023, 4, 30), HoTen = "Tráº§n Thá»‹ F", SoDienThoai = "0987654322", Email = "ftran@gmail.com", DiaChi = "987 ÄÆ°á»ng PQR, Quáº­n 6", Tinh = "TPHCM", Huyen = "Quáº­n 6", Xa = "PhÆ°á»ng 6", PhuongThucThanhToan = "COD", TongTien = 51758000, TrangThai = 2 },
                            
                    new HoaDon { MaKhachHang = 7, NgayDatHang = new DateTime(2024, 7, 5), HoTen = "LÃª VÄƒn G", SoDienThoai = "0123456781", Email = "gle@gmail.com", DiaChi = "135 ÄÆ°á»ng STU, Quáº­n 7", Tinh = "HÃ  Ná»™i", Huyen = "Quáº­n 7", Xa = "PhÆ°á»ng 7", PhuongThucThanhToan = "Momo", TongTien = 30255000, TrangThai = 2 },

                    new HoaDon { MaKhachHang = 8, NgayDatHang = new DateTime(2024, 9, 10), HoTen = "Pháº¡m Thá»‹ H", SoDienThoai = "0987654311", Email = "hpham@gmail.com", DiaChi = "246 ÄÆ°á»ng VWX, Quáº­n 8", Tinh = "ÄÃ  Náºµng", Huyen = "Quáº­n 8", Xa = "PhÆ°á»ng 8", PhuongThucThanhToan = "COD", TongTien = 7585000, TrangThai = 2 },

                    new HoaDon { MaKhachHang = 9, NgayDatHang = new DateTime(2025, 1, 15), HoTen = "Nguyá»…n VÄƒn I", SoDienThoai = "0123456791", Email = "ivan@gmail.com", DiaChi = "357 ÄÆ°á»ng YZ, Quáº­n 9", Tinh = "Háº£i PhÃ²ng", Huyen = "Quáº­n 9", Xa = "PhÆ°á»ng 9", PhuongThucThanhToan = "Momo", TongTien = 63425000, TrangThai = 2 },

                    new HoaDon { MaKhachHang = 10, NgayDatHang = new DateTime(2025, 3, 20), HoTen = "Tráº§n Thá»‹ J", SoDienThoai = "0987654323", Email = "jtran@gmail.com", DiaChi = "468 ÄÆ°á»ng ABCD, Quáº­n 10", Tinh = "TPHCM", Huyen = "Quáº­n 10", Xa = "PhÆ°á»ng 10", PhuongThucThanhToan = "COD", TongTien = 11450000, TrangThai = 2 }

                );
                await _context.SaveChangesAsync();
            }
            
            
        }

    }
}
