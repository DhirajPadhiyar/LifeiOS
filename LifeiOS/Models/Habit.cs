using System.ComponentModel.DataAnnotations;

namespace LifeiOS.Models
{
    public class Habit
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [StringLength(300)]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Frequency")]
        public HabitFrequency Frequency { get; set; } = HabitFrequency.Daily;

        public int CurrentStreak { get; set; } = 0;

        public DateTime? LastCompletedDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public enum HabitFrequency
    {
        Daily,
        Weekly
    }
}