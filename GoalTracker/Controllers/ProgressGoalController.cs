using Microsoft.AspNetCore.Mvc;
using GoalTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

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
            ViewData["ActivePage"] = "ProgressGoals";
            var progressGoals = _context.ProgressTrackingGoals
                                         .Include(pg => pg.ProgressLogs)  // Include ProgressLogs when fetching goals
                                         .ToList();
            return View("~/Views/Progress/Index.cshtml", progressGoals);
        }

        // GET: ProgressGoal/Create
        public IActionResult Create()
        {
            return View("~/Views/Progress/Create.cshtml");
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
            var progressGoal = _context.ProgressTrackingGoals
                                       .Include(pg => pg.ProgressLogs)  // Include ProgressLogs if needed
                                       .FirstOrDefault(pg => pg.Id == id);

            if (progressGoal == null)
            {
                return NotFound();  // If no goal found with that ID, return a 404 page
            }

            return View("~/Views/Progress/Edit.cshtml", progressGoal);  // Return the view with the existing goal
        }


        // POST: ProgressGoal/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ProgressTrackingGoal progressGoal)
        {
            if (id != progressGoal.Id)
            {
                return NotFound();  // If the IDs don't match, return a 404 page
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(progressGoal);  // Update the goal in the database
                    _context.SaveChanges();  // Save the changes
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.ProgressTrackingGoals.Any(pg => pg.Id == id))
                    {
                        return NotFound();  // If the goal is not found in the database, return a 404 page
                    }
                    else
                    {
                        throw;  // Rethrow the exception if another error occurs
                    }
                }

                return RedirectToAction(nameof(Index));  // Redirect back to the Index page after editing
            }

            return View("Progress/Edit", progressGoal);  // Return the form again if validation fails
        }



        // GET: ProgressGoal/Details/5
        public IActionResult Details(int id)
        {
            var progressGoal = _context.ProgressTrackingGoals
                                       .Include(pg => pg.ProgressLogs)  // Include related logs
                                       .FirstOrDefault(pg => pg.Id == id);

            if (progressGoal == null)
            {
                // Return a "Not Found" view or page if the goal does not exist
                return NotFound();
            }

            // Ensure ProgressLogs is not null (initialize it if necessary)
            progressGoal.ProgressLogs = progressGoal.ProgressLogs ?? new List<ProgressLog>();

            // Explicitly specify the view location
            return View("~/Views/Progress/Details.cshtml", progressGoal);
        }


        // GET: ProgressGoal/Delete/5
        public IActionResult Delete(int id)
        {
            var progressGoal = _context.ProgressTrackingGoals
                                       .FirstOrDefault(pg => pg.Id == id);  // Fetch the goal by ID

            if (progressGoal == null)
            {
                return NotFound();  // If the goal doesn't exist, return a 404 error
            }

            return View("~/Views/Progress/Delete.cshtml", progressGoal);  // Pass the goal to the Delete view
        }


        // POST: ProgressGoal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var progressGoal = _context.ProgressTrackingGoals
                                       .FirstOrDefault(pg => pg.Id == id);

            if (progressGoal == null)
            {
                return NotFound();  // If the goal doesn't exist, return a 404 error
            }

            _context.ProgressTrackingGoals.Remove(progressGoal);  // Remove the goal from the context
            _context.SaveChanges();  // Save changes to the database

            return RedirectToAction(nameof(Index));  // Redirect back to the list after deletion
        }

    }
}


