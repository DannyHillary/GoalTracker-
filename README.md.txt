# Goal Tracker

Goal Tracker is a habit and goal-tracking web application built with ASP.NET Core MVC and Entity Framework. 
It allows users to create, manage, and monitor their goals with daily logs to track progress.

## Features
- Create, edit, and delete habit-tracking goals.
- Log daily progress for each goal.
- Mark goals or tasks as completed.
- View detailed goal progress in a user-friendly interface.

## Technologies Used
- **Frontend:** Razor Pages, HTML, CSS, Bootstrap
- **Backend:** ASP.NET Core MVC, C#
- **Database:** Entity Framework Core with SQL Server
- **Tools:** Visual Studio

---

## Getting Started

Follow these instructions to set up the project locally on your machine.

### Prerequisites
Ensure you have the following installed:
- [.NET SDK 8.0](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (or a compatible IDE, Visual Studio Code will require a lot of package downloads)

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/DannyHillary/GoalTracker-.git


2. Navigate to the project Directory:

cd GoalTracker


3. Set up the database:

- Update the connection string in appsettings.json to match your SQL Server configuration.
- Run the following bash command to apply the migrations: dotnet ef database update

4. Build and run the project:

- dotnet build

- dotnet run

- Open the browser to where it is running e.g https://localhost.  It should say where it is running when you run the build.



Future Enhancements

Here are some features we plan to add:

Unit and integration Testing 
User Authentication: Allow users to create accounts and manage their goals securely.
Progress Charts: Visualise goal progress with charts.
Gamification: Reward system for completeing goals (e.g badges, points, leaderboard, Streaks). This will hopefully encourage accountability.
Ai powered insights.
Database Improvements
UI enhancements
Deploy to the Cloud.