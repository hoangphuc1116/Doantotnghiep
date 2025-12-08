# Hướng Dẫn Cập Nhật Database - Laptop Shop

## 📦 Đã Cập Nhật

### 1. Thương Hiệu
- Dell
- HP
- Lenovo
- Asus

### 2. Danh Mục
- Laptop Gaming
- Laptop Văn Phòng
- Laptop Đồ Họa

### 3. Sản Phẩm
10 laptop với thông tin chi tiết (CPU, RAM, VGA, màn hình...)

### 4. Vai Trò & Tài Khoản
- **Admin**: admin / admin123
- **User**: user / user123

### 5. Nội Dung
- Blog về laptop
- Footer, Slider, Giới thiệu
- Chính sách

---

## 🚀 Cách Cập Nhật

### Cách 1: Tự động (Khuyến nghị)
```powershell
.\update-database.ps1
```

### Cách 2: Thủ công
```bash
dotnet ef database drop --force
dotnet ef migrations add UpdateToLaptopStore
dotnet ef database update
dotnet run
```

---

## 🔐 Tài Khoản Mặc Định

### Admin
- **Username**: admin
- **Password**: admin123
- **Email**: admin@laptopshop.com
- **Quyền**: Quản trị toàn bộ hệ thống

### User
- **Username**: user
- **Password**: user123
- **Email**: user@example.com
- **Quyền**: Khách hàng thông thường

⚠️ **Lưu ý**: Đổi mật khẩu ngay sau lần đăng nhập đầu tiên!

---

## 📝 Các File Quan Trọng

- `Repository/SeedData.cs` - Dữ liệu seed
- `Views/Shared/_Layout.cshtml` - Menu đã cập nhật
- `Views/Shared/_HeroSlider.cshtml` - Banner từ database
- `appsettings.json` - Database: LaptopShop

---

## ✅ Checklist

- [x] Cập nhật thương hiệu
- [x] Cập nhật danh mục
- [x] Cập nhật sản phẩm
- [x] Cập nhật menu
- [x] Cập nhật banner
- [x] Thêm vai trò
- [x] Thêm tài khoản admin/user
- [x] Sửa lỗi encoding tiếng Việt

---

## 🎯 Tiếp Theo

1. Upload hình ảnh laptop vào `wwwroot/HinhAnhs/`
2. Cập nhật database
3. Chạy ứng dụng
4. Đăng nhập và kiểm tra

---

**Database mới**: LaptopShop  
**Tất cả dữ liệu**: Laptop thay vì đồng hồ ✓
