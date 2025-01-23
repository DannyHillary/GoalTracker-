using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GoalTracker.Models
{
    /// <summary>
    /// Represents a Progress Tracking Goal. Inherits from <see cref="BaseGoal"/>.
    /// </summary>
    public class ProgressTrackingGoal : BaseGoal
    {

        /// <summary>
        /// Gets or sets the target amount for the progress goal.
        /// </summary>
        /// <remarks>
        /// The target amount represents the goal that needs to be achieved.
        /// </remarks>
        public double TargetAmount { get; set; }


        /// <summary>
        /// Gets or sets the current amount of progress made towards the goal.
        /// </summary>
        /// <remarks>
        /// The current amount represents how much progress has been achieved so far.
        /// </remarks>
        public double CurrentAmount { get; set; }


        /// <summary>
        /// Gets the progress percentage, calculated as the ratio of current progress to the target amount.
        /// </summary>
        /// <remarks>
        /// Returns a value between 0 and 100, rounded to two decimal places.
        /// </remarks>
        public double ProgressPercentage
        {
            get
            {
                if (TargetAmount > 0)
                {
                    return Math.Round((double)CurrentAmount / TargetAmount * 100, 2);
                }
                return 0.00;
            }
        }

        /// <summary>
        /// A collection of progress logs related to the goal.
        /// </summary>
        [ValidateNever]
        public List<ProgressLog> ProgressLogs { get; set; } = new List<ProgressLog>();
    }
}




