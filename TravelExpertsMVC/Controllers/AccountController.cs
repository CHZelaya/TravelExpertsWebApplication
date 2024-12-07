using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> RegisterAsync(RegisterViewModel rvm)
        {
            return View();
        }
    }
}
