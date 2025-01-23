using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoalTracker.Models

{

    /// <summary>
    /// Represents the base model for all goal types in the Goal Tracker Application.
    /// </summary>
    public class BaseGoal
    {

        /// <summary>
        /// Gets or sets the unique identifier for the goal.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the User who owns this goal.
        /// </summary>
        [ForeignKey("User")]
        public int UserId { get; set; }


        /// <summary>
        /// Gets or sets the title of the goal.
        /// </summary>
        /// <remarks>
        /// The title is a brief description of the goal and must not exceed 100 characters
        /// </remarks>
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }


        /// <summary>
        /// Gets or sets the description of the goal.
        /// </summary>
        /// <remarks>
        /// The description provides additional details about the goal and must not exceed 500 characters.
        /// </remarks>
        [MaxLength(500)]
        public string Description { get; set; }


        /// <summary>
        /// Gets or sets the start date of the goal
        /// </summary>
        /// <remarks>
        /// The start date marks when the goal becomes active.
        /// </remarks>
        [Required]
        public DateTime StartDate { get; set; }


        /// <summary>
        /// Gets or sets the end date of the goal
        /// </summary>
        /// <remarks>
        /// The end date marks when the goal is expected to be completed.
        /// </remarks>
        [Required]
        public DateTime? EndDate { get; set; }


        /// <summary>
        /// Gets or sets the category type of the goal.
        /// </summary>
        /// <remarks>
        /// The category determines how the goal is tracked.
        /// </remarks>
        [Required]
        public GoalCategory GoalType { get; set; }


        /// <summary>
        /// Gets or sets the current status of the goal
        /// </summary>
        /// <remarks>
        /// Status reflects the progress of the goal, such as not started, in progress or completed. 
        /// </remarks>
        [Required]
        public GoalStatus Status { get; set; }
    }

    /// <summary>
    /// Represents the possible Category for a goal.
    /// </summary>
    public enum GoalCategory
    {
        /// <summary>
        /// A goal that tracks measurable progress over time (e.g., weight loss, savings).
        /// </summary>
        ProgressTracking,

        /// <summary>
        /// A goal that tracks recurring habits or activities (e.g., daily journaling, exercise).
        /// </summary>
        HabitTracking
    }

    /// <summary>
    /// Represents the possible statuses for a goal.
    /// </summary>
    public enum GoalStatus
    {
        /// <summary>
        /// The goal has not been started yet.
        /// </summary>
        NotStarted,
       
        /// <summary>
        /// The goal is currently in progress.
        /// </summary>
        InProgress,
        
        /// <summary>
        /// The goal has been successfully completed.
        /// </summary>
        Completed
    }
}
