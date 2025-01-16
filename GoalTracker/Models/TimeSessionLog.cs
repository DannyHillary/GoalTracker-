namespace GoalTracker.Models
{
    public class TimeSessionLog
    {
        // Primary key
        public int Id { get; set; }

        // Foreign key for the related TimeTrackingGoal
        public int TimeTrackingGoalId { get; set; }

        // The start time of the session
        public DateTime SessionStartTime { get; set; }

        // The end time of the session
        public DateTime SessionEndTime { get; set; }

        // The duration of the session (calculated as the difference between start and end times)
        public TimeSpan Duration { get; set; }

        // Navigation property to the related TimeTrackingGoal
        public TimeTrackingGoal TimeTrackingGoal { get; set; }
    }
}
