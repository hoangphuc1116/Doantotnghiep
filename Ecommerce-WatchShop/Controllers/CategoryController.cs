using Ecommerce_WatchShop.Models;
using Ecommerce_WatchShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_WatchShop.Controllers
{

    public class CategoryController : Controller
    {
        private readonly DongHoContext _context;

        public CategoryController(DongHoContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string slug = "")
        {
            var category = _context.DanhMucs.Where(c => c.Slug == slug).FirstOrDefault();
            if (category == null)
            {
                return RedirectToAction("Index");
            }

            var products = _context.SanPhams
                .Where(p => p.MaDanhMuc == category.MaDanhMuc)
                .Include(p => p.HinhAnhSanPhams)
                .Include(p => p.DanhGiaSanPhams);
            var result = await products.Select(p => new ProductVM
            {
                ProductId = p.MaSanPham,
                ProductName = p.TenSanPham!,
                Image = p.HinhAnhSanPhams.FirstOrDefault()!.HinhAnh ?? "",
                Price = p.Gia,
                ShortDescription = p.MoTaNgan!,
                ProductRating = p.DanhGiaSanPhams.Any()
                    ? p.DanhGiaSanPhams.Average(r => (double)r.DiemDanhGia!)
                    : 0,
                TotalRating = p.DanhGiaSanPhams.Count,
            }).ToListAsync();

            return View(result);
        }
    }
}
