using AspNetCoreHero.ToastNotification.Abstractions;
using FineBlog.Models;
using FineBlog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FineBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    { 
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly INotyfService _notification;
        
        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, INotyfService notyfService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _notification = notyfService;
        }
        [Authorize(Roles="Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();

            var vm = users.Select(x => new UserVM()
            {
                Id = x.Id,
                FirstName = x.Firstname,
                LastName = x.Lastname,
                UserName = x.UserName,
            }).ToList();
            return View(vm);
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            if (!HttpContext.User.Identity!.IsAuthenticated)
            {
                return View(new LoginVM());
            }
            return RedirectToAction("Index", "User", new { area = "Admin" });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }
            var existingUser = _userManager.Users.FirstOrDefault(x=>x.UserName == vm.Username);
            if (existingUser == null)
            {
                _notification.Error("Kullanıcı adı veya Şifre yanlış.");
                return View(vm);
            }
            var verifyPassword = await _userManager.CheckPasswordAsync(existingUser, vm.Password);
            if (!verifyPassword)
            {
                _notification.Error("Kullanıcı adı veya Şifre yanlış");
                return View(vm);
            }
            await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, vm.RememberMe, true);
            _notification.Success("Giriş Yapıldı.");
            return RedirectToAction("Index", "User", new { area = "Admin" });
        }

        [HttpPost]
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            _notification.Success("Çıkış yapıldı.");
            return RedirectToAction("Index", "Home", new {area =""});
        }

    }
}
