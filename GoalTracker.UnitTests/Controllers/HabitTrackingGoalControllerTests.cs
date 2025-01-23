using GoalTracker.Controllers;
using GoalTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class HabitTrackingGoalControllerTests
{
    private readonly HabitTrackingGoalController _controller;
    private readonly GoalDbContext _context;

    public HabitTrackingGoalControllerTests()
    {
        // Use a unique database name for every test case
        var options = new DbContextOptionsBuilder<GoalDbContext>()
            .UseInMemoryDatabase($"GoalTrackerTestDb_{Guid.NewGuid()}")
            .Options;

        _context = new GoalDbContext(options);
        _controller = new HabitTrackingGoalController(_context);

        // Seed the database with test data
        SeedDatabase(_context);
    }

    // Helper method to seed the database with data
    private void SeedDatabase(GoalDbContext context)
    {
        // Clear existing data to avoid conflicts
        context.HabitTrackingGoals.RemoveRange(context.HabitTrackingGoals);
        context.HabitDailyLogs.RemoveRange(context.HabitDailyLogs);
        context.SaveChanges();
        context.HabitTrackingGoals.Add(new HabitTrackingGoal

        {
            Id = 1, 
            Title = "Test Goal 1",
            Description = "Test description 1",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddMonths(1),
            Frequency = HabitTrackingGoal.HabitFrequency.Daily,
            DailyLogs = new List<HabitDailyLog>
            {
                new HabitDailyLog { Date = DateTime.Now, IsCompleted = false, HabitTrackingGoalId = 1 }
            }
        });

        context.HabitTrackingGoals.Add(new HabitTrackingGoal
        {
            Id = 2, 
            Title = "Test Goal 2",
            Description = "Test description 2",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddMonths(2),
            Frequency = HabitTrackingGoal.HabitFrequency.Weekly,
            DailyLogs = new List<HabitDailyLog>
            {
                new HabitDailyLog { Date = DateTime.Now.AddDays(7), IsCompleted = false, HabitTrackingGoalId = 2 }
            }
        });

        // Save changes to the in-memory database
        context.SaveChanges();
    }

    
    [Fact]
    public void Index_ReturnsViewResult_WithListOfGoals()
    {
        // Act
        var result = _controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<List<HabitTrackingGoal>>(viewResult.Model);
        Assert.Equal(2, model.Count);  // Assert that there are two goals in the model (from seed data)
    }

    
    [Fact]
    public void Create_ReturnsViewResult()
    {
        // Act
        var result = _controller.Create();

        // Assert
        Assert.IsType<ViewResult>(result);
    }

    
    [Fact]
    public void Create_Post_CreatesNewGoal_AndRedirectsToIndex()
    {
        // Arrange
        var newGoal = new HabitTrackingGoal
        {
            Title = "New Goal",
            Description = "New Description",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddMonths(1),
            Frequency = HabitTrackingGoal.HabitFrequency.Daily
        };

        // Act
        var result = _controller.Create(newGoal);

        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
    }

    
    [Fact]
    public void Edit_ReturnsViewResult_WithGoal()
    {
        // Act
        var result = _controller.Edit(1); // ID of the first goal

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<HabitTrackingGoal>(viewResult.Model);
        Assert.Equal(1, model.Id); 
    }


    
    [Fact]
    public void DeleteConfirmed_RemovesGoal_AndRedirectsToIndex()
    {
        // Arrange
        var goal = _context.HabitTrackingGoals.First(); // Get the first goal

        // Act
        var result = _controller.DeleteConfirmed(goal.Id);

        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName); // Check if it redirects to the Index page
    }

    
    [Fact]
    public void MarkCompleted_MarksHabitAsCompleted()
    {
        // Arrange
        var habitTrackingGoalId = 1;
        var date = DateTime.Now;
        var habitLog = _context.HabitDailyLogs.FirstOrDefault(log => log.HabitTrackingGoalId == habitTrackingGoalId && log.Date.Date == date.Date);

        // Act
        var result = _controller.MarkCompleted(habitTrackingGoalId, date);

        // Assert
        Assert.True(habitLog.IsCompleted);  // Ensure that the habit log is marked as completed
    }
}


