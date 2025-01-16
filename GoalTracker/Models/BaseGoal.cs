using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoalTracker.Models

{
    public class BaseGoal
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }

        [Required]
        public GoalCategory GoalType { get; set; }

        [Required]
        public GoalStatus Status { get; set; }
    }

    public enum GoalCategory
    {
        ProgressTracking,
        HabitTracking,
        TimeTracking
    }

    public enum GoalStatus
    {
        NotStarted,
        InProgress,
        Completed
    }
}
