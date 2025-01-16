namespace GoalTracker.Models
{
    public class TimeTrackingGoal : BaseGoal
    {
        // Total accumulated time spent on this goal
        public TimeSpan TotalTimeSpent { get; set; }

        // Navigation property for related TimeSessionLog entries
        public List<TimeSessionLog> TimeSessions { get; set; }
    }
}


