using System.ComponentModel.DataAnnotations;

namespace LifeiOS.Models
{
    public class Goal
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string GoalTitle { get; set; } = string.Empty;

        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime TargetDate { get; set; }

        // Optional
        [Range(1, double.MaxValue)]
        public decimal? TargetValue { get; set; }

        // Optional
        [Range(0, double.MaxValue)]
        public decimal CurrentValue { get; set; }

        [Range(0, 100)]
        public int Progress { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}