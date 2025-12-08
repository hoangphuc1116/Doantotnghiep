# HƯỚNG DẪN IMPORT SẢN PHẨM TỪ EXCEL

## 📦 CÀI ĐẶT PACKAGE

Cần cài đặt package EPPlus để đọc file Excel:

```bash
dotnet add package EPPlus --version 7.0.0
```

## 📋 CẤU TRÚC FILE EXCEL MẪU

File Excel cần có các cột sau (Sheet đầu tiên):

| TenSanPham | MaDanhMuc | MaThuongHieu | Gia | GiaKhuyenMai | SoLuong | MoTaNgan | MoTa | ThongSoKyThuat |
|------------|-----------|--------------|-----|--------------|---------|----------|------|----------------|
| Laptop Dell G15 | 1 | 1 | 28990000 | 25990000 | 10 | Laptop gaming | Mô tả chi tiết | CPU: i5... |

## 🔧 TRIỂN KHAI

### 1. Thêm action Import vào ProductController
### 2. Tạo View Import.cshtml
### 3. Thêm nút Import vào Index.cshtml

## ✅ TÍNH NĂNG

- Upload file Excel (.xlsx)
- Validate dữ liệu
- Hiển thị preview trước khi import
- Import hàng loạt sản phẩm
- Báo lỗi chi tiết nếu có

## 📝 LƯU Ý

- File Excel phải đúng format
- Mã danh mục và thương hiệu phải tồn tại
- Giá phải là số dương
- Số lượng phải >= 0
