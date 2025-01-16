using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoalTracker.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHabitTrackingGoalModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TimeTrackingGoals",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "TimeTrackingGoals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "GoalType",
                table: "TimeTrackingGoals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "TimeTrackingGoals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "TimeTrackingGoals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "TimeTrackingGoals",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TimeTrackingGoals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProgressTrackingGoals",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "ProgressTrackingGoals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "GoalType",
                table: "ProgressTrackingGoals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "ProgressTrackingGoals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ProgressTrackingGoals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ProgressTrackingGoals",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ProgressTrackingGoals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "HabitTrackingGoals",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "HabitTrackingGoals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "GoalType",
                table: "HabitTrackingGoals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "HabitTrackingGoals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "HabitTrackingGoals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "HabitTrackingGoals",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "HabitTrackingGoals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "TimeTrackingGoals");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "TimeTrackingGoals");

            migrationBuilder.DropColumn(
                name: "GoalType",
                table: "TimeTrackingGoals");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "TimeTrackingGoals");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "TimeTrackingGoals");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "TimeTrackingGoals");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TimeTrackingGoals");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProgressTrackingGoals");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "ProgressTrackingGoals");

            migrationBuilder.DropColumn(
                name: "GoalType",
                table: "ProgressTrackingGoals");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "ProgressTrackingGoals");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ProgressTrackingGoals");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "ProgressTrackingGoals");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProgressTrackingGoals");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "HabitTrackingGoals");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "HabitTrackingGoals");

            migrationBuilder.DropColumn(
                name: "GoalType",
                table: "HabitTrackingGoals");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "HabitTrackingGoals");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "HabitTrackingGoals");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "HabitTrackingGoals");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "HabitTrackingGoals");
        }
    }
}
