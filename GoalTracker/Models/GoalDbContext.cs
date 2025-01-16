﻿using Microsoft.EntityFrameworkCore;

namespace GoalTracker.Models
{
    public class GoalDbContext : DbContext
    {
        public GoalDbContext(DbContextOptions<GoalDbContext> options) : base(options) { }

        public DbSet<TimeTrackingGoal> TimeTrackingGoals { get; set; }
        public DbSet<TimeSessionLog> TimeSessionLogs { get; set; }
        public DbSet<HabitTrackingGoal> HabitTrackingGoals { get; set; }
        public DbSet<HabitDailyLog> HabitDailyLogs { get; set; }
        public DbSet<ProgressTrackingGoal> ProgressTrackingGoals { get; set; }
        public DbSet<ProgressLog> ProgressLogs { get; set; }

        // Create relationships between Entities
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // HabitTrackingGoal and HabitDailyLog relationship
            modelBuilder.Entity<HabitTrackingGoal>()
                .HasMany(h => h.DailyLogs)
                .WithOne(dl => dl.HabitTrackingGoal)
                .HasForeignKey(dl => dl.HabitTrackingGoalId);

            // TimeTrackingGoal and TimeSessionLog relationship
            modelBuilder.Entity<TimeTrackingGoal>()
                .HasMany(ttg => ttg.TimeSessions)
                .WithOne(ts => ts.TimeTrackingGoal)
                .HasForeignKey(ts => ts.TimeTrackingGoalId);

            // ProgressTrackingGoal and ProgressLog relationship
            modelBuilder.Entity<ProgressTrackingGoal>()
                .HasMany(ptg => ptg.ProgressLogs)
                .WithOne(pl => pl.ProgressTrackingGoal)
                .HasForeignKey(pl => pl.ProgressTrackingGoalId);
        
    }
    }
}

