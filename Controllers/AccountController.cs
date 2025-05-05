using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebSite.Services.IServices;
using System.Security.Claims;

namespace MyWebSite.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        // Gizli admin login adresi
        [HttpGet("/admin-login-3477")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            // Eğer zaten giriş yapılmışsa doğrudan yönlendir
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View();
        }

        [HttpPost("/admin-login-3477")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string username, string password)
        {
            var result = _userService.ValidateUser(username, password);

            if (!result.Success)
            {
                ViewBag.PopupMessage = result.ErrorMessage;
                return View();
            }

            // Login başarılı
            var user = result.User!;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // Giriş yapan admin ise dashboard'a yönlendir
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete(".AspNetCore.Cookies"); // güvenlik için ekstra temizlik

            return RedirectToAction("Login", "Account");
        }
    }
}
