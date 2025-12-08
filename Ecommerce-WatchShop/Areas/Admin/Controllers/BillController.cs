using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ecommerce_WatchShop.Models;

namespace Ecommerce_WatchShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "Admin")]
    public class BillController : Controller
    {
        private readonly DongHoContext _context;

        public BillController(DongHoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var bills = await _context.HoaDons
                .Include(h => h.KhachHang)
                .OrderByDescending(h => h.NgayDatHang)
                .ToListAsync();
            return View(bills);
        }

        public async Task<IActionResult> Details(int id)
        {
            var bill = await _context.HoaDons
                .Include(h => h.KhachHang)
                .Include(h => h.ChiTietHoaDons)
                    .ThenInclude(ct => ct.SanPham)
                .FirstOrDefaultAsync(h => h.MaHoaDon == id);

            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, int status)
        {
            var bill = await _context.HoaDons.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }

            // Kiểm tra trạng thái hợp lệ
            if (status < 0 || status > 4)
            {
                TempData["error"] = "Trạng thái không hợp lệ";
                return RedirectToAction("Details", new { id = id });
            }

            var oldStatus = bill.TrangThai;
            bill.TrangThai = status;

            // Nếu chuyển sang trạng thái Đã hủy (4), hoàn trả số lượng sản phẩm
            if (status == 4 && oldStatus != 4)
            {
                var orderDetails = await _context.ChiTietHoaDons
                    .Where(ct => ct.MaHoaDon == id)
                    .ToListAsync();

                foreach (var detail in orderDetails)
                {
                    var product = await _context.SanPhams.FindAsync(detail.MaSanPham);
                    if (product != null)
                    {
                        product.SoLuong += detail.SoLuong; // Hoàn trả số lượng
                        _context.SanPhams.Update(product);
                    }
                }

                // Lưu lý do hủy nếu chưa có
                if (string.IsNullOrEmpty(bill.YeuCauHuy))
                {
                    bill.YeuCauHuy = "Admin hủy đơn hàng";
                    bill.NgayYeuCauHuy = DateTime.Now;
                    bill.DaYeuCauHuy = true;
                }
            }

            await _context.SaveChangesAsync();

            string statusText = status switch
            {
                0 => "Chờ xác nhận",
                1 => "Đã xác nhận",
                2 => "Đang giao",
                3 => "Hoàn thành",
                4 => "Đã hủy",
                _ => "Không xác định"
            };

            TempData["success"] = $"Cập nhật trạng thái đơn hàng thành công: {statusText}";
            return RedirectToAction("Details", new { id = id });
        }

        [HttpPost]
        public async Task<IActionResult> ProcessCancelRequest(int id, bool approve)
        {
            var bill = await _context.HoaDons.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }

            if (!bill.DaYeuCauHuy)
            {
                TempData["error"] = "Đơn hàng này không có yêu cầu hủy";
                return RedirectToAction("Details", new { id = id });
            }

            if (approve)
            {
                // Chấp nhận yêu cầu hủy
                bill.TrangThai = 0; // Đã hủy
                bill.DaYeuCauHuy = false; // Reset yêu cầu hủy
                TempData["success"] = "Đã chấp nhận yêu cầu hủy đơn hàng";
            }
            else
            {
                // Từ chối yêu cầu hủy
                bill.DaYeuCauHuy = false; // Reset yêu cầu hủy
                bill.YeuCauHuy = null;
                bill.NgayYeuCauHuy = null;
                TempData["success"] = "Đã từ chối yêu cầu hủy đơn hàng";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = id });
        }

        public async Task<IActionResult> CancelRequests()
        {
            var cancelRequests = await _context.HoaDons
                .Include(h => h.KhachHang)
                .Where(h => h.DaYeuCauHuy == true)
                .OrderByDescending(h => h.NgayYeuCauHuy)
                .ToListAsync();

            return View(cancelRequests);
        }
    }
}