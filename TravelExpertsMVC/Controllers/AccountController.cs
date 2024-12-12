using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using TravelExpertsData.Manager;
using TravelExpertsData.Models;
using TravelExpertsData.ViewModel;
using TravelExpertsMVC.EmailService;

namespace TravelExpertsMVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        private TravelExpertsContext _context;

        private readonly IEmailSender _emailSenderService;
        public AccountController(SignInManager<User> signInManager,UserManager<User> userManager,
            TravelExpertsContext context, IEmailSender emailSenderService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context; 
            _emailSenderService = emailSenderService;
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel rvm, IFormFile? ProfilePicture)//pp optional
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
                //sent email
                string message = "Your next unforgettable adventure begins here!\r\n\r\nAt Travel Experts, we specialize in creating bespoke " +
                    "travel packages tailored to your unique style. Whether you're chasing breathtaking landscapes, immersing yourself in vibrant cultures," +
                    " or seeking the perfect balance of relaxation and adventure, we’ve got you covered.\r\n\r\nDiscover hidden gems, experience local wonders, " +
                    "and let us turn your travel dreams into extraordinary realities.\r\n\r\n🌍 Explore the world with Travel Experts – your journey awaits!";
                var mail = await _emailSenderService.SendEmailAsync(rvm.Email, "Welcome to Travel Experts, "+u.UserName, message);
                //debug here for mail
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
            else {//keep prev pp if null
                vm.ProfilePicture = currentUser.ProfilePicture;
            }
            //update user details
            int rowsAffected = AccountManager.UpdateUser(_context, currentUser, vm);
            if (rowsAffected <= 0)
            {
                ModelState.AddModelError("", "Error Updating Profile");
            }
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public async void TestEmailNtfn()
        {
            //sent email
            string message = "lorem";
            var mail = await _emailSenderService.SendEmailAsync("yefij99220@bawsny.com", "Welcome to Travel Experts", message);
            Console.WriteLine(mail);
        }
    }
}
