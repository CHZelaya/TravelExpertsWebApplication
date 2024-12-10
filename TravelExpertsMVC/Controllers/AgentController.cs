using Microsoft.AspNetCore.Mvc;
using TravelExpertsData.Manager;
using TravelExpertsData.Models;

namespace TravelExpertsMVC.Controllers
{
    public class AgentController : Controller
    {
        private readonly TravelExpertsContext _context;

        public AgentController(TravelExpertsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IActionResult Index()
        {
            List<Agent> agents = AgentManager.GetAllAgents(_context);
            return View(agents);
        }
    }
}
