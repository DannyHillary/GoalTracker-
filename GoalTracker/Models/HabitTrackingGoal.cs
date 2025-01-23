using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GoalTracker.Models
{
    /// <summary>
    /// Represents a Habit Tracking Goal in the Goal Tracker application.
    /// Inherits from <see cref="BaseGoal"/>.
    /// </summary>
    public class HabitTrackingGoal : BaseGoal
    {
        /// <summary>
        /// Gets or sets the frequency of the habit goal.
        /// </summary>
        /// <remarks>
        /// The frequency determines how often the habit is tracked, which could be:
        /// Daily, Weekly, or Monthly.
        public HabitFrequency Frequency { get; set; }

        /// <summary>
        /// Gets or sets the list of daily logs associated with the habit tracking goal.
        /// </summary>
        /// <remarks>
        /// This property is marked with <see cref="ValidateNever"/> to avoid model validation 
        /// when binding data to the view.
        /// </remarks>
        [ValidateNever]
        public List<HabitDailyLog> DailyLogs { get; set; } = new List<HabitDailyLog>();


        /// <summary>
        /// Represents the possible frequencies for a habit tracking goal.
        /// </summary>
        public enum HabitFrequency
        {
            /// <summary>
            /// The habit should be tracked on a daily basis.
            /// </summary>
            Daily,

            /// <summary>
            /// The habit should be tracked on a weekly basis.
            /// </summary>
            Weekly,

            /// <summary>
            /// The habit should be tracked on a monthly basis.
            /// </summary>
            Monthly
        }
    }

}


