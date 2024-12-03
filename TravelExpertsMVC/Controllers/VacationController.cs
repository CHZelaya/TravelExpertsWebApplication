using Microsoft.AspNetCore.Mvc;

namespace TravelExpertsMVC.Controllers
{
    public class VacationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
