
using Ecommerce_WatchShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ecommerce_WatchShop.Helper;

namespace Ecommerce_WatchShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BannerController : Controller
    {
        private readonly DongHoContext _context;

        public BannerController(DongHoContext context)
        {
            _context = context;
        }

        // GET: Admin/Banner
        public async Task<IActionResult> Index()
        {
            var banners = await _context.Sliders.OrderBy(s => s.ThuTuHienThi).ToListAsync();
            return View(banners);
        }

        // GET: Admin/Banner/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banner = await _context.Sliders.FirstOrDefaultAsync(m => m.Ma == id);
            if (banner == null)
            {
                return NotFound();
            }

            return View(banner);
        }

        // GET: Admin/Banner/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Banner/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider banner, IFormFile? hinhAnh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Upload hình ảnh
                    if (hinhAnh != null && hinhAnh.Length > 0)
                    {
                        var fileName = await UploadImageHelper.UploadImageAsync(hinhAnh, "Images");
                        banner.HinhAnh = fileName;
                    }

                    _context.Add(banner);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Thêm banner thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Có lỗi xảy ra: " + ex.Message;
                }
            }
            return View(banner);
        }

        // GET: Admin/Banner/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banner = await _context.Sliders.FindAsync(id);
            if (banner == null)
            {
                return NotFound();
            }
            return View(banner);
        }

        // POST: Admin/Banner/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Slider banner, IFormFile? hinhAnh)
        {
            if (id != banner.Ma)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingBanner = await _context.Sliders.FindAsync(id);
                    if (existingBanner == null)
                    {
                        return NotFound();
                    }

                    // Cập nhật thông tin
                    existingBanner.TieuDe = banner.TieuDe;
                    existingBanner.MoTa = banner.MoTa;
                    existingBanner.Link = banner.Link;
                    existingBanner.ThuTuHienThi = banner.ThuTuHienThi;
                    existingBanner.TrangThai = banner.TrangThai;
                    existingBanner.HienThiTrangChu = banner.HienThiTrangChu;
                    existingBanner.HienThiTrangSanPham = banner.HienThiTrangSanPham;

                    // Upload hình ảnh mới nếu có
                    if (hinhAnh != null && hinhAnh.Length > 0)
                    {
                        // Xóa hình ảnh cũ
                        if (!string.IsNullOrEmpty(existingBanner.HinhAnh))
                        {
                            UploadImageHelper.DeleteImage(existingBanner.HinhAnh, "Images");
                        }

                        var fileName = await UploadImageHelper.UploadImageAsync(hinhAnh, "Images");
                        existingBanner.HinhAnh = fileName;
                    }

                    _context.Update(existingBanner);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật banner thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BannerExists(banner.Ma))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Có lỗi xảy ra: " + ex.Message;
                }
            }
            return View(banner);
        }

        // POST: Admin/Banner/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var banner = await _context.Sliders.FindAsync(id);
                if (banner != null)
                {
                    // Xóa hình ảnh
                    if (!string.IsNullOrEmpty(banner.HinhAnh))
                    {
                        UploadImageHelper.DeleteImage(banner.HinhAnh, "Images");
                    }

                    _context.Sliders.Remove(banner);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Xóa banner thành công!";
                }
                else
                {
                    TempData["Error"] = "Không tìm thấy banner!";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Có lỗi xảy ra: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Admin/Banner/ToggleStatus/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            try
            {
                var banner = await _context.Sliders.FindAsync(id);
                if (banner != null)
                {
                    banner.TrangThai = !banner.TrangThai;
                    _context.Update(banner);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật trạng thái thành công!";
                }
                else
                {
                    TempData["Error"] = "Không tìm thấy banner!";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Có lỗi xảy ra: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BannerExists(int id)
        {
            return _context.Sliders.Any(e => e.Ma == id);
        }
    }
}
