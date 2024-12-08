using Microsoft.AspNetCore.Mvc;
using TravelExpertsData.Models;

namespace TravelExpertsMVC.Controllers
{
    public class BookingController : Controller
    {
        TravelExpertsContext _context;
        public BookingController(TravelExpertsContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            List<BookingDTO> bookings = new List<BookingDTO>();
            bookings = BookingManager.getBooksbyCustomerId(_context,135);
            return View(bookings);
        }

        public IActionResult Detail(int id)
        {
            List<BookingDetailDTO> details = BookingManager.GetBookingDetails(_context, id);
            return View(details);
        }
    }
}
