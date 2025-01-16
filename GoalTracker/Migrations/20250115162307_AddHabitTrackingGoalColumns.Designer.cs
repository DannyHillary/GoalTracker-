﻿// <auto-generated />
using System;
using GoalTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GoalTracker.Migrations
{
    [DbContext(typeof(GoalDbContext))]
    [Migration("20250115162307_AddHabitTrackingGoalColumns")]
    partial class AddHabitTrackingGoalColumns
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GoalTracker.Models.HabitTrackingGoal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Frequency")
                        .HasColumnType("int");

                    b.Property<int>("GoalType")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("HabitTrackingGoals");
                });

            modelBuilder.Entity("GoalTracker.Models.ProgressLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateLogged")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProgressTrackingGoalId")
                        .HasColumnType("int");

                    b.Property<decimal>("ValueAdded")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ProgressTrackingGoalId");

                    b.ToTable("ProgressLogs");
                });

            modelBuilder.Entity("GoalTracker.Models.ProgressTrackingGoal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("CurrentValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("GoalType")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("TargetValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ProgressTrackingGoals");
                });

            modelBuilder.Entity("GoalTracker.Models.TimeSessionLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<DateTime>("SessionEndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SessionStartTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("TimeTrackingGoalId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TimeTrackingGoalId");

                    b.ToTable("TimeSessionLogs");
                });

            modelBuilder.Entity("GoalTracker.Models.TimeTrackingGoal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("GoalType")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<TimeSpan>("TotalTimeSpent")
                        .HasColumnType("time");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TimeTrackingGoals");
                });

            modelBuilder.Entity("HabitDailyLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("HabitTrackingGoalId")
                        .HasColumnType("int");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("HabitTrackingGoalId");

                    b.ToTable("HabitDailyLogs");
                });

            modelBuilder.Entity("GoalTracker.Models.ProgressLog", b =>
                {
                    b.HasOne("GoalTracker.Models.ProgressTrackingGoal", "ProgressTrackingGoal")
                        .WithMany("ProgressLogs")
                        .HasForeignKey("ProgressTrackingGoalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProgressTrackingGoal");
                });

            modelBuilder.Entity("GoalTracker.Models.TimeSessionLog", b =>
                {
                    b.HasOne("GoalTracker.Models.TimeTrackingGoal", "TimeTrackingGoal")
                        .WithMany("TimeSessions")
                        .HasForeignKey("TimeTrackingGoalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TimeTrackingGoal");
                });

            modelBuilder.Entity("HabitDailyLog", b =>
                {
                    b.HasOne("GoalTracker.Models.HabitTrackingGoal", "HabitTrackingGoal")
                        .WithMany("DailyLogs")
                        .HasForeignKey("HabitTrackingGoalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HabitTrackingGoal");
                });

            modelBuilder.Entity("GoalTracker.Models.HabitTrackingGoal", b =>
                {
                    b.Navigation("DailyLogs");
                });

            modelBuilder.Entity("GoalTracker.Models.ProgressTrackingGoal", b =>
                {
                    b.Navigation("ProgressLogs");
                });

            modelBuilder.Entity("GoalTracker.Models.TimeTrackingGoal", b =>
                {
                    b.Navigation("TimeSessions");
                });
#pragma warning restore 612, 618
        }
    }
}
