using Ecommerce_WatchShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ecommerce_WatchShop.Helper;
using Ecommerce_WatchShop.Models.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Ecommerce_WatchShop.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly DongHoContext _context;
        private readonly VNPAYHelper _vnpayHelper;
        public List<CartRequest> Carts => CartHelper.GetCart(HttpContext.Session);

        public CheckoutController(DongHoContext context, VNPAYHelper vnpayHelper)
        {
            _context = context;
            _vnpayHelper = vnpayHelper;
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity!.IsAuthenticated)
            {
                TempData["error"] = "Vui lòng đăng nhập để thanh toán";
                return RedirectToAction("Index", "Home");
            }

            if (Carts == null || Carts.Count == 0)
            {
                TempData["error"] = "Giỏ hàng của bạn đang trống";
                return RedirectToAction("Cart", "Cart");
            }

            var checkoutVM = new CheckoutVM();
            var subtotal = Carts.Sum(item => (decimal)(item.Quantity * item.Price));
            var shippingFee = 30000;
            checkoutVM.TotalAmount = subtotal + shippingFee;

            // Lấy thông tin khách hàng
            var customerIdClaim = HttpContext.User.Claims.SingleOrDefault(c => c.Type == "MaKhachHang");
            if (customerIdClaim != null && int.TryParse(customerIdClaim.Value, out var customerId))
            {
                var customer = await _context.KhachHangs.FindAsync(customerId);
                if (customer != null)
                {
                    checkoutVM.FullName = customer.HoTen ?? "";
                    checkoutVM.Phone = customer.SoDienThoai ?? "";
                    checkoutVM.Email = customer.Email ?? "";
                    checkoutVM.Address = customer.DiaChi ?? "";
                    checkoutVM.Province = customer.Tinh?.TenTinh;
                    checkoutVM.District = customer.Huyen?.TenHuyen;
                    checkoutVM.Ward = customer.Xa?.TenXa;
                }
            }

            ViewBag.CartItems = Carts;
            return View(checkoutVM);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutVM model)
        {
            try
            {
                if (!User.Identity!.IsAuthenticated)
                {
                    TempData["error"] = "Vui lòng đăng nhập để thanh toán";
                    return RedirectToAction("Index", "Home");
                }

                if (Carts == null || Carts.Count == 0)
                {
                    TempData["error"] = "Giỏ hàng của bạn đang trống";
                    return RedirectToAction("Cart", "Cart");
                }

                // Validate model state
                if (!ModelState.IsValid)
                {
                    TempData["error"] = "Vui lòng kiểm tra lại thông tin đã nhập";
                    ViewBag.CartItems = Carts;
                    return View("Index", model);
                }

                // Kiểm tra thông tin bắt buộc
                if (string.IsNullOrWhiteSpace(model.FullName?.Trim()) ||
                    string.IsNullOrWhiteSpace(model.Phone?.Trim()) ||
                    string.IsNullOrWhiteSpace(model.Email?.Trim()) ||
                    string.IsNullOrWhiteSpace(model.Address?.Trim()))
                {
                    TempData["error"] = "Vui lòng điền đầy đủ thông tin bắt buộc";
                    ViewBag.CartItems = Carts;
                    return View("Index", model);
                }

                var customerIdClaim = HttpContext.User.Claims.SingleOrDefault(c => c.Type == "MaKhachHang");
                if (customerIdClaim == null || !int.TryParse(customerIdClaim.Value, out var customerId))
                {
                    TempData["error"] = "Phiên đăng nhập không hợp lệ";
                    return RedirectToAction("Index", "Home");
                }

                // Kiểm tra khách hàng có tồn tại
                var customer = await _context.KhachHangs.FindAsync(customerId);
                if (customer == null)
                {
                    TempData["error"] = "Không tìm thấy thông tin khách hàng";
                    return RedirectToAction("Index", "Home");
                }

                // Kiểm tra sản phẩm trong giỏ hàng có tồn tại
                foreach (var cartItem in Carts)
                {
                    var product = await _context.SanPhams.FindAsync(cartItem.ProductId);
                    if (product == null)
                    {
                        TempData["error"] = $"Sản phẩm {cartItem.ProductName} không còn tồn tại";
                        ViewBag.CartItems = Carts;
                        return View("Index", model);
                    }
                }

                // Tính tổng tiền
                var subtotal = Carts.Sum(item => (decimal)(item.Quantity * item.Price));
                var shippingFee = 30000m;
                var totalAmount = subtotal + shippingFee;

                // Sử dụng transaction để đảm bảo tính toàn vẹn dữ liệu
                using var transaction = await _context.Database.BeginTransactionAsync();

                try
                {
                    // Tạo hóa đơn
                    var hoaDon = new HoaDon
                    {
                        MaKhachHang = customerId,
                        NgayDatHang = DateTime.Now,
                        HoTen = model.FullName.Trim(),
                        SoDienThoai = model.Phone.Trim(),
                        Email = model.Email.Trim(),
                        DiaChi = model.Address.Trim(),
                        Tinh = model.Province?.Trim() ?? "",
                        Huyen = model.District?.Trim() ?? "",
                        Xa = model.Ward?.Trim() ?? "",
                        PhuongThucThanhToan = model.PaymentMethod,
                        TongTien = totalAmount,
                        TrangThai = 0 // Chờ xác nhận
                    };

                    _context.HoaDons.Add(hoaDon);
                    await _context.SaveChangesAsync();

                    // Tạo chi tiết hóa đơn và trừ số lượng sản phẩm
                    foreach (var cartItem in Carts)
                    {
                        var chiTiet = new ChiTietHoaDon
                        {
                            MaHoaDon = hoaDon.MaHoaDon,
                            MaSanPham = cartItem.ProductId,
                            SoLuong = cartItem.Quantity,
                            Gia = (decimal)cartItem.Price,
                            TongTien = (decimal)(cartItem.Quantity * cartItem.Price)
                        };
                        _context.ChiTietHoaDons.Add(chiTiet);

                        // Trừ số lượng sản phẩm trong kho
                        var product = await _context.SanPhams.FindAsync(cartItem.ProductId);
                        if (product != null)
                        {
                            product.SoLuong -= cartItem.Quantity;
                            if (product.SoLuong < 0) product.SoLuong = 0; // Đảm bảo không âm
                            _context.SanPhams.Update(product);
                        }
                    }

                    await _context.SaveChangesAsync();

                    // Xử lý thanh toán VNPAY
                    if (model.PaymentMethod == "VNPAY")
                    {
                        string ipAddress = GetClientIpAddress();
                        string paymentUrl = _vnpayHelper.CreatePaymentUrl(hoaDon, ipAddress);
                        await transaction.CommitAsync();
                        return Redirect(paymentUrl);
                    }

                    // Thanh toán COD
                    await transaction.CommitAsync();

                    // Xóa giỏ hàng
                    CartHelper.ClearCart(HttpContext.Session);

                    TempData["success"] = "Đặt hàng thành công! Mã đơn hàng: #DH" + hoaDon.MaHoaDon.ToString("D6");
                    return RedirectToAction("OrderSuccess", new { orderId = hoaDon.MaHoaDon });
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException dbEx)
            {
                // Log chi tiết lỗi database
                var innerException = dbEx.InnerException?.Message ?? dbEx.Message;
                TempData["error"] = $"Lỗi cơ sở dữ liệu: {innerException}";
                ViewBag.CartItems = Carts;
                return View("Index", model);
            }
            catch (Exception ex)
            {
                // Log chi tiết lỗi
                TempData["error"] = $"Có lỗi xảy ra: {ex.Message}";
                ViewBag.CartItems = Carts;
                return View("Index", model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> VNPAYReturn()
        {
            try
            {
                var vnpayConfig = HttpContext.RequestServices.GetService<IConfiguration>().GetSection("VNPAY");
                string hashSecret = vnpayConfig["HashSecret"];

                // Lấy các tham số từ query string
                var vnp_Params = new Dictionary<string, string>();
                foreach (var key in Request.Query.Keys)
                {
                    vnp_Params[key] = Request.Query[key];
                }

                // Xác minh chữ ký
                bool isValidSignature = _vnpayHelper.ValidateSignature(vnp_Params, hashSecret);
                if (!isValidSignature)
                {
                    TempData["error"] = "Chữ ký không hợp lệ. Giao dịch không được xác nhận.";
                    return RedirectToAction("Index", "Home");
                }

                // Kiểm tra mã giao dịch
                string vnp_TxnRef = vnp_Params["vnp_TxnRef"];
                if (!int.TryParse(vnp_TxnRef, out var orderId))
                {
                    TempData["error"] = "Mã đơn hàng không hợp lệ.";
                    return RedirectToAction("Index", "Home");
                }

                var order = await _context.HoaDons
                    .FirstOrDefaultAsync(h => h.MaHoaDon == orderId && h.PhuongThucThanhToan == "VNPAY");

                if (order == null)
                {
                    TempData["error"] = "Không tìm thấy đơn hàng.";
                    return RedirectToAction("Index", "Home");
                }

                // Kiểm tra trạng thái thanh toán
                string vnp_ResponseCode = vnp_Params["vnp_ResponseCode"];
                if (vnp_ResponseCode == "00")
                {
                    // Thanh toán thành công - Đơn hàng vẫn ở trạng thái Chờ xác nhận
                    // Admin sẽ xác nhận đơn hàng sau
                    order.TrangThai = 0; // Chờ xác nhận
                    await _context.SaveChangesAsync();

                    // Xóa giỏ hàng
                    CartHelper.ClearCart(HttpContext.Session);

                    TempData["success"] = $"Thanh toán thành công! Mã đơn hàng: #DH{order.MaHoaDon.ToString("D6")}";
                    return RedirectToAction("OrderSuccess", new { orderId = order.MaHoaDon });
                }
                else
                {
                    // Thanh toán thất bại - Hủy đơn hàng
                    order.TrangThai = 4; // Đã hủy
                    order.YeuCauHuy = $"Thanh toán thất bại. Mã lỗi: {vnp_ResponseCode}";
                    order.NgayYeuCauHuy = DateTime.Now;
                    order.DaYeuCauHuy = true;
                    await _context.SaveChangesAsync();

                    TempData["error"] = $"Thanh toán thất bại. Mã lỗi: {vnp_ResponseCode}";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Lỗi xử lý kết quả thanh toán: {ex.Message}";
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> OrderSuccess(int orderId)
        {
            if (!User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var customerIdClaim = HttpContext.User.Claims.SingleOrDefault(c => c.Type == "MaKhachHang");
            if (customerIdClaim == null || !int.TryParse(customerIdClaim.Value, out var customerId))
            {
                return RedirectToAction("Index", "Home");
            }

            var order = await _context.HoaDons
                .Include(h => h.ChiTietHoaDons)
                    .ThenInclude(ct => ct.SanPham)
                .FirstOrDefaultAsync(h => h.MaHoaDon == orderId && h.MaKhachHang == customerId);

            if (order == null)
            {
                TempData["error"] = "Không tìm thấy đơn hàng";
                return RedirectToAction("Index", "Home");
            }

            // Kiểm tra xem đơn hàng có thể hủy được không (trong vòng 1 giờ)
            ViewBag.CanCancel = order.TrangThai == 1 && (DateTime.Now - order.NgayDatHang).TotalHours <= 1;

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> RequestCancelOrder(int orderId, string reason)
        {
            try
            {
                if (!User.Identity!.IsAuthenticated)
                {
                    return Json(new { success = false, message = "Vui lòng đăng nhập" });
                }

                var customerIdClaim = HttpContext.User.Claims.SingleOrDefault(c => c.Type == "MaKhachHang");
                if (customerIdClaim == null || !int.TryParse(customerIdClaim.Value, out var customerId))
                {
                    return Json(new { success = false, message = "Phiên đăng nhập không hợp lệ" });
                }

                var order = await _context.HoaDons
                    .FirstOrDefaultAsync(h => h.MaHoaDon == orderId && h.MaKhachHang == customerId);

                if (order == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy đơn hàng" });
                }

                // Kiểm tra trạng thái đơn hàng
                if (order.TrangThai == 0 || order.TrangThai == 4)
                {
                    return Json(new { success = false, message = "Đơn hàng này đã được hủy hoặc hoàn thành" });
                }

                // Kiểm tra đã yêu cầu hủy chưa
                if (order.DaYeuCauHuy)
                {
                    return Json(new { success = false, message = "Bạn đã gửi yêu cầu hủy đơn hàng này rồi" });
                }

                // Cập nhật yêu cầu hủy
                order.DaYeuCauHuy = true;
                order.YeuCauHuy = reason?.Trim() ?? "";
                order.NgayYeuCauHuy = DateTime.Now;
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Gửi yêu cầu hủy đơn hàng thành công. Admin sẽ xem xét và phản hồi sớm nhất." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            try
            {
                if (!User.Identity!.IsAuthenticated)
                {
                    return Json(new { success = false, message = "Vui lòng đăng nhập" });
                }

                var customerIdClaim = HttpContext.User.Claims.SingleOrDefault(c => c.Type == "MaKhachHang");
                if (customerIdClaim == null || !int.TryParse(customerIdClaim.Value, out var customerId))
                {
                    return Json(new { success = false, message = "Phiên đăng nhập không hợp lệ" });
                }

                var order = await _context.HoaDons
                    .FirstOrDefaultAsync(h => h.MaHoaDon == orderId && h.MaKhachHang == customerId);

                if (order == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy đơn hàng" });
                }

                // Kiểm tra trạng thái đơn hàng - Chỉ cho phép hủy khi đơn hàng chưa được xác nhận
                if (order.TrangThai != 0)
                {
                    return Json(new { success = false, message = "Chỉ có thể hủy đơn hàng chưa được xác nhận" });
                }

                // Cập nhật trạng thái đơn hàng thành đã hủy
                order.TrangThai = 4; // Đã hủy
                order.YeuCauHuy = "Khách hàng hủy đơn";
                order.NgayYeuCauHuy = DateTime.Now;
                order.DaYeuCauHuy = true;
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Hủy đơn hàng thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        private string GetClientIpAddress()
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            if (string.IsNullOrEmpty(ipAddress) || ipAddress == "::1")
            {
                return "127.0.0.1"; // Dự phòng cho localhost
            }
            return ipAddress;
        }
    }
}