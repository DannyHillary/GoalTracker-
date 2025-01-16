namespace GoalTracker.Models
{
    public class ProgressLog
    {
        // Primary key
        public int Id { get; set; }

        // Foreign key for the related ProgressTrackingGoal
        public int ProgressTrackingGoalId { get; set; }

        // Date when the progress was logged
        public DateTime DateLogged { get; set; }

        // The value added to the progress goal
        public decimal ValueAdded { get; set; }

        // Navigation property to the related ProgressTrackingGoal
        public ProgressTrackingGoal ProgressTrackingGoal { get; set; }
    }
}

