using Microsoft.AspNetCore.Mvc;
using GoalTracker.Models;
using Microsoft.EntityFrameworkCore;


namespace GoalTracker.Controllers
{
    /// <summary>
    /// Manages operations related to habit tracking goals, including CRUD functionality and marking completion.
    /// </summary>
    public class HabitTrackingGoalController : Controller
    {
        private readonly GoalDbContext _context;



        /// <summary>
        /// Initialises a new instance of the HabitTrackingGoalController class.
        /// </summary>
        /// <param name="context">The database context for accessing habit tracking goals and related data.</param>
        public HabitTrackingGoalController(GoalDbContext context)
        {
            _context = context;
            
        }


        /// <summary>
        /// Displays the list of all habit tracking goals.
        /// </summary>
        /// <returns>The view displaying all habit tracking goals.</returns>
        public IActionResult Index()
        {
            ViewData["ActivePage"] = "HabitTrackingGoals";
            var goals = _context.HabitTrackingGoals.ToList();  // Fetch data from the DB

            // Ensure that the list is never null
            if (goals == null)
            {
                goals = new List<HabitTrackingGoal>();
            }

            return View(goals);  // Pass the list (even if it's empty) to the view
        }


        /// <summary>
        /// Displays the form to create a new habit tracking goal.
        /// </summary>
        /// <returns>The view for creating a new habit tracking goal.</returns>
        public IActionResult Create()
        {
            return View(); // Display the form for creating a new HabitTrackingGoal
        }



        /// <summary>
        /// Creates a new habit tracking goal in the database.
        /// </summary>
        /// <param name="habitTrackingGoal">The habit tracking goal to create.</param>
        /// <returns>A redirection to the Index view if creation is successful, or the current view if validation fails.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HabitTrackingGoal habitTrackingGoal)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(habitTrackingGoal);
            }

            // Call the method to generate habit logs
            GenerateHabitLogs(habitTrackingGoal);

