using LifeiOS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LifeiOS.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Habit> Habits { get; set; }
        public DbSet<Goal> Goals { get; set; } 
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<CalenderEvent> CalenderEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Goal>()
                .Property(g => g.TargetValue)
                .HasPrecision(18, 2);

            builder.Entity<Goal>()
                .Property(g => g.CurrentValue)
                .HasPrecision(18, 2);
        }
    }
}
