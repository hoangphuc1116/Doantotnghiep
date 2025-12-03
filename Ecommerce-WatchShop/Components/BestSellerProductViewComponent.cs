using Ecommerce_WatchShop.Models;
using Ecommerce_WatchShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_CaFeShop.Components

{
    public class BestSellerProductViewComponent : ViewComponent
    {
        private readonly DongHoContext _context;

        public BestSellerProductViewComponent(DongHoContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Best seller dựa trên số lượng bán hàng thực tế từ ChiTietHoaDon
            var bestSellerProduct = await _context.SanPhams
                .Where(p => p.DaXoa == 0 && p.TrangThai == 1) // Chỉ sản phẩm đang hoạt động
                .Include(p => p.DanhGiaSanPhams)
                .Include(p => p.ChiTietHoaDons)
                    .ThenInclude(ct => ct.HoaDon) // Include HoaDon để kiểm tra trạng thái đơn hàng
                .Select(p => new
                {
                    Product = p,
                    // Tính tổng số lượng đã bán từ các đơn hàng đã hoàn thành
                    TotalSold = p.ChiTietHoaDons
                        .Where(ct => ct.HoaDon.TrangThai == 2) // Chỉ tính đơn hàng đã hoàn thành
                        .Sum(ct => ct.SoLuong),
                    AvgRating = p.DanhGiaSanPhams.Any()
                        ? p.DanhGiaSanPhams.Average(r => (double)r.DiemDanhGia!) : 0
                })
                .Where(x => x.TotalSold > 0) // Chỉ lấy sản phẩm đã có người mua
                .OrderByDescending(x => x.TotalSold) // Sắp xếp theo số lượng bán giảm dần
                .ThenByDescending(x => x.AvgRating) // Thứ hai là đánh giá cao
                .Take(8) // Lấy top 8 sản phẩm bán chạy nhất
                .Select(x => new ProductVM()
                {
                    Slug = x.Product.Slug,
                    ProductName = x.Product.TenSanPham,
                    Price = x.Product.Gia,
                    Image = x.Product.HinhAnh,
                    ProductRating = x.AvgRating,
                    SoldQuantity = x.TotalSold
                })
                .ToListAsync();

            ViewBag.BestSellerProduct = bestSellerProduct;
            return View();
        }
    }
}