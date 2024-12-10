using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelExpertsData.Manager;
using TravelExpertsData.Models;

namespace TravelExpertsMVC.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        TravelExpertsContext _context;
        private readonly UserManager<User> _userManager;
        public BookingController(TravelExpertsContext context, UserManager<User> userManager)
        {
            this._context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User) as User;
            List<BookingDTO> bookings = new List<BookingDTO>();
            bookings = BookingManager.getBooksbyCustomerId(_context,user!.CustomerId);
            return View(bookings);
        }

        public IActionResult Detail(int id)
        {
            List<BookingDetailDTO> details = BookingManager.GetBookingDetails(_context, id);
            return View(details);
        }
    }
}
