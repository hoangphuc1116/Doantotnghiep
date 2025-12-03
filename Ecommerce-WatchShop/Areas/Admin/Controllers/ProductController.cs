using Ecommerce_WatchShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ecommerce_WatchShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "Admin")]
    public class ProductController : Controller
    {
        private readonly DongHoContext _context;

        public ProductController(DongHoContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách sản phẩm
        public async Task<IActionResult> Index()
        {
            var products = await _context.SanPhams.Include(p => p.ThuongHieu).Include(p => p.DanhMuc).ToListAsync();
            return View(products);
        }

        // Hiển thị form thêm sản phẩm
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.ThuongHieuId = new SelectList(await _context.ThuongHieus.ToListAsync(), "MaThuongHieu", "TenThuongHieu");
            ViewBag.DanhMucId = new SelectList(await _context.DanhMucs.ToListAsync(), "MaDanhMuc", "TenDanhMuc");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SanPham product, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                TempData["error"] = "Dữ liệu không hợp lệ: " + string.Join("; ", errors);
                ViewBag.ThuongHieuId = new SelectList(await _context.ThuongHieus.ToListAsync(), "MaThuongHieu", "TenThuongHieu", product.MaThuongHieu);
                ViewBag.DanhMucId = new SelectList(await _context.DanhMucs.ToListAsync(), "MaDanhMuc", "TenDanhMuc", product.MaDanhMuc);
                return View(product);
            }

            // Kiểm tra MaDanhMuc và MaThuongHieu có hợp lệ không
            if (product.MaDanhMuc == null || product.MaThuongHieu == null || string.IsNullOrEmpty(product.GioiTinh) || string.IsNullOrEmpty(product.MoTaNgan) || string.IsNullOrEmpty(product.ThongSoKyThuat))
            {
                TempData["error"] = "Vui lòng điền đầy đủ thông tin (danh mục, thương hiệu, giới tính, mô tả ngắn, thông số kỹ thuật).";
                ViewBag.ThuongHieuId = new SelectList(await _context.ThuongHieus.ToListAsync(), "MaThuongHieu", "TenThuongHieu", product.MaThuongHieu);
                ViewBag.DanhMucId = new SelectList(await _context.DanhMucs.ToListAsync(), "MaDanhMuc", "TenDanhMuc", product.MaDanhMuc);
                return View(product);
            }

            try
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    if (imageFile.Length > 5 * 1024 * 1024)
                    {
                        TempData["error"] = "Kích thước file ảnh không được vượt quá 5MB.";
                        ViewBag.ThuongHieuId = new SelectList(await _context.ThuongHieus.ToListAsync(), "MaThuongHieu", "TenThuongHieu", product.MaThuongHieu);
                        ViewBag.DanhMucId = new SelectList(await _context.DanhMucs.ToListAsync(), "MaDanhMuc", "TenDanhMuc", product.MaDanhMuc);
                        return View(product);
                    }

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    product.HinhAnh = "/images/" + fileName;
                }

                // Tính phần trăm khuyến mãi nếu có giá khuyến mãi
                if (product.GiaKhuyenMai.HasValue && product.GiaKhuyenMai > 0 && product.GiaKhuyenMai < product.Gia)
                {
                    product.PhanTramKhuyenMai = Math.Round((decimal)(((product.Gia - product.GiaKhuyenMai.Value) / product.Gia) * 100), 2);
                }
                else
                {
                    product.GiaKhuyenMai = null;
                    product.PhanTramKhuyenMai = null;
                }

                product.Slug = GenerateUniqueSlug(product.TenSanPham);
                product.NgayTao = DateTime.Now;
                product.NgayCapNhat = DateTime.Now;
                product.TrangThai = 1;
                product.DaXoa = 0;

                _context.Add(product);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    TempData["success"] = "Thêm sản phẩm thành công!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = "Không có dữ liệu nào được lưu vào database.";
                }
            }
            catch (DbUpdateException ex)
            {
                TempData["error"] = $"Lỗi khi thêm sản phẩm: {ex.InnerException?.Message ?? ex.Message}";
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Lỗi khi thêm sản phẩm: {ex.Message}";
            }

            ViewBag.ThuongHieuId = new SelectList(await _context.ThuongHieus.ToListAsync(), "MaThuongHieu", "TenThuongHieu", product.MaThuongHieu);
            ViewBag.DanhMucId = new SelectList(await _context.DanhMucs.ToListAsync(), "MaDanhMuc", "TenDanhMuc", product.MaDanhMuc);
            return View(product);
        }

        // Hiển thị form cập nhật sản phẩm
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.SanPhams.FindAsync(id);
            if (product == null)
            {
                TempData["error"] = "Không tìm thấy sản phẩm";
                return RedirectToAction("Index");
            }

            ViewBag.ThuongHieuId = new SelectList(await _context.ThuongHieus.ToListAsync(), "MaThuongHieu", "TenThuongHieu", product.MaThuongHieu);
            ViewBag.DanhMucId = new SelectList(await _context.DanhMucs.ToListAsync(), "MaDanhMuc", "TenDanhMuc", product.MaDanhMuc);
            ViewBag.GioiTinhList = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Value = "Nam", Text = "Nam" },
                new SelectListItem { Value = "Nữ", Text = "Nữ" },
                new SelectListItem { Value = "Unisex", Text = "Unisex" }
            }, "Value", "Text", product.GioiTinh);
            return View(product);
        }

        // Xử lý cập nhật sản phẩm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SanPham product, IFormFile? imageFile)
        {
            if (id != product.MaSanPham)
            {
                TempData["error"] = "ID sản phẩm không hợp lệ";
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ThuongHieuId = new SelectList(await _context.ThuongHieus.ToListAsync(), "MaThuongHieu", "TenThuongHieu", product.MaThuongHieu);
                ViewBag.DanhMucId = new SelectList(await _context.DanhMucs.ToListAsync(), "MaDanhMuc", "TenDanhMuc", product.MaDanhMuc);
                return View(product);
            }

            try
            {
                var existingProduct = await _context.SanPhams.FindAsync(id);
                if (existingProduct == null)
                {
                    TempData["error"] = "Không tìm thấy sản phẩm";
                    return RedirectToAction("Index");
                }

                existingProduct.TenSanPham = product.TenSanPham;
                existingProduct.Gia = product.Gia;
                existingProduct.MoTa = product.MoTa;
                existingProduct.SoLuong = product.SoLuong;
                existingProduct.MaThuongHieu = product.MaThuongHieu;
                existingProduct.MaDanhMuc = product.MaDanhMuc;
                existingProduct.ThongSoKyThuat = product.ThongSoKyThuat;
                existingProduct.GioiTinh = product.GioiTinh;
                existingProduct.MoTaNgan = product.MoTaNgan;
                existingProduct.PhanTramKhuyenMai = product.PhanTramKhuyenMai;

                // Tính phần trăm khuyến mãi nếu có giá khuyến mãi
                if (product.GiaKhuyenMai.HasValue && product.GiaKhuyenMai > 0 && product.GiaKhuyenMai < product.Gia)
                {
                    existingProduct.GiaKhuyenMai = product.GiaKhuyenMai;
                    existingProduct.PhanTramKhuyenMai = Math.Round((decimal)(((product.Gia - product.GiaKhuyenMai.Value) / product.Gia) * 100), 2);
                }
                else
                {
                    existingProduct.GiaKhuyenMai = null;
                    existingProduct.PhanTramKhuyenMai = null;
                }

                // Tạo hoặc cập nhật Slug nếu chưa có hoặc tên thay đổi
                if (string.IsNullOrEmpty(existingProduct.Slug) || (existingProduct.TenSanPham != product.TenSanPham))
                {
                    existingProduct.Slug = GenerateUniqueSlug(product.TenSanPham);
                }

                if (imageFile != null && imageFile.Length > 0)
                {
                    if (imageFile.Length > 5 * 1024 * 1024)
                    {
                        TempData["error"] = "Kích thước file ảnh không được vượt quá 5MB.";
                        ViewBag.ThuongHieuId = new SelectList(await _context.ThuongHieus.ToListAsync(), "MaThuongHieu", "TenThuongHieu", product.MaThuongHieu);
                        ViewBag.DanhMucId = new SelectList(await _context.DanhMucs.ToListAsync(), "MaDanhMuc", "TenDanhMuc", product.MaDanhMuc);
                        return View(product);
                    }

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    existingProduct.HinhAnh = "/images/" + fileName;
                }

                existingProduct.NgayCapNhat = DateTime.Now;

                _context.Update(existingProduct);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    TempData["success"] = "Cập nhật sản phẩm thành công!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "Không có thay đổi nào được lưu vào cơ sở dữ liệu.";
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                TempData["error"] = $"Lỗi cập nhật dữ liệu: {ex.Message}";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Đã xảy ra lỗi: {ex.Message}";
                return RedirectToAction("Index");
            }

            ViewBag.ThuongHieuId = new SelectList(await _context.ThuongHieus.ToListAsync(), "MaThuongHieu", "TenThuongHieu", product.MaThuongHieu);
            ViewBag.DanhMucId = new SelectList(await _context.DanhMucs.ToListAsync(), "MaDanhMuc", "TenDanhMuc", product.MaDanhMuc);
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> HideProduct(int id)
        {
            // Tìm sản phẩm theo ID
            var product = await _context.SanPhams.FindAsync(id);

            if (product == null)
            {
                TempData["error"] = "Không tìm thấy sản phẩm";
                return RedirectToAction("Index");
            }

            // Đánh dấu sản phẩm là đã ẩn (bằng cách đặt Deleted = 1 hoặc Status = 0)
            product.TrangThai = 0; // Hoặc sử dụng product.Deleted = 1;

            // Cập nhật thông tin sản phẩm
            product.NgayCapNhat = DateTime.Now;
            _context.Update(product);

            await _context.SaveChangesAsync();

            TempData["success"] = "Sản phẩm đã bị ẩn!";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> ShowProduct(int id)
        {
            // Tìm sản phẩm theo ID
            var product = await _context.SanPhams.FindAsync(id);

            if (product == null)
            {
                TempData["error"] = "Không tìm thấy sản phẩm";
                return RedirectToAction("Index");
            }

            // Đánh dấu sản phẩm là hiển thị lại (thay đổi Status hoặc Deleted)
            product.TrangThai = 1; // Hoặc product.Deleted = 0;

            // Cập nhật thông tin sản phẩm
            product.NgayCapNhat = DateTime.Now;
            _context.Update(product);

            await _context.SaveChangesAsync();

            TempData["success"] = "Sản phẩm đã hiển thị lại!";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            // Tìm sản phẩm theo ID
            var product = await _context.SanPhams.FindAsync(id);

            if (product == null)
            {
                TempData["error"] = "Không tìm thấy sản phẩm";
                return RedirectToAction("Index");
            }

            try
            {
                // Xóa sản phẩm khỏi cơ sở dữ liệu
                _context.SanPhams.Remove(product);
                await _context.SaveChangesAsync();

                TempData["success"] = "Sản phẩm đã được xóa!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Lỗi khi xóa sản phẩm: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
        private string GenerateUniqueSlug(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            string baseSlug = GenerateSlug(text);
            string slug = baseSlug;
            int counter = 1;

            // Kiểm tra trùng lặp và thêm số đếm nếu cần
            while (_context.SanPhams.Any(p => p.Slug == slug && p.MaSanPham != 0)) // Tránh trùng với chính nó
            {
                slug = $"{baseSlug}-{counter++}";
            }

            return slug;
        }
        private string GenerateSlug(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            string str = text.ToLower();
            // Loại bỏ dấu tiếng Việt
            str = Regex.Replace(str, "[áàạảãâấầậẩẫăắằặẳẵ]", "a");
            str = Regex.Replace(str, "[éèẹẻẽêếềệểễ]", "e");
            str = Regex.Replace(str, "[íìịỉĩ]", "i");
            str = Regex.Replace(str, "[óòọỏõôốồộổỗơớờợởỡ]", "o");
            str = Regex.Replace(str, "[úùụủũưứừựửữ]", "u");
            str = Regex.Replace(str, "[ýỳỵỷỹ]", "y");
            str = Regex.Replace(str, "[đ]", "d");
            // Thay khoảng trắng và ký tự đặc biệt bằng dấu gạch ngang
            str = Regex.Replace(str, "[^a-z0-9-]", "-");
            str = Regex.Replace(str, "[-]+", "-");
            str = str.Trim('-');
            return str;
        }
    }
}