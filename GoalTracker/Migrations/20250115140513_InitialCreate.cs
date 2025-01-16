using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoalTracker.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HabitTrackingGoals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Frequency = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitTrackingGoals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProgressTrackingGoals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TargetValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrentValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressTrackingGoals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeTrackingGoals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalTimeSpent = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeTrackingGoals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HabitDailyLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HabitTrackingGoalId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitDailyLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HabitDailyLogs_HabitTrackingGoals_HabitTrackingGoalId",
                        column: x => x.HabitTrackingGoalId,
                        principalTable: "HabitTrackingGoals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgressLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgressTrackingGoalId = table.Column<int>(type: "int", nullable: false),
                    DateLogged = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValueAdded = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgressLogs_ProgressTrackingGoals_ProgressTrackingGoalId",
                        column: x => x.ProgressTrackingGoalId,
                        principalTable: "ProgressTrackingGoals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeSessionLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeTrackingGoalId = table.Column<int>(type: "int", nullable: false),
                    SessionStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionEndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSessionLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeSessionLogs_TimeTrackingGoals_TimeTrackingGoalId",
                        column: x => x.TimeTrackingGoalId,
                        principalTable: "TimeTrackingGoals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HabitDailyLogs_HabitTrackingGoalId",
                table: "HabitDailyLogs",
                column: "HabitTrackingGoalId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressLogs_ProgressTrackingGoalId",
                table: "ProgressLogs",
                column: "ProgressTrackingGoalId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSessionLogs_TimeTrackingGoalId",
                table: "TimeSessionLogs",
                column: "TimeTrackingGoalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HabitDailyLogs");

            migrationBuilder.DropTable(
                name: "ProgressLogs");

            migrationBuilder.DropTable(
                name: "TimeSessionLogs");

            migrationBuilder.DropTable(
                name: "HabitTrackingGoals");

            migrationBuilder.DropTable(
                name: "ProgressTrackingGoals");

            migrationBuilder.DropTable(
                name: "TimeTrackingGoals");
        }
    }
}
