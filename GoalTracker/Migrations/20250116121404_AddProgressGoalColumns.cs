using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoalTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddProgressGoalColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentValue",
                table: "ProgressTrackingGoals");

            migrationBuilder.DropColumn(
                name: "TargetValue",
                table: "ProgressTrackingGoals");

            migrationBuilder.AddColumn<double>(
                name: "CurrentAmount",
                table: "ProgressTrackingGoals",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TargetAmount",
                table: "ProgressTrackingGoals",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentAmount",
                table: "ProgressTrackingGoals");

            migrationBuilder.DropColumn(
                name: "TargetAmount",
                table: "ProgressTrackingGoals");

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentValue",
                table: "ProgressTrackingGoals",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TargetValue",
                table: "ProgressTrackingGoals",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
