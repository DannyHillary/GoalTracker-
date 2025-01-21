using System.Diagnostics;
using GoalTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoalTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GoalDbContext _context;

        public HomeController(ILogger<HomeController> logger, GoalDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["ActivePage"] = "Home";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public JsonResult GetHabitsForCalendar()
        {
            var habits = _context.HabitTrackingGoals
                                 .Select(h => new
                                 {
                                     title = h.Title,
                                     start = h.StartDate.ToString("yyyy-MM-dd"),
                          
                                 }).ToList();

            return new JsonResult(habits);
        }

    }
}
