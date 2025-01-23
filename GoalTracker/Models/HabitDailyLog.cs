using GoalTracker.Models;

namespace GoalTracker.Models
{
    /// <summary>
    /// Represents a daily log entry for a habit in the Habit Tracking Goal.
    /// </summary>
    public class HabitDailyLog
    {
        /// <summary>
        /// Gets or sets the unique identifier for the habit log entry.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the related Habit Tracking Goal.
        /// </summary>
        /// <remarks>
        /// This foreign key links the daily log entry to a specific habit tracking goal.
        public int HabitTrackingGoalId { get; set; }

        /// <summary>
        /// Gets or sets the date of the log entry.
        /// </summary>
        /// <remarks>
        /// The date when the habit is logged as either completed or not.
        /// </remarks>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the habit was completed on the specified date.
        /// </summary>
        /// <remarks>
        /// A boolean value that represents if the habit was successfully completed on that specific day.
        /// </remarks>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Navigation property to the related HabitTrackingGoal.
        /// </summary>
        public HabitTrackingGoal HabitTrackingGoal { get; set; }
    }
}

