using Ecommerce_WatchShop.Models;
using Ecommerce_WatchShop.Models.ViewModels;
using MailKit.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MailKit.Net.Smtp;
using MimeKit;
using System.Security.Cryptography;

namespace Ecommerce_WatchShop.Controllers;

public class AccountController : Controller
{
    private readonly DongHoContext _context;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AccountController> _logger;

    public AccountController(DongHoContext context, IConfiguration configuration, ILogger<AccountController> logger)
    {
        _context = context;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var customerIdClaim = User.Claims.FirstOrDefault(c => c.Type == "MaKhachHang");
        if (customerIdClaim == null) return RedirectToAction("Index", "Home");

        if (!int.TryParse(customerIdClaim.Value, out int customerId))
            return BadRequest("Customer ID không hợp lệ.");

        var customer = await _context.KhachHangs
            .FirstOrDefaultAsync(c => c.MaKhachHang == customerId);

        if (customer == null) return NotFound();
        return View(customer);
    }

    public async Task<IActionResult> Edit()
    {
        var customerIdClaim = User.Claims.FirstOrDefault(c => c.Type == "MaKhachHang");
        if (customerIdClaim == null) return RedirectToAction("Index", "Home");

        if (!int.TryParse(customerIdClaim.Value, out int customerId))
            return BadRequest("Customer ID không hợp lệ.");

        var customer = await _context.KhachHangs
            .FirstOrDefaultAsync(c => c.MaKhachHang == customerId);

        if (customer == null) return NotFound();

        var customerVM = new CustomerVM
        {
            FullName = customer.HoTen,
            Phone = customer.SoDienThoai,
            Address = customer.DiaChi,
            Email = customer.Email,
            DisplayName = customer.TenHienThi,
            Dob = customer.NgaySinh,
            Gender = customer.GioiTinh
        };

        return View(customerVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(CustomerVM customerVM)
    {
        var customerIdClaim = User.Claims.FirstOrDefault(c => c.Type == "MaKhachHang");
        if (customerIdClaim == null) return RedirectToAction("Index", "Home");

        if (!int.TryParse(customerIdClaim.Value, out int customerId))
            return BadRequest("Customer ID không hợp lệ.");

        var customer = await _context.KhachHangs
            .FirstOrDefaultAsync(c => c.MaKhachHang == customerId);

        if (customer == null) return NotFound();

        if (!ModelState.IsValid)
        {
            return View(customerVM);
        }

        customer.HoTen = customerVM.FullName;
        customer.SoDienThoai = customerVM.Phone;
        customer.DiaChi = customerVM.Address;
        customer.Email = customerVM.Email;
        customer.TenHienThi = customerVM.DisplayName;
        customer.NgaySinh = customerVM.Dob;
        customer.GioiTinh = customerVM.Gender;

        _context.Update(customer);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Account");
    }

    [HttpGet]
    public async Task<IActionResult> Order(int? status)
    {
        ViewBag.Title = "Đơn hàng";

        var customerIdClaim = User.Claims.FirstOrDefault(c => c.Type == "MaKhachHang");
        if (customerIdClaim == null) return RedirectToAction("Index", "Home");

        if (!int.TryParse(customerIdClaim.Value, out int customerId))
            return BadRequest("Customer ID không hợp lệ.");

        Console.WriteLine($"Customer ID: {customerId}");

        var query = _context.HoaDons.Where(b => b.MaKhachHang == customerId);

        if (status.HasValue)
        {
            query = query.Where(b => b.TrangThai == status.Value);
        }

        var bills = await query.ToListAsync();

        return View(bills);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CancelOrder(int id, string reason)
    {
        var customerIdClaim = User.Claims.FirstOrDefault(c => c.Type == "MaKhachHang");
        if (customerIdClaim == null) return Json(new { success = false, message = "Vui lòng đăng nhập!" });

        if (!int.TryParse(customerIdClaim.Value, out int customerId))
            return Json(new { success = false, message = "Customer ID không hợp lệ." });

        var bill = await _context.HoaDons.FirstOrDefaultAsync(b => b.MaHoaDon == id && b.MaKhachHang == customerId);

        if (bill == null)
        {
            return Json(new { success = false, message = "Không tìm thấy đơn hàng!" });
        }

        // Chỉ cho phép hủy khi đơn hàng chưa xác nhận (TrangThai = 0)
        if (bill.TrangThai != 0)
        {
            return Json(new { success = false, message = "Chỉ có thể hủy đơn hàng chưa được xác nhận!" });
        }

        if (string.IsNullOrWhiteSpace(reason))
        {
            return Json(new { success = false, message = "Vui lòng chọn lý do hủy đơn!" });
        }

        bill.DaYeuCauHuy = true;
        bill.YeuCauHuy = reason;
        bill.NgayYeuCauHuy = DateTime.Now;
        bill.TrangThai = 4; // 4 = Đã hủy

        // Hoàn trả số lượng sản phẩm vào kho
        var orderDetails = await _context.ChiTietHoaDons
            .Where(ct => ct.MaHoaDon == id)
            .ToListAsync();

        foreach (var detail in orderDetails)
        {
            var product = await _context.SanPhams.FindAsync(detail.MaSanPham);
            if (product != null)
            {
                product.SoLuong += detail.SoLuong; // Cộng lại số lượng
                _context.SanPhams.Update(product);
            }
        }

        _context.HoaDons.Update(bill);
        await _context.SaveChangesAsync();

        return Json(new { success = true, message = "Đơn hàng đã được hủy thành công!" });
    }

    [HttpGet]
    public async Task<IActionResult> Favorite()
    {
        var customerIdClaim = User.Claims.FirstOrDefault(c => c.Type == "MaKhachHang");
        if (customerIdClaim == null)
        {
            return RedirectToAction("LoginPartial", "Home");
        }

        if (!int.TryParse(customerIdClaim.Value, out int customerId))
        {
            return BadRequest("Customer ID không hợp lệ.");
        }

        var favoriteProducts = await _context.YeuThichs
            .Include(f => f.SanPham)
            .Where(f => f.MaKhachHang == customerId)
            .Select(f => new FavoriteVM
            {
                ProductId = f.SanPham.MaSanPham,
                Name = f.SanPham.TenSanPham,
                Price = f.SanPham.Gia,
                Image = f.SanPham.HinhAnh ?? "default-image.jpg",
                Slug = f.SanPham.Slug ?? ""
            })
            .ToListAsync();

        return View(favoriteProducts);
    }

    [HttpPost]
    public JsonResult AddToWishlist(int productId)
    {
        var customerIdClaim = User.Claims.FirstOrDefault(c => c.Type == "MaKhachHang");
        if (customerIdClaim == null)
        {
            return Json(new { success = false, message = "Bạn cần đăng nhập để thêm vào danh sách yêu thích." });
        }

        if (!int.TryParse(customerIdClaim.Value, out int customerId))
        {
            return Json(new { success = false, message = "Customer ID không hợp lệ." });
        }

        var existingWishlist = _context.YeuThichs
            .FirstOrDefault(w => w.MaKhachHang == customerId && w.MaSanPham == productId);

        if (existingWishlist != null)
        {
            return Json(new { success = false, message = "Sản phẩm đã có trong danh sách yêu thích!" });
        }

        var wishlist = new YeuThich
        {
            MaKhachHang = customerId,
            MaSanPham = productId
        };
        _context.YeuThichs.Add(wishlist);
        _context.SaveChanges();

        return Json(new { success = true, message = "Đã thêm vào danh sách yêu thích!" });
    }

    [HttpGet]
    public IActionResult RemoveFavorite(int productId)
    {
        var customerIdClaim = User.Claims.FirstOrDefault(c => c.Type == "MaKhachHang");
        if (customerIdClaim == null)
        {
            return RedirectToAction("LoginPartial", "Home");
        }

        if (!int.TryParse(customerIdClaim.Value, out int customerId))
        {
            return BadRequest("Customer ID không hợp lệ.");
        }

        var wishlistItem = _context.YeuThichs
            .FirstOrDefault(w => w.MaKhachHang == customerId && w.MaSanPham == productId);
        if (wishlistItem != null)
        {
            _context.YeuThichs.Remove(wishlistItem);
            _context.SaveChanges();
            TempData["success"] = "Đã xóa sản phẩm khỏi danh sách yêu thích!";
        }

        return RedirectToAction("Favorite");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult QuenMatKhau()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> QuenMatKhau(ForgotPasswordVM model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var account = await _context.TaiKhoans
            .Include(a => a.KhachHang)
            .FirstOrDefaultAsync(a => a.KhachHang.Email == model.Email);
        if (account == null || account.KhachHang == null)
        {
            ModelState.AddModelError("", "Không tìm thấy tài khoản hoặc email liên quan với thông tin này.");
            return View(model);
        }

        var token = GenerateToken();
        var tokenKhoiPhuc = new TokenKhoiPhucMatKhau
        {
            MaTaiKhoan = account.MaTaiKhoan,
            MaToken = token,
            NgayHetHan = DateTime.UtcNow.AddHours(1)
        };

        _context.TokenKhoiPhucMatKhaus.Add(tokenKhoiPhuc);
        await _context.SaveChangesAsync();

        var resetLink = Url.Action("DatLaiMatKhau", "Account", new { token = token }, Request.Scheme);
        try
        {
            await SendResetEmail(account.KhachHang.Email, resetLink);
            _logger.LogInformation("Email sent successfully to {Email} with reset link: {ResetLink}", account.KhachHang.Email, resetLink);
            TempData["success"] = "Đã gửi liên kết đặt lại mật khẩu qua email của bạn. Vui lòng kiểm tra hộp thư.";
            return RedirectToAction("QuenMatKhau");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send reset email to {Email}", account.KhachHang.Email);
            TempData["error"] = $"Có lỗi khi gửi email: {ex.Message}";
            return View(model);
        }
    }

    [HttpGet]
    public IActionResult DatLaiMatKhau(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return BadRequest("Mã token không hợp lệ.");
        }

        var tokenKhoiPhuc = _context.TokenKhoiPhucMatKhaus
            .FirstOrDefault(rt => rt.MaToken == token && rt.NgayHetHan > DateTime.UtcNow);
        if (tokenKhoiPhuc == null)
        {
            TempData["error"] = "Mã token không hợp lệ hoặc đã hết hạn.";
            return RedirectToAction("QuenMatKhau");
        }

        var model = new ResetPasswordVM { Token = token };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DatLaiMatKhau(ResetPasswordVM model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var tokenKhoiPhuc = _context.TokenKhoiPhucMatKhaus
            .FirstOrDefault(rt => rt.MaToken == model.Token && rt.NgayHetHan > DateTime.UtcNow);
        if (tokenKhoiPhuc == null)
        {
            TempData["error"] = "Mã token không hợp lệ hoặc đã hết hạn.";
            return RedirectToAction("QuenMatKhau");
        }

        var account = await _context.TaiKhoans
            .FirstOrDefaultAsync(a => a.MaTaiKhoan == tokenKhoiPhuc.MaTaiKhoan);
        if (account == null)
        {
            TempData["error"] = "Tài khoản không tồn tại.";
            return RedirectToAction("QuenMatKhau");
        }

        account.MatKhau = model.NewPassword; // Nên mã hóa trước khi lưu
        _context.TokenKhoiPhucMatKhaus.Remove(tokenKhoiPhuc);
        await _context.SaveChangesAsync();

        TempData["success"] = "Mật khẩu đã được đặt lại thành công. Vui lòng đăng nhập lại.";
        return RedirectToAction("Login", "Home");
    }

    private string GenerateToken()
    {
        var bytes = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(bytes);
        }
        return Convert.ToBase64String(bytes);
    }

    private async Task SendResetEmail(string email, string resetLink)
    {
        var emailSettings = _configuration.GetSection("EmailSettings").Get<EmailSettings>();
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Ecommerce WatchShop", emailSettings.FromEmail));
        message.To.Add(new MailboxAddress("", email));
        message.Subject = "Đặt lại mật khẩu";

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = $"<h2>Đặt lại mật khẩu</h2><p>Nhấp vào liên kết dưới đây để đặt lại mật khẩu của bạn:</p><p><a href='{resetLink}'>{resetLink}</a></p><p>Liên kết này sẽ hết hạn sau 1 giờ.</p>"
        };

        message.Body = bodyBuilder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(emailSettings.SmtpServer, emailSettings.SmtpPort, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(emailSettings.SmtpUsername, emailSettings.SmtpPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}