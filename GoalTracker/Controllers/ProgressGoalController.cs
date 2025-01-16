using Microsoft.AspNetCore.Mvc;
using GoalTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GoalTracker.Controllers
{
    public class ProgressGoalController : Controller
    {
        private readonly GoalDbContext _context;

        public ProgressGoalController(GoalDbContext context)
        {
            _context = context;
        }

        // GET: ProgressGoal
        public IActionResult Index()
        {
            var progressGoals = _context.ProgressTrackingGoals
                                         .Include(pg => pg.ProgressLogs)  // Include ProgressLogs when fetching goals
                                         .ToList();
            return View("~/Views/Progress/Index.cshtml", progressGoals);
        }

        // GET: ProgressGoal/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProgressGoal/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProgressTrackingGoal progressGoal)
        {
            if (ModelState.IsValid)
            {
                _context.ProgressTrackingGoals.Add(progressGoal);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(progressGoal);
        }

        // GET: ProgressGoal/Edit/5
        public IActionResult Edit(int id)
        {
            var goal = _context.ProgressTrackingGoals
                                .Include(pg => pg.ProgressLogs)  // Ensure related ProgressLogs are included when editing
                                .FirstOrDefault(pg => pg.Id == id);
            if (goal == null)
            {
                return NotFound();
            }
            return View(goal);
        }

        // POST: ProgressGoal/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ProgressTrackingGoal progressGoal)
        {
            if (id != progressGoal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(progressGoal);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(progressGoal);
        }

        // GET: ProgressGoal/Details/5
        public IActionResult Details(int id)
        {
            var goal = _context.ProgressTrackingGoals
                                .Include(pg => pg.ProgressLogs)  // Include related ProgressLogs in the details view
                                .FirstOrDefault(pg => pg.Id == id);
            if (goal == null)
            {
                return NotFound();
            }
            return View(goal);
        }

        // GET: ProgressGoal/Delete/5
        public IActionResult Delete(int id)
        {
            var goal = _context.ProgressTrackingGoals
                                .Include(pg => pg.ProgressLogs)  // Ensure related logs are fetched before deleting
                                .FirstOrDefault(pg => pg.Id == id);
            if (goal == null)
            {
                return NotFound();
            }
            return View(goal);
        }

        // POST: ProgressGoal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var goal = _context.ProgressTrackingGoals
                                .Include(pg => pg.ProgressLogs)  // Include related ProgressLogs before deleting
                                .FirstOrDefault(pg => pg.Id == id);

            if (goal != null)
            {
                // Optionally, delete the related ProgressLogs first
                foreach (var log in goal.ProgressLogs)
                {
                    _context.ProgressLogs.Remove(log);  // Remove related ProgressLogs
                }

                _context.ProgressTrackingGoals.Remove(goal);  // Remove the goal itself
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}


