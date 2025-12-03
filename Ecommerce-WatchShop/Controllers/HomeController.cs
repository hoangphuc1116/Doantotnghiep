using Ecommerce_WatchShop.Models;
using Ecommerce_WatchShop.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;


namespace Ecommerce_WatchShop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DongHoContext _context;

    public HomeController(ILogger<HomeController> logger, DongHoContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var sliders = await _context.Sliders.Where(s => s.TrangThai).OrderBy(s => s.ThuTuHienThi).ToListAsync();
        return View(sliders);
    }

    public async Task<IActionResult> Introduction()
    {
        var aboutVM = new AboutVM
        {
            GioiThieus = await _context.GioiThieus.FirstOrDefaultAsync(),
            ChinhSachs = await _context.ChinhSachs.ToListAsync()
        };
        return View(aboutVM);
    }
    [HttpGet]
    public IActionResult Contact()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Contact(ContactVM contactVM)
    {
        if (ModelState.IsValid)
        {
            var contact = new LienHe
            {
                HoTen = string.IsNullOrEmpty(contactVM.FullName) ? "" : contactVM.FullName,
                Email = string.IsNullOrEmpty(contactVM.Email) ? "" : contactVM.Email,
                TieuDe = string.IsNullOrEmpty(contactVM.Subject) ? "" : contactVM.Subject,
                GhiChu = string.IsNullOrEmpty(contactVM.Note) ? "" : contactVM.Note
            };


            _context.LienHes.Add(contact);
            await _context.SaveChangesAsync();


            return RedirectToAction("Index");
        }

        return View("Contact", contactVM);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterVM registerVM, string? ReturnUrl = null)
    {
        ViewData["ReturnUrl"] = ReturnUrl;

        if (!ModelState.IsValid)
        {
            return PartialView("_RegisterPartial", registerVM);
        }

        var exist = _context.TaiKhoans
            .FirstOrDefault(a => a.TenDangNhap == registerVM.Username);

        if (exist != null)
        {
            ModelState.AddModelError("Username", "Username đã tồn tại.");
            return PartialView("_RegisterPartial", registerVM);
        }

        var account = new TaiKhoan
        {
            TenDangNhap = registerVM.Username,
            MatKhau = registerVM.Password,
            MaVaiTro = 1,
        };

        _context.TaiKhoans.Add(account);
        await _context.SaveChangesAsync();

        var customer = new KhachHang
        {
            MaTaiKhoan = account.MaTaiKhoan,
            TenHienThi = account.TenDangNhap
        };

        _context.KhachHangs.Add(customer);
        await _context.SaveChangesAsync();
        TempData["success"] = "Đăng ký thành công";
        if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
        {
            return Json(new { redirectToUrl = ReturnUrl });
        }
        else
        {
            return Json(new { redirectToUrl = Url.Action("Index", "Home") });
        }
    }
    [HttpGet]
    public IActionResult LoginPartial()
    {
        return PartialView("_LoginPartial", new LoginVM());
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVM loginVM, string? ReturnUrl = null)
    {
        ViewData["ReturnUrl"] = ReturnUrl;
        if (!ModelState.IsValid)
        {
            return PartialView("_LoginPartial", loginVM);
        }

        var account = await _context.TaiKhoans
            .Include(a => a.KhachHang)
            .FirstOrDefaultAsync(a => a.TenDangNhap == loginVM.Username);

        if (account == null)
        {
            ModelState.AddModelError("Username", "Tài khoản không tồn tại");
            return PartialView("_LoginPartial", loginVM);
        }

        if (account.MatKhau != loginVM.Password)
        {
            ModelState.AddModelError("Password", "Tài khoản hoặc mật khẩu bị sai");
            return PartialView("_LoginPartial", loginVM);
        }

        if (account.MaVaiTro == 2)
        {
            ModelState.AddModelError("Username", "Không có quyền truy cập");
            return PartialView("_LoginPartial", loginVM);
        }

        var customer = account.KhachHang;
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, account.MaTaiKhoan.ToString()),
        new Claim("MaTaiKhoan", account.MaTaiKhoan.ToString())
    };

        // Thêm MaKhachHang, sử dụng 0 nếu chưa có KhachHang
        claims.Add(new Claim("MaKhachHang", (customer?.MaKhachHang ?? 0).ToString()));

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = loginVM.RememberMe,
            ExpiresUtc = DateTimeOffset.Now.AddDays(5),
        };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

        TempData["success"] = "Đăng nhập thành công";
        if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
        {
            return Json(new { redirectToUrl = ReturnUrl });
        }
        else
        {
            return Json(new { redirectToUrl = Url.Action("Index", "Home") });
        }
    }
    //Trang yêu thích
    [HttpGet]
    public async Task<IActionResult> Favorite()
    {
        var customerIdClaim = User.Claims.FirstOrDefault(c => c.Type == "CustomerId");
        if (customerIdClaim == null)
        {
            return RedirectToAction("LoginPartial");
        }

        var customerId = int.Parse(customerIdClaim.Value);
        var favorites = await _context.YeuThichs
            .Where(yt => yt.MaKhachHang == customerId)
            .Include(yt => yt.SanPham)
            .Select(yt => new FavoriteVM
            {
                ProductId = yt.SanPham.MaSanPham,
                Name = yt.SanPham.TenSanPham ?? "Chưa có tên",
                Price = yt.SanPham.Gia,
                Image = yt.SanPham.HinhAnh ?? "default-image.jpg",
                Slug = yt.SanPham.Slug ?? ""
            })
            .ToListAsync();

        return View(favorites); // Trả về danh sách FavoriteVM trực tiếp
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(int statuscode)
    {
        if (statuscode == 404)
        {
            return View("404");
        }
        else
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        }
    }
}