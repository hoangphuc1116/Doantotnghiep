using Ecommerce_WatchShop.Helper;
using Ecommerce_WatchShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DongHo_Admin.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "Admin")]
    public class CategoryController : Controller
    {
        private readonly DongHoContext _context;

        public CategoryController(DongHoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.DanhMucs.ToListAsync();
            return View(categories);
        }

        // Thêm danh mục
        [HttpPost]
        public async Task<IActionResult> Create(DanhMuc model, IFormFile? hinhAnh)
        {
            Console.WriteLine($"CategoryName: {model.TenDanhMuc}, ParentId: {model.MaDanhMucCha}, Slug: {model.Slug}");

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.TenDanhMuc))
                {
                    return Json(new { success = false, message = "Tên danh mục không được để trống!" });
                }

                // Upload hình ảnh
                if (hinhAnh != null && hinhAnh.Length > 0)
                {
                    var fileName = await UploadImageHelper.UploadImageAsync(hinhAnh, "Images");
                    model.HinhAnh = "/Images/" + fileName;
                }

                model.Slug = await SlugHelper.GenerateUniqueSlug(_context, model.TenDanhMuc, SlugHelper.EntityType.Category, model.MaDanhMuc);
                _context.DanhMucs.Add(model);
                try
                {
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, data = new { id = model.MaDanhMuc, name = model.TenDanhMuc, slug = model.Slug, parentId = model.MaDanhMucCha, image = model.HinhAnh } });
                }
                catch (DbUpdateException ex)
                {
                    return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.InnerException?.Message ?? ex.Message });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
                }
            }
            return Json(new { success = false, message = "Dữ liệu không hợp lệ!" });
        }

        // Cập nhật danh mục
        [HttpPost]
        public async Task<IActionResult> Edit(DanhMuc model, IFormFile? hinhAnh)
        {
            if (ModelState.IsValid)
            {
                var category = await _context.DanhMucs.FindAsync(model.MaDanhMuc);
                if (category != null)
                {
                    category.TenDanhMuc = model.TenDanhMuc;
                    category.Slug = await SlugHelper.GenerateUniqueSlug(_context, category.TenDanhMuc, SlugHelper.EntityType.Category, model.MaDanhMuc);
                    category.MaDanhMucCha = model.MaDanhMucCha;

                    // Upload hình ảnh mới nếu có
                    if (hinhAnh != null && hinhAnh.Length > 0)
                    {
                        // Xóa hình ảnh cũ nếu có
                        if (!string.IsNullOrEmpty(category.HinhAnh))
                        {
                            var oldFileName = category.HinhAnh.Replace("/Images/", "");
                            UploadImageHelper.DeleteImage(oldFileName, "Images");
                        }

                        var fileName = await UploadImageHelper.UploadImageAsync(hinhAnh, "Images");
                        category.HinhAnh = "/Images/" + fileName;
                    }

                    _context.Update(category);
                    await _context.SaveChangesAsync();

                    return Json(new { success = true, message = "Cập nhật danh mục thành công!", data = new { id = category.MaDanhMuc, name = category.TenDanhMuc, slug = category.Slug, parentId = category.MaDanhMucCha, image = category.HinhAnh } });
                }
                return Json(new { success = false, message = "Không tìm thấy danh mục!" });
            }
            return Json(new { success = false, message = "Dữ liệu không hợp lệ!" });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var category = _context.DanhMucs.Find(id);
            if (category != null)
            {
                _context.DanhMucs.Remove(category);
                _context.SaveChanges();
                return Json(new { success = true, message = "Xóa danh mục thành công!" });
            }
            return Json(new { success = false, message = "Không tìm thấy danh mục!" });
        }

        [HttpGet]
        public IActionResult Search(string searchQuery)
        {
            var categories = _context.DanhMucs
                .Where(c => c.TenDanhMuc!.ToLower().Contains(searchQuery.ToLower()) || c.Slug!.Contains(searchQuery.ToLower()))
                .Select(c => new
                {
                    categoryId = c.MaDanhMuc,
                    categoryName = c.TenDanhMuc,
                    slug = c.Slug,
                    parentId = c.MaDanhMucCha,
                    parentName = c.MaDanhMucCha.HasValue ? _context.DanhMucs.Where(p => p.MaDanhMuc == c.MaDanhMucCha).Select(p => p.TenDanhMuc).FirstOrDefault() ?? "Không có" : "Không có",
                    image = c.HinhAnh
                })
                .ToList();

            return Json(new { success = true, data = categories });
        }
    }
}