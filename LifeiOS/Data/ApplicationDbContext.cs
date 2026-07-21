using LifeiOS.Models;
using Microsoft.EntityFrameworkCore;

namespace LifeiOS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Habit> Habits { get; set; }
        public DbSet<Goal> Goals { get; set; } 
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<CalenderEvent> CalenderEvents { get; set; }
    }
}
