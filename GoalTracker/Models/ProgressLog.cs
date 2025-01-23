namespace GoalTracker.Models
{
    /// <summary>
    /// Represents a log entry for progress made towards a ProgressTrackingGoal.
    /// </summary>
    public class ProgressLog
    {
        /// <summary>
        /// Gets or sets the unique identifier for the progress log entry.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the related ProgressTrackingGoal.
        /// </summary>
        /// <remarks>
        /// This foreign key links the progress log entry to a specific progress tracking goal.
        /// </remarks>
        public int ProgressTrackingGoalId { get; set; }

        /// <summary>
        /// Gets or sets the date when the progress was logged.
        /// </summary>
        /// <remarks>
        /// The date represents when progress was recorded for the associated goal.
        /// </remarks>
        public DateTime DateLogged { get; set; }

        /// <summary>
        /// Gets or sets the value added to the progress goal.
        /// </summary>
        /// <remarks>
        /// This represents the amount of progress made on a specific date for the goal.
        /// </remarks>
        public decimal ValueAdded { get; set; }

        /// <summary>
        /// Navigation property to the related ProgressTrackingGoal.
        /// </summary>
        public ProgressTrackingGoal ProgressTrackingGoal { get; set; }
    }
}

