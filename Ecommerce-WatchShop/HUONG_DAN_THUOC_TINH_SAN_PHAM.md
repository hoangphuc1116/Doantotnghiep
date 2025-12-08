# Hệ Thống Thuộc Tính Sản Phẩm (Product Attributes)

## 📋 Tổng Quan

Hệ thống thuộc tính sản phẩm cho phép:
- Admin quản lý các thuộc tính (CPU, RAM, VGA, Ổ cứng, Màn hình...)
- Admin quản lý giá trị của từng thuộc tính (Intel i5, 8GB, RTX 3060...)
- Gán thuộc tính cho sản phẩm
- Khách hàng lọc sản phẩm theo thuộc tính

---

## 🗂️ Cấu Trúc Database

### 1. Bảng ThuocTinhSanPham (Product Attributes)
```
MaThuocTinh (PK)
TenThuocTinh (nvarchar) - VD: "CPU", "RAM", "VGA"
Slug (varchar)
MoTa (nvarchar)
ThuTuHienThi (int)
HienThi (bool)
```

### 2. Bảng GiaTriThuocTinh (Attribute Values)
```
MaGiaTri (PK)
MaThuocTinh (FK)
GiaTri (nvarchar) - VD: "Intel Core i5", "8GB", "RTX 3060"
Slug (varchar)
ThuTuHienThi (int)
```

### 3. Bảng SanPhamThuocTinh (Product-Attribute Mapping)
```
Id (PK)
MaSanPham (FK)
MaGiaTri (FK)
```

---

## 📊 Mối Quan Hệ

```
ThuocTinhSanPham (1) ----< (N) GiaTriThuocTinh
                                      |
                                      | (N)
                                      |
                                      v
                              SanPhamThuocTinh
                                      |
                                      | (N)
                                      v
                                  SanPham (1)
```

---

## 🎯 Dữ Liệu Mẫu

### Thuộc Tính:
1. **CPU** - Bộ xử lý
   - Intel Core i3
   - Intel Core i5
   - Intel Core i7
   - AMD Ryzen 5
   - AMD Ryzen 7

2. **RAM** - Bộ nhớ RAM
   - 8GB
   - 16GB
   - 32GB

3. **VGA** - Card đồ họa
   - Intel UHD Graphics
   - Intel Iris Xe
   - NVIDIA RTX 3050
   - NVIDIA RTX 3060
   - NVIDIA RTX 3070 Ti

4. **Ổ cứng** - Dung lượng lưu trữ
   - 256GB SSD
   - 512GB SSD
   - 1TB SSD

5. **Màn hình** - Kích thước màn hình
   - 14 inch
   - 15.6 inch

---

## 🚀 Cập Nhật Database

### Bước 1: Tạo Migration
```bash
dotnet ef migrations add AddProductAttributes
```

### Bước 2: Cập Nhật Database
```bash
dotnet ef database update
```

### Bước 3: Chạy Ứng Dụng
```bash
dotnet run
```

---

## 🔧 Các Bước Tiếp Theo

### 1. Tạo Admin Controller
- `Areas/Admin/Controllers/AttributeController.cs`
- Quản lý thuộc tính
- Quản lý giá trị thuộc tính

### 2. Tạo Admin Views
- `Areas/Admin/Views/Attribute/Index.cshtml`
- Danh sách thuộc tính
- Form thêm/sửa thuộc tính
- Quản lý giá trị thuộc tính

### 3. Cập Nhật Product Controller
- Thêm chức năng gán thuộc tính cho sản phẩm
- Lọc sản phẩm theo thuộc tính

### 4. Cập Nhật Product Views
- Thêm filter thuộc tính vào sidebar
- Hiển thị thuộc tính trên trang chi tiết sản phẩm

---

## 💡 Ví Dụ Sử Dụng

### Gán Thuộc Tính Cho Sản Phẩm
```csharp
// Laptop Dell Inspiron 15
// CPU: Intel Core i5
// RAM: 8GB
// VGA: Intel UHD Graphics
// Ổ cứng: 256GB SSD
// Màn hình: 15.6 inch

var product = _context.SanPhams.Find(2); // Dell Inspiron 15
var attributes = new List<int> { 2, 6, 11, 16, 20 }; // IDs của giá trị thuộc tính

foreach (var attrId in attributes)
{
    _context.SanPhamThuocTinhs.Add(new SanPhamThuocTinh
    {
        MaSanPham = product.MaSanPham,
        MaGiaTri = attrId
    });
}
await _context.SaveChangesAsync();
```

### Lọc Sản Phẩm Theo Thuộc Tính
```csharp
// Lọc laptop có RAM 16GB và VGA RTX 3060
var products = _context.SanPhams
    .Where(p => p.SanPhamThuocTinhs.Any(pt => pt.MaGiaTri == 7)) // RAM 16GB
    .Where(p => p.SanPhamThuocTinhs.Any(pt => pt.MaGiaTri == 14)) // RTX 3060
    .ToList();
```

---

## 📝 TODO List

- [ ] Tạo AttributeController trong Admin
- [ ] Tạo Views quản lý thuộc tính
- [ ] Cập nhật ProductController để gán thuộc tính
- [ ] Thêm filter thuộc tính vào trang sản phẩm
- [ ] Hiển thị thuộc tính trên chi tiết sản phẩm
- [ ] Seed data gán thuộc tính cho 10 laptop mẫu

---

## 🎨 Giao Diện Dự Kiến

### Admin - Quản Lý Thuộc Tính
```
┌─────────────────────────────────────────────────┐
│ Quản Lý Thuộc Tính Sản Phẩm          [+ Thêm]  │
├─────────────────────────────────────────────────┤
│ ID │ Tên Thuộc Tính │ Số Giá Trị │ Hiển Thị │  │
├────┼────────────────┼────────────┼──────────┼──┤
│ 1  │ CPU            │ 5          │ ✓        │✏️│
│ 2  │ RAM            │ 3          │ ✓        │✏️│
│ 3  │ VGA            │ 5          │ ✓        │✏️│
└─────────────────────────────────────────────────┘
```

### Trang Sản Phẩm - Filter
```
┌─────────────────┐
│ Lọc Sản Phẩm    │
├─────────────────┤
│ CPU             │
│ ☐ Intel i3      │
│ ☐ Intel i5      │
│ ☐ Intel i7      │
│                 │
│ RAM             │
│ ☐ 8GB           │
│ ☐ 16GB          │
│ ☐ 32GB          │
│                 │
│ VGA             │
│ ☐ RTX 3050      │
│ ☐ RTX 3060      │
│ ☐ RTX 3070 Ti   │
└─────────────────┘
```

---

## ⚠️ Lưu Ý

1. **Performance**: Với nhiều thuộc tính, cần optimize query
2. **Indexing**: Thêm index cho bảng SanPhamThuocTinh
3. **Caching**: Cache danh sách thuộc tính để giảm query
4. **Validation**: Kiểm tra trùng lặp khi gán thuộc tính

---

Xem file tiếp theo để biết cách implement Admin Controller và Views!
