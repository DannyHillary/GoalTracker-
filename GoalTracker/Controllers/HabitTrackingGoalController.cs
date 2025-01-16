using Microsoft.AspNetCore.Mvc;
using GoalTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace GoalTracker.Controllers
{
    public class HabitTrackingGoalController : Controller
    {
        private readonly GoalDbContext _context;

        // Constructor to inject DbContext
        public HabitTrackingGoalController(GoalDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var goals = _context.HabitTrackingGoals.ToList();  // Fetch data from the DB

            // Ensure that the list is never null
            if (goals == null)
            {
                goals = new List<HabitTrackingGoal>();
            }

            return View(goals);  // Pass the list (even if it's empty) to the view
        }

        // GET: HabitTrackingGoal/Create
        public IActionResult Create()
        {
            return View(); // Display the form for creating a new HabitTrackingGoal
        }

        // POST: HabitTrackingGoal/Create
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



            if (ModelState.IsValid)
            {
                _context.HabitTrackingGoals.Add(habitTrackingGoal);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index)); // Redirect to the Index page after saving
            }
            return View(habitTrackingGoal); // If not valid, return to the Create page with the current model
        }

        // GET: HabitTrackingGoal/Edit/5
        public IActionResult Edit(int id)
        {
            var goal = _context.HabitTrackingGoals.Find(id);
            if (goal == null)
            {
                return NotFound(); // Return NotFound if the goal is not found
            }
            return View(goal); // Display the Edit form with the existing data
        }

        // POST: HabitTrackingGoal/Edit/5
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

        // GET: HabitTrackingGoal/Delete/5
        public IActionResult Delete(int id)
        {
            var goal = _context.HabitTrackingGoals.Find(id);
            if (goal == null)
            {
                return NotFound(); // Return NotFound if the goal is not found
            }
            return View(goal); // Display the Delete confirmation view
        }

        // POST: HabitTrackingGoal/Delete/5
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

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habitTrackingGoal = _context.HabitTrackingGoals
                .FirstOrDefault(m => m.Id == id);

            if (habitTrackingGoal == null)
            {
                return NotFound();
            }

            return View(habitTrackingGoal);
        }
    }
}

