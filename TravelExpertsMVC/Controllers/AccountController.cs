using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelExpertsData.Manager;
using TravelExpertsData.Models;
using TravelExpertsData.ViewModel;

namespace TravelExpertsMVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        private TravelExpertsContext _context;
        public AccountController(SignInManager<User> signInManager,UserManager<User> userManager, TravelExpertsContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context; 
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel rvm, IFormFile ProfilePicture)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
                
            Customer? c = AccountManager.SaveCustomer(_context, rvm);
            if (c == null)
            {
                ModelState.AddModelError("","Error Saving User");
                return View(rvm);
            }
            User u = new User
            {
                CustomerId = c.CustomerId,
                Customer = c,
                Email = rvm.Email,
                UserName = rvm.UserName,
                TravelPreference = rvm.TravelPreference,
            };
            //converting iForm to byte []
            if (ProfilePicture != null && ProfilePicture.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await ProfilePicture.CopyToAsync(memoryStream);
                u.ProfilePicture = memoryStream.ToArray();//updating pp in user
            }

            var result = await userManager.CreateAsync(u, rvm.Password!);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(u, false);
                return RedirectToAction("Index", "Home");
            }
            foreach (var item in result.Errors)
                ModelState.AddModelError("", item.Description);
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();//signout
            return RedirectToAction("Index", "Home");
        }
    }
}
