using Ecommerce_WatchShop.Helper;
using Ecommerce_WatchShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_WatchShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "Admin")]
    public class AttributeController : Controller
    {
        private readonly DongHoContext _context;

        public AttributeController(DongHoContext context)
        {
            _context = context;
        }

        // GET: Admin/Attribute
        public async Task<IActionResult> Index()
        {
            var attributes = await _context.ThuocTinhSanPhams
                .Include(a => a.GiaTriThuocTinhs)
                .OrderBy(a => a.ThuTuHienThi)
                .ToListAsync();
            return View(attributes);
        }

        // POST: Admin/Attribute/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ThuocTinhSanPham model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.TenThuocTinh))
                {
                    return Json(new { success = false, message = "Tên thuộc tính không được để trống!" });
                }

                model.Slug = await SlugHelper.GenerateUniqueSlug(_context, model.TenThuocTinh, SlugHelper.EntityType.Attribute, 0);
                _context.ThuocTinhSanPhams.Add(model);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Thêm thuộc tính thành công!", data = new { id = model.MaThuocTinh, name = model.TenThuocTinh, slug = model.Slug } });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        // POST: Admin/Attribute/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ThuocTinhSanPham model)
        {
            try
            {
                var attribute = await _context.ThuocTinhSanPhams.FindAsync(model.MaThuocTinh);
                if (attribute == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy thuộc tính!" });
                }

                attribute.TenThuocTinh = model.TenThuocTinh;
                attribute.Slug = await SlugHelper.GenerateUniqueSlug(_context, model.TenThuocTinh, SlugHelper.EntityType.Attribute, model.MaThuocTinh);
                attribute.MoTa = model.MoTa;
                attribute.ThuTuHienThi = model.ThuTuHienThi;
                attribute.HienThi = model.HienThi;

                _context.Update(attribute);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Cập nhật thuộc tính thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        // POST: Admin/Attribute/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var attribute = await _context.ThuocTinhSanPhams
                    .Include(a => a.GiaTriThuocTinhs)
                    .FirstOrDefaultAsync(a => a.MaThuocTinh == id);

                if (attribute == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy thuộc tính!" });
                }

                // Xóa tất cả giá trị thuộc tính
                _context.GiaTriThuocTinhs.RemoveRange(attribute.GiaTriThuocTinhs);
                _context.ThuocTinhSanPhams.Remove(attribute);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Xóa thuộc tính thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        // GET: Admin/Attribute/Values/{id}
        public async Task<IActionResult> Values(int id)
        {
            var attribute = await _context.ThuocTinhSanPhams
                .Include(a => a.GiaTriThuocTinhs.OrderBy(v => v.ThuTuHienThi))
                .FirstOrDefaultAsync(a => a.MaThuocTinh == id);

            if (attribute == null)
            {
                return NotFound();
            }

            return View(attribute);
        }

        // POST: Admin/Attribute/CreateValue
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateValue(GiaTriThuocTinh model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.GiaTri))
                {
                    return Json(new { success = false, message = "Giá trị không được để trống!" });
                }

                model.Slug = await SlugHelper.GenerateUniqueSlug(_context, model.GiaTri, SlugHelper.EntityType.AttributeValue, 0);
                _context.GiaTriThuocTinhs.Add(model);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Thêm giá trị thành công!", data = new { id = model.MaGiaTri, value = model.GiaTri, slug = model.Slug } });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        // POST: Admin/Attribute/EditValue
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditValue(GiaTriThuocTinh model)
        {
            try
            {
                var value = await _context.GiaTriThuocTinhs.FindAsync(model.MaGiaTri);
                if (value == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy giá trị!" });
                }

                value.GiaTri = model.GiaTri;
                value.Slug = await SlugHelper.GenerateUniqueSlug(_context, model.GiaTri, SlugHelper.EntityType.AttributeValue, model.MaGiaTri);
                value.ThuTuHienThi = model.ThuTuHienThi;

                _context.Update(value);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Cập nhật giá trị thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        // POST: Admin/Attribute/DeleteValue
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteValue(int id)
        {
            try
            {
                var value = await _context.GiaTriThuocTinhs.FindAsync(id);
                if (value == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy giá trị!" });
                }

                _context.GiaTriThuocTinhs.Remove(value);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Xóa giá trị thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }
    }
}
