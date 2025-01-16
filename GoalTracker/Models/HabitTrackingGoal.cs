using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GoalTracker.Models
{
    public class HabitTrackingGoal : BaseGoal
    {
        public HabitFrequency Frequency { get; set; }

        [ValidateNever]
        public List<HabitDailyLog> DailyLogs { get; set; } = new List<HabitDailyLog>();

        public enum HabitFrequency
        {
            Daily,
            Weekly,
            Monthly
        }
    }

}


