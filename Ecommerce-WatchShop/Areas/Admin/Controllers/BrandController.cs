using Ecommerce_WatchShop.Helper;
using Ecommerce_WatchShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_WatchShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "Admin")]
    public class BrandController : Controller
    {
        private readonly DongHoContext _context;

        public BrandController(DongHoContext context)
        {
            _context = context;
        }

        // Hien thi danh sach thuong hieu
        public async Task<IActionResult> Index(string searchQuery = "", int page = 1)
        {
            var pageSize = 5;
            var brands = _context.ThuongHieus
                .Include(b => b.DanhMuc) // Include DanhMuc de lay ten danh muc
                .AsQueryable();

            // Tim kiem
            if (!string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.ToLower().Trim();
                brands = brands.Where(b => b.TenThuongHieu != null && b.TenThuongHieu.ToLower().Contains(searchQuery));
            }

            var totalBrands = await brands.CountAsync();
            var totalPages = (int)Math.Ceiling(totalBrands / (double)pageSize);

            var result = await brands
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Lay danh sach danh muc cho form
            ViewBag.DanhMucList = await _context.DanhMucs
                .Select(d => new { d.MaDanhMuc, d.TenDanhMuc })
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchQuery = searchQuery;

            return View(result);
        }

        // Lay danh sach thuong hieu dang JSON
        [HttpGet]
        public async Task<IActionResult> GetBrands(string searchQuery = "")
        {
            var brands = _context.ThuongHieus
                .Include(b => b.DanhMuc) // Include DanhMuc
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.ToLower().Trim();
                brands = brands.Where(b => b.TenThuongHieu != null && b.TenThuongHieu.ToLower().Contains(searchQuery));
            }

            var result = await brands.Select(b => new
            {
                id = b.MaThuongHieu,
                name = b.TenThuongHieu,
                slug = b.Slug,
                description = b.MoTa,
                categoryId = b.MaDanhMuc,
                categoryName = b.DanhMuc != null ? b.DanhMuc.TenDanhMuc : "Khong co danh muc",
                status = b.TrangThai,
                productCount = b.SanPhams.Count
            }).ToListAsync();

            // Lay danh sach danh muc cho form
            var danhMucList = await _context.DanhMucs
                .Select(d => new { id = d.MaDanhMuc, name = d.TenDanhMuc })
                .ToListAsync();

            return Json(new { success = true, data = result, danhMucList = danhMucList });
        }

        // Hien thi form them thuong hieu
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.DanhMucList = await _context.DanhMucs
                .Select(d => new { d.MaDanhMuc, d.TenDanhMuc })
                .ToListAsync();
            return View();
        }

        // Xu ly them thuong hieu
        [HttpPost]
        public async Task<IActionResult> Create(ThuongHieu model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    return Json(new { success = false, message = "Du lieu khong hop le: " + string.Join(", ", errors) });
                }

                if (string.IsNullOrEmpty(model.TenThuongHieu))
                {
                    return Json(new { success = false, message = "Ten thuong hieu khong duoc de trong!" });
                }

                // Kiem tra trung ten thuong hieu
                if (await _context.ThuongHieus.AnyAsync(b => b.TenThuongHieu.ToLower() == model.TenThuongHieu.ToLower()))
                {
                    return Json(new { success = false, message = "Ten thuong hieu da ton tai!" });
                }

                
                // Kiem tra danh muc
                if (model.MaDanhMuc.HasValue && model.MaDanhMuc != 0)
                {
                    var danhMuc = await _context.DanhMucs.FindAsync(model.MaDanhMuc);
                    if (danhMuc == null)
                    {
                        return Json(new { success = false, message = "Danh muc khong ton tai!" });
                    }
                }

                // Tao slug duy nhat
                model.Slug = await SlugHelper.GenerateUniqueSlug(_context, model.TenThuongHieu, SlugHelper.EntityType.Brand, null);
                _context.ThuongHieus.Add(model);
                await _context.SaveChangesAsync();

                var danhMucName = model.MaDanhMuc.HasValue ? (await _context.DanhMucs.FindAsync(model.MaDanhMuc))?.TenDanhMuc : "Khong co danh muc";

                return Json(new
                {
                    success = true,
                    message = "Them thuong hieu thanh cong!",
                    data = new
                    {
                        id = model.MaThuongHieu,
                        name = model.TenThuongHieu,
                        slug = model.Slug,
                        description = model.MoTa,
                        categoryId = model.MaDanhMuc ?? 0,
                        categoryName = danhMucName,
                        status = model.TrangThai,
                        productCount = 0
                    }
                });
            }
            catch (DbUpdateException ex)
            {
                return Json(new { success = false, message = "Loi khi luu vao database: " + (ex.InnerException?.Message ?? ex.Message) });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Co loi xay ra: " + ex.Message });
            }
        }

        // Hien thi form sua thuong hieu
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return Json(new { success = false, message = $"ID khong hop le: {id}" });
            }
            var brand = await _context.ThuongHieus
                .Include(b => b.DanhMuc)
                .FirstOrDefaultAsync(b => b.MaThuongHieu == id);
            if (brand == null)
            {
                return Json(new { success = false, message = $"Thuong hieu khong ton tai voi ID: {id}" });
            }

            // Lay danh sach danh muc
            var danhMucList = await _context.DanhMucs
                .Select(d => new { id = d.MaDanhMuc, name = d.TenDanhMuc })
                .ToListAsync();

            return Json(new
            {
                success = true,
                data = new
                {
                    id = brand.MaThuongHieu,
                    name = brand.TenThuongHieu,
                    slug = brand.Slug,
                    description = brand.MoTa,
                    categoryId = brand.MaDanhMuc ?? 0,
                    categoryName = brand.DanhMuc != null ? brand.DanhMuc.TenDanhMuc : "Khong co danh muc",
                    status = brand.TrangThai,
                    productCount = brand.SanPhams.Count
                },
                danhMucList = danhMucList
            });
        }

        // Xu ly sua thuong hieu
        [HttpPost]
        public async Task<IActionResult> Edit(ThuongHieu model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    return Json(new { success = false, message = "Du lieu khong hop le: " + string.Join(", ", errors) });
                }

                var brand = await _context.ThuongHieus.FindAsync(model.MaThuongHieu);
                if (brand == null)
                {
                    return Json(new { success = false, message = "Thuong hieu khong ton tai!" });
                }

                // Kiem tra trung ten thuong hieu
                if (await _context.ThuongHieus.AnyAsync(b => b.TenThuongHieu.ToLower() == model.TenThuongHieu.ToLower() && b.MaThuongHieu != model.MaThuongHieu))
                {
                    return Json(new { success = false, message = "Ten thuong hieu da ton tai!" });
                }


                // Kiem tra danh muc
                if (model.MaDanhMuc.HasValue && model.MaDanhMuc != 0)
                {
                    var danhMuc = await _context.DanhMucs.FindAsync(model.MaDanhMuc);
                    if (danhMuc == null)
                    {
                        return Json(new { success = false, message = "Danh muc khong ton tai!" });
                    }
                }

                brand.TenThuongHieu = model.TenThuongHieu;
                brand.Slug = await SlugHelper.GenerateUniqueSlug(_context, model.TenThuongHieu, SlugHelper.EntityType.Brand, model.MaThuongHieu);
                brand.MoTa = model.MoTa;
                brand.MaDanhMuc = model.MaDanhMuc;
                brand.TrangThai = model.TrangThai;

                await _context.SaveChangesAsync();

                var danhMucName = model.MaDanhMuc.HasValue ? (await _context.DanhMucs.FindAsync(model.MaDanhMuc))?.TenDanhMuc : "Khong co danh muc";

                return Json(new
                {
                    success = true,
                    message = "Cap nhat thuong hieu thanh cong!",
                    data = new
                    {
                        id = brand.MaThuongHieu,
                        name = brand.TenThuongHieu,
                        slug = brand.Slug,
                        description = brand.MoTa,
                        categoryId = brand.MaDanhMuc ?? 0,
                        categoryName = danhMucName,
                        status = brand.TrangThai,
                        productCount = brand.SanPhams.Count
                    }
                });
            }
            catch (DbUpdateException ex)
            {
                return Json(new { success = false, message = "Loi khi luu vao database: " + (ex.InnerException?.Message ?? ex.Message) });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Co loi xay ra: " + ex.Message });
            }
        }

        // Xoa thuong hieu
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var brand = await _context.ThuongHieus.FindAsync(id);
                if (brand == null)
                {
                    return Json(new { success = false, message = "Thuong hieu khong ton tai!" });
                }

                if (brand.SanPhams.Any())
                {
                    return Json(new { success = false, message = "Khong the xoa vi thuong hieu dang duoc su dung!" });
                }

                _context.ThuongHieus.Remove(brand);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Xoa thuong hieu thanh cong!" });
            }
            catch (DbUpdateException ex)
            {
                return Json(new { success = false, message = "Loi khi xoa tu database: " + (ex.InnerException?.Message ?? ex.Message) });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Co loi xay ra: " + ex.Message });
            }
        }

        // Xem chi tiet thuong hieu
        public async Task<IActionResult> Details(int id)
        {
            var brand = await _context.ThuongHieus
                .Include(b => b.DanhMuc)
                .Include(b => b.SanPhams)
                .FirstOrDefaultAsync(b => b.MaThuongHieu == id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }
    }
}