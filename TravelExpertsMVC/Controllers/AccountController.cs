using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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

        [HttpGet]
        public async Task<IActionResult> GetProfileImage()
        {
            var user = await userManager.GetUserAsync(User);//gets the signed in user details
            if (user?.ProfilePicture != null && user.ProfilePicture.Length > 0)
            {
                return File(user.ProfilePicture, "image/jpeg");
            }

            // Return default profile picture if none exists
            var defaultImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "default-profile.jpg");
            var defaultImage = System.IO.File.ReadAllBytes(defaultImagePath);
            return File(defaultImage, "image/jpeg");
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                //authenticate
                var result = await signInManager.PasswordSignInAsync(lvm.Username, lvm.Password, lvm.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Login");
                    return View();
                }
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();//signout
            return RedirectToAction("Index", "Home");
        }
       
        public async Task<IActionResult> EditProfile()
        {
            var user = await userManager.GetUserAsync(User);
            //get user details
            EditProfileViewModel model = AccountManager.GetUserDetails(_context,user!.Id)!;
            return View(model);
        }

        public async Task<IActionResult> DeleteProfilePicture(EditProfileViewModel vm)
        {
            var user = await userManager.GetUserAsync(User);
            if(!AccountManager.IsProfilePictureDelete(_context,user.Id))
            {
                ModelState.AddModelError("", "Error deleting Picture");                
            }
            return RedirectToAction("EditProfile", "Account",vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfileAsync(EditProfileViewModel vm, IFormFile? ProfilePicture)//impt note u have to put nullable file else the form will read it as required
        {
            if (!ModelState.IsValid) return View(vm);
            
            var currentUser = await userManager.GetUserAsync(User);

            //converting iForm to byte []
            if (ProfilePicture != null && ProfilePicture.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await ProfilePicture.CopyToAsync(memoryStream);
                vm.ProfilePicture = memoryStream.ToArray();//updating pp in vm
            }
            //update user details
            int rowsAffected = AccountManager.UpdateUser(_context, currentUser, vm);
            if (rowsAffected <= 0)
            {
                ModelState.AddModelError("", "Error Updating Profile");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
