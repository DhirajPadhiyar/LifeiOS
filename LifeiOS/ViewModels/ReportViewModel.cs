namespace LifeiOS.ViewModels
{
    public class ReportViewModel
    {
        public int CompletedTasks { get; set; }

        public int PendingTasks { get; set; }

        public int TotalNotes { get; set; }

        public int TotalGoals { get; set; }

        public double GoalProgress { get; set; }

        public int TotalHabits { get; set; }

        public double HabitCompletion { get; set; }

        public decimal MonthlyExpenses { get; set; }

        public int UpcomingEvents { get; set; }
        public int ProductivityScore { get; set; }

        public string ProductivityLevel { get; set; } = string.Empty;
    }
}
