using Ecommerce_WatchShop.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_WatchShop
{
    public class SeedData
    {
        public static async Task SeedingData(DongHoContext _context)
        {
            await _context.Database.MigrateAsync();
            
            // Seed Vai Trò
            if (!_context.VaiTros.Any())
            {
                VaiTro khachHang = new VaiTro { Loai = "Khách hàng" };
                VaiTro admin = new VaiTro { Loai = "Admin" };

                await _context.VaiTros.AddRangeAsync(khachHang, admin);
                await _context.SaveChangesAsync();
            }

            // Seed Tài Khoản
            if (!_context.TaiKhoans.Any())
            {
                var roleKhachHang = _context.VaiTros.FirstOrDefault(r => r.Loai == "Khách hàng");
                var roleAdmin = _context.VaiTros.FirstOrDefault(r => r.Loai == "Admin");

                // Tài khoản Admin
                TaiKhoan adminAccount = new TaiKhoan
                {
                    TenDangNhap = "admin",
                    MatKhau = "admin123",
                    MaVaiTro = roleAdmin.MaVaiTro
                };

                // Tài khoản User mẫu
                TaiKhoan userAccount = new TaiKhoan
                {
                    TenDangNhap = "user",
                    MatKhau = "user123",
                    MaVaiTro = roleKhachHang.MaVaiTro
                };

                await _context.TaiKhoans.AddRangeAsync(adminAccount, userAccount);
                await _context.SaveChangesAsync();

                // Tạo Khách Hàng cho tài khoản user
                KhachHang khachHangUser = new KhachHang
                {
                    MaTaiKhoan = userAccount.MaTaiKhoan,
                    TenHienThi = "Người dùng mẫu",
                    HoTen = "Nguyễn Văn A",
                    Email = "user@example.com",
                    SoDienThoai = "0123456789",
                    DiaChi = "123 Đường ABC, Quận 1, TP.HCM",
                    GioiTinh = true,
                    NgaySinh = new DateOnly(1990, 1, 1)
                };

                // Tạo Khách Hàng cho tài khoản admin
                KhachHang khachHangAdmin = new KhachHang
                {
                    MaTaiKhoan = adminAccount.MaTaiKhoan,
                    TenHienThi = "Administrator",
                    HoTen = "Admin System",
                    Email = "admin@laptopshop.com",
                    SoDienThoai = "0987654321",
                    DiaChi = "Văn phòng quản trị",
                    GioiTinh = true
                };

                await _context.KhachHangs.AddRangeAsync(khachHangUser, khachHangAdmin);
                await _context.SaveChangesAsync();
            }

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

                DanhMuc laptopGaming = new DanhMuc { TenDanhMuc = "Laptop Gaming", MaDanhMucCha = null, Slug = "laptop-gaming", HinhAnh = "/Images/default-large.jpg" };
                DanhMuc laptopVanPhong = new DanhMuc { TenDanhMuc = "Laptop Văn Phòng", MaDanhMucCha = null, Slug = "laptop-van-phong", HinhAnh = "/Images/Artboard-1.jpg" };
                DanhMuc laptopDoHoa = new DanhMuc { TenDanhMuc = "Laptop Đồ Họa", MaDanhMucCha = null, Slug = "laptop-do-hoa", HinhAnh = "/Images/ricky-kharawala-Yka2yhGJwjc-unsplash 1.png" };

                await _context.DanhMucs.AddRangeAsync(laptopGaming, laptopVanPhong, laptopDoHoa);
                await _context.SaveChangesAsync();
            }
            if (!_context.SanPhams.Any())
            {
                var laptopGaming = _context.DanhMucs.FirstOrDefault(c => c.TenDanhMuc == "Laptop Gaming");
                var laptopVanPhong = _context.DanhMucs.FirstOrDefault(c => c.TenDanhMuc == "Laptop Văn Phòng");
                var laptopDoHoa = _context.DanhMucs.FirstOrDefault(c => c.TenDanhMuc == "Laptop Đồ Họa");

                var dell = _context.ThuongHieus.FirstOrDefault(b => b.TenThuongHieu == "Dell");
                var hp = _context.ThuongHieus.FirstOrDefault(b => b.TenThuongHieu == "HP");
                var lenovo = _context.ThuongHieus.FirstOrDefault(b => b.TenThuongHieu == "Lenovo");
                var asus = _context.ThuongHieus.FirstOrDefault(b => b.TenThuongHieu == "Asus");

                await _context.SanPhams.AddRangeAsync(
                    new SanPham
                    {
                        HinhAnh = "Lenovo-Legion-5.png",
                        TenSanPham = "Lenovo Legion 5",
                        MaDanhMuc = laptopGaming.MaDanhMuc,
                        MaThuongHieu = lenovo.MaThuongHieu,
                        Gia = 25990000,
                        MoTaNgan = "Laptop gaming mạnh mẽ với hiệu năng vượt trội",
                        MoTa = "Lenovo Legion 5 là laptop gaming cao cấp với thiết kế hiện đại, trang bị chip AMD Ryzen 7, card đồ họa RTX 3060, màn hình 15.6 inch 165Hz mang đến trải nghiệm gaming mượt mà.",
                        ThongSoKyThuat = "CPU: AMD Ryzen 7 5800H<br>RAM: 16GB DDR4<br>Ổ cứng: 512GB SSD NVMe<br>VGA: NVIDIA RTX 3060 6GB<br>Màn hình: 15.6 inch FHD 165Hz<br>Pin: 80Wh<br>Trọng lượng: 2.4kg",
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
                        Gia = 15990000,
                        MoTaNgan = "Dell Inspiron 15 3520 - Laptop văn phòng hiệu suất cao, thiết kế thanh lịch",
                        MoTa = "Dell Inspiron 15 3520 là laptop văn phòng lý tưởng với thiết kế mỏng nhẹ, hiệu năng ổn định từ Intel Core i5 thế hệ 12, phù hợp cho công việc văn phòng và học tập.",
                        ThongSoKyThuat = "CPU: Intel Core i5-1235U<br>RAM: 8GB DDR4<br>Ổ cứng: 256GB SSD<br>VGA: Intel UHD Graphics<br>Màn hình: 15.6 inch FHD<br>Pin: 41Wh<br>Trọng lượng: 1.85kg",
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
                        Gia = 32990000,
                        MoTaNgan = "Asus ROG Strix G15 - Laptop gaming cao cấp với hiệu năng đỉnh cao",
                        MoTa = "Asus ROG Strix G15 là laptop gaming cao cấp với thiết kế ROG đặc trưng, trang bị Intel Core i7 thế hệ 12, RTX 3070 Ti, màn hình 300Hz mang đến trải nghiệm gaming đỉnh cao.",
                        ThongSoKyThuat = "CPU: Intel Core i7-12700H<br>RAM: 16GB DDR5<br>Ổ cứng: 1TB SSD NVMe<br>VGA: NVIDIA RTX 3070 Ti 8GB<br>Màn hình: 15.6 inch FHD 300Hz<br>Pin: 90Wh<br>Trọng lượng: 2.3kg",
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
                        Gia = 18990000,
                        MoTaNgan = "HP Pavilion 15 - Laptop văn phòng đa năng, thiết kế sang trọng",
                        MoTa = "HP Pavilion 15 là laptop văn phòng đa năng với thiết kế sang trọng, hiệu năng ổn định từ Intel Core i5, phù hợp cho công việc văn phòng, giải trí và học tập.",
                        ThongSoKyThuat = "CPU: Intel Core i5-1235U<br>RAM: 8GB DDR4<br>Ổ cứng: 512GB SSD<br>VGA: Intel Iris Xe Graphics<br>Màn hình: 15.6 inch FHD<br>Pin: 41Wh<br>Trọng lượng: 1.75kg",
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
                        Gia = 42990000,
                        MoTaNgan = "Dell XPS 15 - Laptop đồ họa cao cấp với màn hình OLED 4K tuyệt đẹp",
                        MoTa = "Dell XPS 15 là laptop cao cấp dành cho đồ họa và sáng tạo nội dung với màn hình OLED 4K, Intel Core i7 thế hệ 12, RTX 3050 Ti, thiết kế kim loại nguyên khối sang trọng.",
                        ThongSoKyThuat = "CPU: Intel Core i7-12700H<br>RAM: 16GB DDR5<br>Ổ cứng: 512GB SSD NVMe<br>VGA: NVIDIA RTX 3050 Ti 4GB<br>Màn hình: 15.6 inch OLED 4K<br>Pin: 86Wh<br>Trọng lượng: 1.96kg",
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
                        Gia = 38990000,
                        MoTaNgan = "Lenovo ThinkPad X1 Carbon - Laptop doanh nhân cao cấp, siêu mỏng nhẹ",
                        MoTa = "Lenovo ThinkPad X1 Carbon là laptop doanh nhân cao cấp với thiết kế siêu mỏng nhẹ, độ bền quân đội MIL-STD-810H, Intel Core i7 thế hệ 12, bàn phím ThinkPad huyền thoại.",
                        ThongSoKyThuat = "CPU: Intel Core i7-1260P<br>RAM: 16GB LPDDR5<br>Ổ cứng: 512GB SSD NVMe<br>VGA: Intel Iris Xe Graphics<br>Màn hình: 14 inch 2.8K OLED<br>Pin: 57Wh<br>Trọng lượng: 1.12kg",
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
                        Gia = 35990000,
                        MoTaNgan = "HP Envy 14 - Laptop sáng tạo nội dung với màn hình 2.8K OLED",
                        MoTa = "HP Envy 14 là laptop cao cấp dành cho sáng tạo nội dung với màn hình OLED 2.8K, Intel Core i7 thế hệ 12, RTX 3050, thiết kế kim loại sang trọng và hiệu năng mạnh mẽ.",
                        ThongSoKyThuat = "CPU: Intel Core i7-1260P<br>RAM: 16GB DDR4<br>Ổ cứng: 512GB SSD NVMe<br>VGA: NVIDIA RTX 3050 4GB<br>Màn hình: 14 inch 2.8K OLED<br>Pin: 68Wh<br>Trọng lượng: 1.49kg",
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
                        Gia = 13990000,
                        MoTaNgan = "Asus Vivobook 15 - Laptop văn phòng giá tốt, thiết kế trẻ trung",
                        MoTa = "Asus Vivobook 15 là laptop văn phòng với thiết kế trẻ trung, nhiều màu sắc, hiệu năng ổn định từ Intel Core i3 thế hệ 12, phù hợp cho học sinh, sinh viên và công việc văn phòng cơ bản.",
                        ThongSoKyThuat = "CPU: Intel Core i3-1215U<br>RAM: 8GB DDR4<br>Ổ cứng: 256GB SSD<br>VGA: Intel UHD Graphics<br>Màn hình: 15.6 inch FHD<br>Pin: 42Wh<br>Trọng lượng: 1.7kg",
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
                        Gia = 28990000,
                        MoTaNgan = "Dell G15 Gaming - Laptop gaming giá tốt với hiệu năng mạnh mẽ",
                        MoTa = "Dell G15 Gaming là laptop gaming với thiết kế mạnh mẽ, trang bị Intel Core i7 thế hệ 12, RTX 3060, màn hình 165Hz, hệ thống tản nhiệt hiệu quả cho trải nghiệm gaming tuyệt vời.",
                        ThongSoKyThuat = "CPU: Intel Core i7-12700H<br>RAM: 16GB DDR5<br>Ổ cứng: 512GB SSD NVMe<br>VGA: NVIDIA RTX 3060 6GB<br>Màn hình: 15.6 inch FHD 165Hz<br>Pin: 56Wh<br>Trọng lượng: 2.65kg",
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
                        Gia = 29990000,
                        MoTaNgan = "Asus Zenbook 14 OLED - Laptop cao cấp với màn hình OLED tuyệt đẹp",
                        MoTa = "Asus Zenbook 14 OLED là laptop cao cấp với thiết kế mỏng nhẹ, màn hình OLED 2.8K sống động, Intel Core i5 thế hệ 12, phù hợp cho công việc sáng tạo và di động.",
                        ThongSoKyThuat = "CPU: Intel Core i5-1240P<br>RAM: 8GB LPDDR5<br>Ổ cứng: 512GB SSD NVMe<br>VGA: Intel Iris Xe Graphics<br>Màn hình: 14 inch 2.8K OLED<br>Pin: 75Wh<br>Trọng lượng: 1.39kg",
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
                    new HinhAnhSanPham { MaSanPham = 1, HinhAnh = "Lenovo-Legion-5-2.jpg" },
                    new HinhAnhSanPham { MaSanPham = 1, HinhAnh = "Lenovo-Legion-5-3.jpg" },
                    new HinhAnhSanPham { MaSanPham = 2, HinhAnh = "Dell-Inspiron-15-2.png" },
                    new HinhAnhSanPham { MaSanPham = 2, HinhAnh = "Dell-Inspiron-15-3.png" },
                    new HinhAnhSanPham { MaSanPham = 2, HinhAnh = "Dell-Box.png" },
                    new HinhAnhSanPham { MaSanPham = 3, HinhAnh = "Asus-ROG-Strix-G15-2.png" },
                    new HinhAnhSanPham { MaSanPham = 3, HinhAnh = "Asus-ROG-Strix-G15-3.png" },
                    new HinhAnhSanPham { MaSanPham = 4, HinhAnh = "HP-Pavilion-15-2.png" },
                    new HinhAnhSanPham { MaSanPham = 4, HinhAnh = "HP-Pavilion-15-3.png" },
                    new HinhAnhSanPham { MaSanPham = 5, HinhAnh = "Dell-XPS-15-2.png" },
                    new HinhAnhSanPham { MaSanPham = 5, HinhAnh = "Dell-XPS-15-3.png" },
                    new HinhAnhSanPham { MaSanPham = 5, HinhAnh = "Dell-XPS-15-4.png" },
                    new HinhAnhSanPham { MaSanPham = 6, HinhAnh = "Lenovo-ThinkPad-X1-2.png" },
                    new HinhAnhSanPham { MaSanPham = 6, HinhAnh = "Lenovo-ThinkPad-X1-3.png" },
                    new HinhAnhSanPham { MaSanPham = 6, HinhAnh = "Lenovo-ThinkPad-X1-4.png" },
                    new HinhAnhSanPham { MaSanPham = 7, HinhAnh = "HP-Envy-14-2.png" },
                    new HinhAnhSanPham { MaSanPham = 7, HinhAnh = "HP-Envy-14-3.png" },
                    new HinhAnhSanPham { MaSanPham = 8, HinhAnh = "Asus-Vivobook-15-2.png" },
                    new HinhAnhSanPham { MaSanPham = 8, HinhAnh = "Asus-Vivobook-15-3.png" },
                    new HinhAnhSanPham { MaSanPham = 9, HinhAnh = "Dell-G15-2.png" },
                    new HinhAnhSanPham { MaSanPham = 9, HinhAnh = "Dell-G15-3.png" },
                    new HinhAnhSanPham { MaSanPham = 10, HinhAnh = "Asus-Zenbook-14-2.png" },
                    new HinhAnhSanPham { MaSanPham = 10, HinhAnh = "Asus-Zenbook-14-3.png" },
                    new HinhAnhSanPham { MaSanPham = 10, HinhAnh = "Asus-Zenbook-14-4.png" }
                );
                await _context.SaveChangesAsync();
            }
            if (!_context.BaiViets.Any())
            {
                await _context.BaiViets.AddRangeAsync(
                new BaiViet
                {
                    HinhAnh = "Blog_1.jpg",
                    TieuDe = "Hướng Dẫn Chọn Laptop Phù Hợp Với Nhu Cầu",
                    NoiDung = "Khám phá cách chọn laptop phù hợp với nhu cầu sử dụng của bạn, từ gaming, văn phòng đến đồ họa chuyên nghiệp."
                },
                new BaiViet
                {
                    HinhAnh = "Blog_Meo.jpg",
                    TieuDe = "Mẹo Bảo Quản Và Vệ Sinh Laptop",
                    NoiDung = "Học cách bảo quản và vệ sinh laptop đúng cách để máy luôn hoạt động tốt và bền lâu theo thời gian."
                },
                new BaiViet
                {
                    HinhAnh = "Blog_YN.jpg",
                    TieuDe = "Tầm Quan Trọng Của Laptop Trong Cuộc Sống Hiện Đại",
                    NoiDung = "Laptop đã trở thành công cụ không thể thiếu trong cuộc sống hiện đại, phục vụ cho công việc, học tập và giải trí."
                },
                new BaiViet
                {
                    HinhAnh = "Blog_PC.jpg",
                    TieuDe = "Chọn Laptop Gaming Hay Laptop Văn Phòng?",
                    NoiDung = "Bạn đang phân vân giữa laptop gaming và laptop văn phòng? Hãy tham khảo bài viết này để đưa ra quyết định đúng đắn."
                },
                new BaiViet
                {
                    HinhAnh = "Blog_Hublot.jpg",
                    TieuDe = "So Sánh Các Thương Hiệu Laptop Hàng Đầu",
                    NoiDung = "Tìm hiểu về các thương hiệu laptop hàng đầu như Dell, HP, Lenovo, Asus và điểm mạnh của từng thương hiệu."
                },
                new BaiViet
                {
                    HinhAnh = "Blog_Co.jpg",
                    TieuDe = "Top 10 Laptop Đáng Mua Nhất Năm 2025",
                    NoiDung = "Khám phá danh sách top 10 mẫu laptop đáng mua nhất trong năm 2025 với hiệu năng vượt trội và giá cả hợp lý."
                }
                );
                await _context.SaveChangesAsync();
            }
            if (!_context.HinhAnhBaiViets.Any())
            {
                await _context.HinhAnhBaiViets.AddRangeAsync
                (
                new  HinhAnhBaiViet { MaBaiViet = 1, NoiDung = "Hình ảnh chi tiết về các loại laptop", HinhAnh = "Blog_1_detail.jpg" },
                new HinhAnhBaiViet { MaBaiViet = 1, NoiDung = "Hình ảnh so sánh cấu hình laptop", HinhAnh = "Blog_1_mechanism.jpg" },
                new HinhAnhBaiViet { MaBaiViet = 2, NoiDung = "Hình ảnh các dụng cụ vệ sinh laptop", HinhAnh = "Blog_Meo_tools.jpg" },
                new HinhAnhBaiViet { MaBaiViet = 2, NoiDung = "Hình ảnh quy trình bảo quản laptop", HinhAnh = "Blog_Meo_process.jpg" },
                new HinhAnhBaiViet { MaBaiViet = 3, NoiDung = "Hình ảnh laptop trong công việc", HinhAnh = "Blog_YN_watch.jpg" },
                new HinhAnhBaiViet { MaBaiViet = 3, NoiDung = "Hình ảnh lịch sử phát triển laptop", HinhAnh = "Blog_YN_history.jpg" },
                new HinhAnhBaiViet { MaBaiViet = 4, NoiDung = "Hình ảnh các mẫu laptop gaming", HinhAnh = "Blog_PC_style.jpg" },
                new HinhAnhBaiViet { MaBaiViet = 4, NoiDung = "Hình ảnh laptop văn phòng", HinhAnh = "Blog_PC_special.jpg" },
                new HinhAnhBaiViet { MaBaiViet = 5, NoiDung = "Hình ảnh các thương hiệu laptop", HinhAnh = "Blog_Hublot_types.jpg" },
                new HinhAnhBaiViet { MaBaiViet = 5, NoiDung = "Hình ảnh chi tiết cấu hình laptop", HinhAnh = "Blog_Hublot_parts.jpg" },
                new HinhAnhBaiViet { MaBaiViet = 6, NoiDung = "Hình ảnh các mẫu laptop cao cấp", HinhAnh = "Blog_Co_men_watch.jpg" },
                new HinhAnhBaiViet { MaBaiViet = 6, NoiDung = "Hình ảnh laptop nổi bật năm 2025", HinhAnh = "Blog_Co_2025.jpg" }
                );
                await _context.SaveChangesAsync();
            }

            if (!_context.Footers.Any())
            {
                await _context.Footers.AddRangeAsync(
                    new Footer
                    {
                        Logo = "Logo.png",
                        MoTa = "ZZZ không chỉ là nơi để mua sắm, mà còn là một nơi để khám phá, tìm hiểu và đắm mình trong thế giới laptop công nghệ.",
                        DiaChi = "65 Đ. Huỳnh Thúc Kháng, Bến Nghé, Quận 1, Hồ Chí Minh",
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
                    new FooterLink { TieuDe = "Giới Thiệu", Url = "/Home/Introduction", MaNhom = 1, ThuTuHienThi = 1, TrangThai = true },
                    new FooterLink { TieuDe = "Liên Hệ", Url = "/Home/Contact", MaNhom = 1, ThuTuHienThi = 2, TrangThai = true },
                    new FooterLink { TieuDe = "Tài Khoản Của Tôi", Url = "/Account/Index", MaNhom = 2, ThuTuHienThi = 1, TrangThai = true },
                    new FooterLink { TieuDe = "Yêu Thích", Url = "/Account/Favorite", MaNhom = 2, ThuTuHienThi = 2, TrangThai = true },
                    new FooterLink { TieuDe = "Lịch Sử Đơn Hàng", Url = "/Account/Order", MaNhom = 2, ThuTuHienThi = 3, TrangThai = true },
                    new FooterLink { TieuDe = "Laptop Gaming", Url = "/laptop-gaming", MaNhom = 3, ThuTuHienThi = 1, TrangThai = true },
                    new FooterLink { TieuDe = "Laptop Văn Phòng", Url = "/laptop-van-phong", MaNhom = 3, ThuTuHienThi = 2, TrangThai = true },
                    new FooterLink { TieuDe = "Laptop Đồ Họa", Url = "/laptop-do-hoa", MaNhom = 3, ThuTuHienThi = 3, TrangThai = true },
                    new FooterLink { TieuDe = "Laptop Dell", Url = "/dell", MaNhom = 3, ThuTuHienThi = 4, TrangThai = true },
                    new FooterLink { TieuDe = "Laptop Asus", Url = "/asus", MaNhom = 3, ThuTuHienThi = 5, TrangThai = true }
                );
                await _context.SaveChangesAsync();
            }

            if (!_context.Sliders.Any())
            {
                await _context.Sliders.AddRangeAsync
                (
                    new Slider { TieuDe = "Laptop Dell Inspiron", MoTa = "Sản Phẩm Nổi Bật", HinhAnh = "/Images/ricky-kharawala-Yka2yhGJwjc-unsplash 1.png", Link = "/Product/ProductDetail/2", ThuTuHienThi = 1, TrangThai = true },
                    new Slider { TieuDe = "Laptop Gaming", MoTa = "Giảm giá đến 15%", HinhAnh = "/Images/Artboard-1.jpg", Link = "/Product/ProductDetail/1", ThuTuHienThi = 2, TrangThai = true },
                    new Slider { TieuDe = "Laptop Cao Cấp", MoTa = "Biểu tượng của công nghệ và hiệu năng", HinhAnh = "/Images/default-large.jpg", Link = "/Product/ProductDetail/5", ThuTuHienThi = 3, TrangThai = true }
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
                        ZZZ LAPTOP không chỉ là nơi để mua sắm, mà còn là một nơi để khám phá, tìm hiểu và đắm mình trong thế giới công nghệ laptop.
                        <br />
                        ZZZ LAPTOP được xây dựng nhằm cung cấp cho khách hàng những sản phẩm laptop cao cấp, chất lượng, 
                        chính hãng cam kết mang đến cho khách hàng những mẫu laptop hoàn hảo về cả thiết kế lẫn hiệu năng 
                        và hoàn thành sứ mệnh ""Nơi An Tâm Mua Laptop Chính Hãng"". Đồng thời chúng tôi cũng hướng đến những trải nghiệm dễ dàng, 
                        an toàn và nhanh chóng khi mua sắm trực tuyến thông qua hệ thống hỗ trợ thanh toán và vận hành vững mạnh.
                        ",
                        DiaChi = "65 Đ. Huỳnh Thúc Kháng, Bến Nghé, Quận 1, Hồ Chí Minh",
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
                        TieuDe = "Giao hàng nhanh",
                        NoiDung = @"Chúng tôi cam kết cung cấp dịch vụ giao hàng nhanh chóng và đáng tin cậy. Đơn hàng của bạn sẽ được xử lý và giao trong vòng 1-2 ngày làm việc, tùy thuộc vào địa chỉ giao hàng. 
                                Đặc biệt, đối với các đơn hàng trong khu vực nội thành, chúng tôi sẽ giao trong ngày nếu đơn hàng được đặt trước 12h00. 
                                Mọi chi phí giao hàng sẽ được hiển thị rõ ràng khi bạn thanh toán, và miễn phí vận chuyển cho đơn hàng có giá trị từ 15 triệu đồng trở lên. 
                                Chúng tôi luôn nỗ lực mang đến trải nghiệm giao hàng nhanh chóng, tiện lợi và không gây phiền phức cho khách hàng."
                    },
                    new ChinhSach
                    {
                        TieuDe = "Miễn phí giao hàng",
                        NoiDung = @"Cửa hàng sẽ miễn phí giao hàng cho tất cả các đơn hàng trong phạm vi nội thành.
                                Đối với các đơn hàng ở phạm vi ngoài thành phố thì sẽ được tính phí vận chuyển.
                                Thời gian nhận hàng sẽ từ 1-5 ngày tùy vào địa điểm nhận hàng.
                                Cửa hàng sẽ lựa chọn đối tác vận chuyển uy tín để đảm bảo laptop được giao đến khách hàng một cách an toàn và đúng thời gian.
                                Trong quá trình vận chuyển, nếu sản phẩm bị hư hỏng hoặc thất lạc, cửa hàng sẽ chịu trách nhiệm hoàn toàn và có thể gửi lại sản phẩm mới hoặc hoàn tiền cho khách hàng.
                                Chính sách miễn phí giao hàng có thể không áp dụng cho các khu vực vùng sâu, vùng xa hoặc quốc tế, và trong trường hợp này, khách hàng sẽ được thông báo rõ ràng về các chi phí phát sinh."
                    },
                    new ChinhSach
                    {
                        TieuDe = "Cam kết chính hãng",
                        NoiDung = @"Cửa hàng cam kết tất cả laptop bán ra đều là hàng chính hãng, được nhập khẩu hoặc phân phối trực tiếp từ nhà sản xuất hoặc đại lý ủy quyền.
                                Mỗi sản phẩm sẽ đi kèm với các giấy tờ chứng nhận chính hãng, bao gồm sổ bảo hành, hóa đơn mua hàng, và các giấy tờ liên quan khác.
                                Laptop mua tại cửa hàng sẽ được bảo hành theo tiêu chuẩn của nhà sản xuất. Thời gian bảo hành và các dịch vụ đi kèm sẽ được thực hiện tại các trung tâm bảo hành ủy quyền.
                                Nếu khách hàng chứng minh được sản phẩm là hàng giả, cửa hàng cam kết hoàn trả toàn bộ số tiền đã thanh toán và có thể bồi thường thêm tùy theo chính sách cụ thể.
                                Cửa hàng sẽ cung cấp dịch vụ hậu mãi, bao gồm sửa chữa và bảo trì laptop, với cam kết sử dụng linh kiện chính hãng.
                                Khách hàng có thể yên tâm về chất lượng sản phẩm và dịch vụ hậu mãi khi mua hàng tại cửa hàng."
                    },
                    new ChinhSach
                    {
                        TieuDe = "Đổi trả trong 7 ngày",
                        NoiDung = @"Khách hàng có thể đổi trả sản phẩm trong vòng 7 ngày kể từ ngày nhận hàng nếu sản phẩm có lỗi từ nhà sản xuất hoặc không đúng như mô tả.
                                Sản phẩm đổi trả phải còn nguyên vẹn, chưa qua sử dụng, còn đầy đủ phụ kiện và hộp đựng.
                                Cửa hàng sẽ kiểm tra sản phẩm và xác nhận lỗi trước khi tiến hành đổi trả.
                                Trong trường hợp sản phẩm không thể đổi, cửa hàng sẽ hoàn tiền 100% cho khách hàng.
                                Chi phí vận chuyển đổi trả sẽ do cửa hàng chịu nếu lỗi từ phía cửa hàng hoặc nhà sản xuất."
                    }
                );
                await _context.SaveChangesAsync();
            }

            // Seed Thuộc tính sản phẩm
            if (!_context.ThuocTinhSanPhams.Any())
            {
                var thuocTinhCPU = new ThuocTinhSanPham { TenThuocTinh = "CPU", Slug = "cpu", MoTa = "Bộ xử lý", ThuTuHienThi = 1, HienThi = true };
                var thuocTinhRAM = new ThuocTinhSanPham { TenThuocTinh = "RAM", Slug = "ram", MoTa = "Bộ nhớ RAM", ThuTuHienThi = 2, HienThi = true };
                var thuocTinhVGA = new ThuocTinhSanPham { TenThuocTinh = "VGA", Slug = "vga", MoTa = "Card đồ họa", ThuTuHienThi = 3, HienThi = true };
                var thuocTinhOCung = new ThuocTinhSanPham { TenThuocTinh = "Ổ cứng", Slug = "o-cung", MoTa = "Dung lượng lưu trữ", ThuTuHienThi = 4, HienThi = true };
                var thuocTinhManHinh = new ThuocTinhSanPham { TenThuocTinh = "Màn hình", Slug = "man-hinh", MoTa = "Kích thước màn hình", ThuTuHienThi = 5, HienThi = true };

                await _context.ThuocTinhSanPhams.AddRangeAsync(thuocTinhCPU, thuocTinhRAM, thuocTinhVGA, thuocTinhOCung, thuocTinhManHinh);
                await _context.SaveChangesAsync();

                // Seed Giá trị thuộc tính
                // CPU
                await _context.GiaTriThuocTinhs.AddRangeAsync(
                    new GiaTriThuocTinh { MaThuocTinh = thuocTinhCPU.MaThuocTinh, GiaTri = "Intel Core i3", Slug = "intel-core-i3", ThuTuHienThi = 1 },
                    new GiaTriThuocTinh { MaThuocTinh = thuocTinhCPU.MaThuocTinh, GiaTri = "Intel Core i5", Slug = "intel-core-i5", ThuTuHienThi = 2 },
                    new GiaTriThuocTinh { MaThuocTinh = thuocTinhCPU.MaThuocTinh, GiaTri = "Intel Core i7", Slug = "intel-core-i7", ThuTuHienThi = 3 },
                    new GiaTriThuocTinh { MaThuocTinh = thuocTinhCPU.MaThuocTinh, GiaTri = "AMD Ryzen 5", Slug = "amd-ryzen-5", ThuTuHienThi = 4 },
                    new GiaTriThuocTinh { MaThuocTinh = thuocTinhCPU.MaThuocTinh, GiaTri = "AMD Ryzen 7", Slug = "amd-ryzen-7", ThuTuHienThi = 5 }
                );

                // RAM
                await _context.GiaTriThuocTinhs.AddRangeAsync(
                    new GiaTriThuocTinh { MaThuocTinh = thuocTinhRAM.MaThuocTinh, GiaTri = "8GB", Slug = "8gb", ThuTuHienThi = 1 },
                    new GiaTriThuocTinh { MaThuocTinh = thuocTinhRAM.MaThuocTinh, GiaTri = "16GB", Slug = "16gb", ThuTuHienThi = 2 },
                    new GiaTriThuocTinh { MaThuocTinh = thuocTinhRAM.MaThuocTinh, GiaTri = "32GB", Slug = "32gb", ThuTuHienThi = 3 }
                );

                // VGA
                await _context.GiaTriThuocTinhs.AddRangeAsync(
                    new GiaTriThuocTinh { MaThuocTinh = thuocTinhVGA.MaThuocTinh, GiaTri = "Intel UHD Graphics", Slug = "intel-uhd", ThuTuHienThi = 1 },
                    new GiaTriThuocTinh { MaThuocTinh = thuocTinhVGA.MaThuocTinh, GiaTri = "Intel Iris Xe", Slug = "intel-iris-xe", ThuTuHienThi = 2 },
                    new GiaTriThuocTinh { MaThuocTinh = thuocTinhVGA.MaThuocTinh, GiaTri = "NVIDIA RTX 3050", Slug = "rtx-3050", ThuTuHienThi = 3 },
                    new GiaTriThuocTinh { MaThuocTinh = thuocTinhVGA.MaThuocTinh, GiaTri = "NVIDIA RTX 3060", Slug = "rtx-3060", ThuTuHienThi = 4 },
                    new GiaTriThuocTinh { MaThuocTinh = thuocTinhVGA.MaThuocTinh, GiaTri = "NVIDIA RTX 3070 Ti", Slug = "rtx-3070-ti", ThuTuHienThi = 5 }
                );

                // Ổ cứng
                await _context.GiaTriThuocTinhs.AddRangeAsync(
                    new GiaTriThuocTinh { MaThuocTinh = thuocTinhOCung.MaThuocTinh, GiaTri = "256GB SSD", Slug = "256gb-ssd", ThuTuHienThi = 1 },
                    new GiaTriThuocTinh { MaThuocTinh = thuocTinhOCung.MaThuocTinh, GiaTri = "512GB SSD", Slug = "512gb-ssd", ThuTuHienThi = 2 },
                    new GiaTriThuocTinh { MaThuocTinh = thuocTinhOCung.MaThuocTinh, GiaTri = "1TB SSD", Slug = "1tb-ssd", ThuTuHienThi = 3 }
                );

                // Màn hình
                await _context.GiaTriThuocTinhs.AddRangeAsync(
                    new GiaTriThuocTinh { MaThuocTinh = thuocTinhManHinh.MaThuocTinh, GiaTri = "14 inch", Slug = "14-inch", ThuTuHienThi = 1 },
                    new GiaTriThuocTinh { MaThuocTinh = thuocTinhManHinh.MaThuocTinh, GiaTri = "15.6 inch", Slug = "15-6-inch", ThuTuHienThi = 2 }
                );

                await _context.SaveChangesAsync();
            }
        }
    }
}
