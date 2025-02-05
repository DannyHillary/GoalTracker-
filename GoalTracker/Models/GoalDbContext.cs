using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GoalTracker.Models
{
    /// <summary>
    /// Represents the database context for the Goal Tracker application.
    /// </summary>
    public class GoalDbContext : IdentityDbContext<IdentityUser>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="GoalDbContext"/> class.
        /// </summary>
        /// <param name="options">The options to configure the database context.</param>
        public GoalDbContext(DbContextOptions<GoalDbContext> options) : base(options) {}
        /// <summary>
        /// Gets or sets the HabitTrackingGoals in the database.
        /// </summary>
        public DbSet<HabitTrackingGoal> HabitTrackingGoals { get; set; }

        /// <summary>
        /// Gets or sets the HabitDailyLogs in the database.
        /// </summary>
        public DbSet<HabitDailyLog> HabitDailyLogs { get; set; }

        /// <summary>
        /// Gets or sets the ProgressTrackingGoals in the database.
        /// </summary>
        public DbSet<ProgressTrackingGoal> ProgressTrackingGoals { get; set; }

        /// <summary>
        /// Gets or sets the ProgressLogs in the database.
        /// </summary>
        public DbSet<ProgressLog> ProgressLogs { get; set; }


        /// <summary>
        /// Configures relationships between entities using Fluent API.
        /// </summary>
        /// <param name="modelBuilder">The model builder used to configure the model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // HabitTrackingGoal and HabitDailyLog relationship
            modelBuilder.Entity<HabitTrackingGoal>()
                .HasMany(h => h.DailyLogs)
                .WithOne(dl => dl.HabitTrackingGoal)
                .HasForeignKey(dl => dl.HabitTrackingGoalId);


            // ProgressTrackingGoal and ProgressLog relationship
            modelBuilder.Entity<ProgressTrackingGoal>()
                .HasMany(ptg => ptg.ProgressLogs)
                .WithOne(pl => pl.ProgressTrackingGoal)
                .HasForeignKey(pl => pl.ProgressTrackingGoalId);

            // Defined the precision scale for 'ValueAdded' decimal column in 'ProgressLog'
            modelBuilder.Entity<ProgressLog>()
                .Property(p => p.ValueAdded)
                .HasColumnType("decimal(18,2)");

            // Defined the relationship between User and Goals
            modelBuilder.Entity<HabitTrackingGoal>()
                .HasOne(h => h.User)
                .WithMany()
                .HasForeignKey(h => h.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProgressTrackingGoal>()
            .HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