            // Add the new goal along with its logs to the database
            _context.HabitTrackingGoals.Add(habitTrackingGoal);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index)); // Redirect to the Index page after saving
        }



        /// <summary>
        /// Displays the form to edit an existing habit tracking goal.
        /// </summary>
        /// <param name="id">The ID of the habit tracking goal to edit.</param>
        /// <returns>The view for editing an existing habit tracking goal.</returns>
        public IActionResult Edit(int id)
        {
            var goal = _context.HabitTrackingGoals.Find(id);
            if (goal == null)
            {
                return NotFound(); // Return NotFound if the goal is not found
            }
            return View(goal); // Display the Edit form with the existing data
        }


        /// <summary>
        /// Updates an existing habit tracking goal in the database.
        /// </summary>
        /// <param name="id">The ID of the habit tracking goal to edit.</param>
        /// <param name="habitTrackingGoal">The habit tracking goal with updated data.</param>
        /// <returns>A redirection to the Index view if update is successful, or the current view if validation fails.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, HabitTrackingGoal habitTrackingGoal)
        {
            if (id != habitTrackingGoal.Id)
            {
                return NotFound(); // Ensure the ID in the route matches the ID in the model
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Call the method to regenerate habit logs
                    GenerateHabitLogs(habitTrackingGoal);

                    // Update the goal and logs in the database
                    _context.HabitTrackingGoals.Update(habitTrackingGoal);
                    _context.SaveChanges(); // Save the updated goal to the database
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.HabitTrackingGoals.Any(e => e.Id == habitTrackingGoal.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index)); // Redirect to the Index view after saving
            }

            return View(habitTrackingGoal); // Return the current model if validation fails
        }


        /// <summary>
        /// Displays the confirmation page for deleting a specific habit tracking goal.
        /// </summary>
        /// <param name="id">The ID of the habit tracking goal to delete.</param>
        /// <returns>The view confirming the deletion of the habit tracking goal.</returns>
        public IActionResult Delete(int id)
        {
            var goal = _context.HabitTrackingGoals.Find(id);
            if (goal == null)
            {
                return NotFound(); // Return NotFound if the goal is not found
            }
            return View(goal); // Display the Delete confirmation view
        }


        /// <summary>
        /// Deletes a specific habit tracking goal from the database.
        /// </summary>
        /// <param name="id">The ID of the habit tracking goal to delete.</param>
        /// <returns>A redirection to the Index view after successful deletion.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var goal = _context.HabitTrackingGoals.Find(id);
            if (goal != null)
            {
                _context.HabitTrackingGoals.Remove(goal);
                _context.SaveChanges(); // Delete the goal and save changes
            }
            return RedirectToAction(nameof(Index)); // Redirect back to Index after deletion
        }


        /// <summary>
        /// Displays the details of a specific habit tracking goal.
        /// </summary>
        /// <param name="id">The ID of the habit tracking goal to view.</param>
        /// <returns>The view displaying the details of the habit tracking goal.</returns>
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound(); // Ensure ID is provided
            }



            // Fetch the goal and its associated daily logs from the database
            var habitTrackingGoal = _context.HabitTrackingGoals
                .Include(g => g.DailyLogs) // Include HabitDailyLogs with the goal
                .FirstOrDefault(m => m.Id == id);

            if (habitTrackingGoal == null)
            {
                
                return NotFound(); // If goal is not found, return NotFound page
            }

          
            return View(habitTrackingGoal); // Pass the goal with logs to the view
        }



        /// <summary>
        /// Generates daily logs for the habit tracking goal based on its frequency.
        /// </summary>
        /// <param name="goal">The habit tracking goal for which to generate logs.</param>
        private void GenerateHabitLogs(HabitTrackingGoal goal)
        {
            // Clear existing logs if the frequency has changed or if there are any previous logs
            goal.DailyLogs.Clear();

            // Generate logs based on the frequency
            if (goal.Frequency == HabitTrackingGoal.HabitFrequency.Daily)
            {
                for (DateTime date = goal.StartDate; date <= goal.EndDate; date = date.AddDays(1))
                {
                    goal.DailyLogs.Add(new HabitDailyLog
                    {
                        Date = date,
                        IsCompleted = false, // Initially not completed
                        HabitTrackingGoalId = goal.Id
                    });
                }
            }
            else if (goal.Frequency == HabitTrackingGoal.HabitFrequency.Weekly)
            {
                for (DateTime date = goal.StartDate; date <= goal.EndDate; date = date.AddDays(7))
                {
                    goal.DailyLogs.Add(new HabitDailyLog
                    {
                        Date = date,
                        IsCompleted = false,
                        HabitTrackingGoalId = goal.Id
                    });
                }
            }
            else if (goal.Frequency == HabitTrackingGoal.HabitFrequency.Monthly)
            {
                for (DateTime date = goal.StartDate; date <= goal.EndDate; date = date.AddMonths(1))
                {
                    goal.DailyLogs.Add(new HabitDailyLog
                    {
                        Date = date,
                        IsCompleted = false,
                        HabitTrackingGoalId = goal.Id
                    });
                }
            }
        }


        /// <summary>
        /// Marks a specific habit as completed for a given date.
        /// </summary>
        /// <param name="habitTrackingGoalId">The ID of the habit tracking goal.</param>
        /// <param name="date">The date to mark as completed.</param>
        /// <returns>A redirection to the Details view for the given habit tracking goal after marking the completion.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkCompleted(int habitTrackingGoalId, DateTime date)
        {
            
            // Find the log entry for the specific date
            var habitLog = _context.HabitDailyLogs
                                  .FirstOrDefault(log => log.HabitTrackingGoalId == habitTrackingGoalId && log.Date.Date == date.Date);

            if (habitLog != null)
            {
                habitLog.IsCompleted = true; // Mark the habit as completed
                _context.SaveChanges();  // Save the changes to the database
                
        }

            // After marking the habit as completed, redirect back to the Habit Details page
            return RedirectToAction("Details", new { id = (int?)habitTrackingGoalId });
        }


    }
}

