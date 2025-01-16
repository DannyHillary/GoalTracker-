using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GoalTracker.Models
{
    public class ProgressTrackingGoal : BaseGoal
    {
        // Additional fields specific to Progress Goal
       
        public double TargetAmount { get; set; }  // Target value for progress

        public double CurrentAmount { get; set; }  // Current progress made

        // Method to calculate the progress percentage
        public double ProgressPercentage => (CurrentAmount / TargetAmount) * 100;

        // Navigation property for related ProgressLog entries
        [ValidateNever]
        public List<ProgressLog> ProgressLogs { get; set; } = new List<ProgressLog>();
    }
}




