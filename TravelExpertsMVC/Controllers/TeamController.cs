using Microsoft.AspNetCore.Mvc;

namespace TravelExpertsMVC.Controllers
{
    public class TeamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
