using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoalTracker.Controllers;
using GoalTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

public class ProgressGoalControllerTests
{
    private readonly ProgressGoalController _controller;
    private readonly GoalDbContext _context;

    public ProgressGoalControllerTests()
    {
        // Set up an in-memory database for testing
        var options = new DbContextOptionsBuilder<GoalDbContext>()
            .UseInMemoryDatabase($"GoalTrackerTestDb_{Guid.NewGuid()}")
            .Options;

        _context = new GoalDbContext(options);
        _controller = new ProgressGoalController(_context);

        // Seed the database with test data
        SeedDatabase(_context);
    }

    private void SeedDatabase(GoalDbContext context)
    {
        // Clear existing data
        context.ProgressTrackingGoals.RemoveRange(context.ProgressTrackingGoals);
        context.SaveChanges();

        // Seed ProgressTrackingGoals and ProgressLogs
        context.ProgressTrackingGoals.Add(new ProgressTrackingGoal
        {
            Id = 1,
            Title = "Goal 1",
            Description = "Description 1",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddMonths(1),
            CurrentAmount = 2,
            TargetAmount = 10,
            ProgressLogs = new List<ProgressLog>
            {
                new ProgressLog { Id = 1, ProgressTrackingGoalId = 1, DateLogged = DateTime.Now.AddDays(-1), ValueAdded = 2 }
            }
        });

        context.ProgressTrackingGoals.Add(new ProgressTrackingGoal
        {
            Id = 2,
            Title = "Goal 2",
            Description = "Description 2",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddMonths(1),
            CurrentAmount = 5,
            TargetAmount = 20,
            ProgressLogs = new List<ProgressLog>
            {
                new ProgressLog { Id = 2, ProgressTrackingGoalId = 2, DateLogged = DateTime.Now.AddDays(-2), ValueAdded = 5 }
            }
        });

        context.SaveChanges();
    }

    [Fact]
    public void Index_ReturnsViewResult_WithListOfGoals()
    {
        // Act
        var result = _controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<List<ProgressTrackingGoal>>(viewResult.Model);
        Assert.Equal(2, model.Count); // Ensure two goals are returned
    }

    [Fact]
    public void Create_Post_CreatesNewGoal_AndRedirectsToIndex()
    {
        // Arrange
        var newGoal = new ProgressTrackingGoal
        {
            Title = "New Goal",
            Description = "New Description",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddMonths(1),
            CurrentAmount = 0,
            TargetAmount = 15
        };

        // Act
        var result = _controller.Create(newGoal);

        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        Assert.Contains(_context.ProgressTrackingGoals, g => g.Title == "New Goal"); // Ensure goal is added to DB
    }

    [Fact]
    public void Delete_ReturnsViewResult_WithGoal()
    {
        // Act
        var result = _controller.Delete(1);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<ProgressTrackingGoal>(viewResult.Model);
        Assert.Equal(1, model.Id); // Ensure the correct goal is fetched
    }

    [Fact]
    public void Increment_IncreasesCurrentAmount()
    {
        // Act
        var result = _controller.Increment(1);

        // Assert
        var goal = _context.ProgressTrackingGoals.First(g => g.Id == 1);
        Assert.Equal(3, goal.CurrentAmount); 
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
    }

    [Fact]
    public void Decrement_DecreasesCurrentAmount()
    {
        // Act
        var result = _controller.Decrement(2);

        // Assert
        var goal = _context.ProgressTrackingGoals.First(g => g.Id == 2);
        Assert.Equal(4, goal.CurrentAmount); 
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
    }

    [Fact]
    public void Edit_ReturnsViewResult_WithGoal()
    {
        // Act
        var result = _controller.Edit(1);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<ProgressTrackingGoal>(viewResult.Model);
        Assert.Equal(1, model.Id); // Ensure correct goal is fetched
    }

    [Fact]
    public void DeleteConfirmed_RemovesGoal_AndRedirectsToIndex()
    {
        // Act
        var result = _controller.DeleteConfirmed(1);

        // Assert
        Assert.DoesNotContain(_context.ProgressTrackingGoals, g => g.Id == 1); // Ensure goal is removed
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
    }
}

