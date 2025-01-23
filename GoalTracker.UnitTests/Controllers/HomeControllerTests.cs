using GoalTracker.Controllers;
using GoalTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GoalTracker.UnitTests.Controllers
{
    public class HomeControllerTests
    {
        private readonly HomeController _controller;
        private readonly GoalDbContext _context;

        public HomeControllerTests()
        {
            // Set up an in-memory database for testing
            var options = new DbContextOptionsBuilder<GoalDbContext>()
                .UseInMemoryDatabase(databaseName: "GoalTrackerTestDb")
                .Options;

            _context = new GoalDbContext(options);
            _controller = new HomeController(_context);

            // Seed the database with data for the tests
            SeedDatabase(_context);
        }

        // Helper method to seed the database with data
        private void SeedDatabase(GoalDbContext context)
        {
            context.HabitTrackingGoals.Add(new HabitTrackingGoal
            {
                Title = "Sample Title",
                Description = "Sample Goal",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                GoalType = GoalCategory.HabitTracking

                
            });

            context.SaveChanges();
        }

        [Fact]
        public void Index_ShouldReturn_ViewResult()
        {
            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Home", _controller.ViewData["ActivePage"]);
        }

        [Fact]
        public void Privacy_ShouldReturn_ViewResult()
        {
            // Act
            var result = _controller.Privacy();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

       
    }
}



