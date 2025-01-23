using System.Diagnostics;
using GoalTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoalTracker.Controllers
{

    /// <summary>
    /// Handles the home page and general site navigation functionality.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly GoalDbContext _context;


        /// <summary>
        /// Initialises a new instance of the HomeController class.
        /// </summary>
        /// <param name="context">The database context for accessing data.</param>
        public HomeController(GoalDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Displays the home page view.
        /// </summary>
        /// <returns>The view for the home page.</returns>
        public IActionResult Index()
        {
            ViewData["ActivePage"] = "Home";
            return View();
        }


        /// <summary>
        /// Displays the privacy policy view.
        /// </summary>
        /// <returns>The view for the privacy policy page.</returns>
        public IActionResult Privacy()
        {
            return View();
        }


        /// <summary>
        /// Handles errors and displays the error page.
        /// </summary>
        /// <returns>The error view, containing the request identifier.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}
