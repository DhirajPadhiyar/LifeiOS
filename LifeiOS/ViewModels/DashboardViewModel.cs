using LifeiOS.Models;

namespace LifeiOS.ViewModels
{
    public class DashboardViewModel
    {
        // Summary Cards
        public int TotalTasks { get; set; }
        public int TotalNotes { get; set; }
        public int TotalHabits { get; set; }
        public int TotalGoals { get; set; }
        public decimal TotalExpensesThisMonth { get; set; }
        public int TotalEvents { get; set; }

        // Task Summary
        public int CompletedTasks { get; set; }
        public int PendingTasks { get; set; }
        public int OverdueTasks { get; set; }
        public int InProgressTasks { get; set; }

        // Habit Summary
        public int CompletedHabitsToday { get; set; }

        // Dashboard Lists
        public List<TaskItem> RecentTasks { get; set; } = new();
        public List<Note> LatestNotes { get; set; } = new();
        public List<Goal> Goals { get; set; } = new();
        public List<CalendarEvent> TodayEvents { get; set; } = new();

        // Expense Chart
        public List<decimal> MonthlyExpenses { get; set; } = new();
        public List<string> ExpenseMonths { get; set; } = new();
    }
}