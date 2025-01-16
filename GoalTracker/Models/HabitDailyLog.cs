using GoalTracker.Models;

public class HabitDailyLog
{
    // Primary key
    public int Id { get; set; }

    // Foreign key for HabitTrackingGoal
    public int HabitTrackingGoalId { get; set; }

    // Date of the log entry
    public DateTime Date { get; set; }

    // Indicates if the habit was completed on this day
    public bool IsCompleted { get; set; }

    // Navigation property to the related HabitTrackingGoal
    public HabitTrackingGoal HabitTrackingGoal { get; set; }
}

