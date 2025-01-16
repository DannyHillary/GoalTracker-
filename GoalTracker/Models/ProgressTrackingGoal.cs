namespace GoalTracker.Models
{
    public class ProgressTrackingGoal : BaseGoal
    {
        // Numeric goal target value
        public decimal TargetValue { get; set; }

        // Current achieved value
        public decimal CurrentValue { get; set; }

        // Navigation property for related ProgressLog entries
        public List<ProgressLog> ProgressLogs { get; set; }
    }
}



