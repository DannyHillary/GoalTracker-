using Microsoft.AspNetCore.Mvc;
using GoalTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Microsoft.AspNetCore.Identity;

namespace GoalTracker.Controllers
{
    /// <summary>
    /// Manages operations related to progress tracking goals, including CRUD functionality.
    /// </summary>
    public class ProgressGoalController : Controller
    {
        private readonly GoalDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        /// <summary>
        /// Initialises a new instance of the ProgressGoalController class.
        /// </summary>
        /// <param name="context">The database context for accessing the progress tracking goals and related data.</param>
        public ProgressGoalController(GoalDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }



        /// <summary>
        /// Displays the list of all progress tracking goals.
        /// </summary>
        /// <returns>The view displaying all progress goals with their logs.</returns>
        public IActionResult Index()
        {
            // Check if the user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);

                // Fetch the goals that belong to the logged-in user
                var goals = _context.ProgressTrackingGoals.Where(g => g.UserId == userId).ToList();

                // If no goals exist, show a message
                if (goals == null || goals.Count == 0)
                {
                    ViewData["NoGoalsMessage"] = "You haven't created any goals yet. Start by adding one!";
                }

                // Pass the goals to the view
                return View("~/Views/Progress/Index.cshtml", goals);
            }
            else
            {
                return Redirect("~/Identity/Account/Login");
            }
        }



        /// <summary>
        /// Displays the form to create a new progress tracking goal.
        /// </summary>
        /// <returns>The view for creating a new progress goal.</returns>
        public IActionResult Create()
        {
            return View("~/Views/Progress/Create.cshtml");
        }



        /// <summary>
        /// Creates a new progress tracking goal in the database.
        /// </summary>
        /// <param name="progressGoal">The progress tracking goal to create.</param>
        /// <returns>A redirection to the Index view if creation is successful, or the current view if validation fails.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProgressTrackingGoal progressTrackingGoal)
        {

            if (!ModelState.IsValid)
            {

                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(progressTrackingGoal);


            }

            // Add the new goal along with its logs to the database
            _context.ProgressTrackingGoals.Add(progressTrackingGoal);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index)); // Redirect to the Index page after saving
        }
        



        /// <summary>
        /// Displays the form to edit an existing progress tracking goal.
        /// </summary>
        /// <param name="id">The ID of the progress tracking goal to edit.</param>
        /// <returns>The view for editing an existing progress goal.</returns>
        public IActionResult Edit(int id)
        {
            var progressGoal = _context.ProgressTrackingGoals
                                       .Include(pg => pg.ProgressLogs)  // Include ProgressLogs
                                       .FirstOrDefault(pg => pg.Id == id);

            if (progressGoal == null)
            {
                return NotFound();  // If no goal found with that ID, return a 404 page
            }

            return View("~/Views/Progress/Edit.cshtml", progressGoal);  // Return the view with the existing goal
        }



        /// <summary>
        /// Updates an existing progress tracking goal in the database.
        /// </summary>
        /// <param name="id">The ID of the progress tracking goal to edit.</param>
        /// <param name="progressGoal">The progress tracking goal with updated data.</param>
        /// <returns>A redirection to the Index view if update is successful, or the current view if validation fails.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ProgressTrackingGoal progressGoal)
        {
            if (id != progressGoal.Id)
            {
                return NotFound();  // If the IDs don't match, return a 404 page
            }

            var userId = _userManager.GetUserId(User); // Get logged-in user's ID
            var existingGoal = _context.ProgressTrackingGoals.AsNoTracking().FirstOrDefault(g => g.Id == id);

            if (existingGoal == null)
            {
                return NotFound(); 
            }

            if (existingGoal.UserId != userId)
            {
                return Forbid(); 
            }
            
            // Preserve the UserId
            progressGoal.UserId = existingGoal.UserId;


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(progressGoal);  
                    _context.SaveChanges();  
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.ProgressTrackingGoals.Any(e => e.Id == progressGoal.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));  // Redirect back to the Index page after editing
            }

            return View("Progress/Edit", progressGoal);  // Return the form again if validation fails
        }



        /// <summary>
        /// Displays the details of a specific progress tracking goal.
        /// </summary>
        /// <param name="id">The ID of the progress tracking goal to view.</param>
        /// <returns>The view displaying the details of the progress goal.</returns>
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


        /// <summary>
        /// Displays the confirmation page for deleting a specific progress tracking goal.
        /// </summary>
        /// <param name="id">The ID of the progress tracking goal to delete.</param>
        /// <returns>The view confirming the deletion of the progress goal.</returns>

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



        /// <summary>
        /// Deletes a specific progress tracking goal from the database.
        /// </summary>
        /// <param name="id">The ID of the progress tracking goal to delete.</param>
        /// <returns>A redirection to the Index view after successful deletion.</returns>
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


        /// <summary>
        /// Increments the current amount of progress for a specific goal.
        /// </summary>
        /// <param name="id">The ID of the progress tracking goal to increment.</param>
        /// <returns>A redirection to the Index view after the increment.</returns>
        [HttpPost]
        public IActionResult Increment(int id)
        {
            var goal = _context.ProgressTrackingGoals.FirstOrDefault(pg => pg.Id == id);
            if (goal != null)
            {
                goal.CurrentAmount++; // Increment the goal's current amount
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index)); // Redirect back to the index view
        }

        /// <summary>
        /// Decrements the current amount of progress for a specific goal.
        /// </summary>
        /// <param name="id">The ID of the progress tracking goal to decrement.</param>
        /// <returns>A redirection to the Index view after the decrement.</returns>
        [HttpPost]
        public IActionResult Decrement(int id)
        {
            var goal = _context.ProgressTrackingGoals.FirstOrDefault(pg => pg.Id == id);
            if (goal != null && goal.CurrentAmount > 0)
            {
                goal.CurrentAmount--; // Decrement the goal's current amount (ensure it doesn't go below zero)
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index)); // Redirect back to the index view
        }


    }
}


