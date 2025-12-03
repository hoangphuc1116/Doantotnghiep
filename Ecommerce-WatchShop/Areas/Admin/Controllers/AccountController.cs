using Ecommerce_WatchShop.Models;
using Ecommerce_WatchShop.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Ecommerce_WatchShop.Areas.Admin.Controllers
{
    [AllowAnonymous]
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly DongHoContext _context;
        public AccountController(DongHoContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            var account = await _context.TaiKhoans.FirstOrDefaultAsync(a => a.TenDangNhap == loginVM.Username);
            if (account == null)
            {
                ModelState.AddModelError("Username", "Tài khoản Admin không tồn tại");
                return View(loginVM);
            }

            if (account.MatKhau != loginVM.Password)
            {
                ModelState.AddModelError("Password", "Tài khoản hoặc mật khẩu không đúng");
                return View(loginVM);
            }

            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(loginVM.Username))
            {
                claims.Add(new Claim(ClaimTypes.Name, loginVM.Username));
            }
            if (account.MaVaiTro > 0)
            {
                var roleValue = account.MaVaiTro.ToString();
                if (!string.IsNullOrEmpty(roleValue))
                {
                    claims.Add(new Claim(ClaimTypes.Role, roleValue));
                }
            }

            var claimsIdentity = new ClaimsIdentity(claims, "Admin");
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                RedirectUri = returnUrl
            };

            await HttpContext.SignInAsync("Admin", new ClaimsPrincipal(claimsIdentity), authProperties);

            if (account.MaVaiTro == 2)
            {
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }

            return RedirectToAction("Login", "Account");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Admin");
            return RedirectToAction("Login", "Account");
        }

    }
}
