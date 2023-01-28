using BizLand.Areas.Manage.ViewModels;
using BizLand.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BizLand.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AccauntController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccauntController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel adminLoginVM)
        {
            AppUser admin = await _userManager.FindByNameAsync(adminLoginVM.UserName);
            if (admin == null)
            {
                ModelState.AddModelError("" , "Username or password is incorrect");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(admin , adminLoginVM.Password, false ,false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is incorrect");
                return View();
            }


            return RedirectToAction("index" ,"dashboard");
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login" , "accaunt");
        }
    }
}
