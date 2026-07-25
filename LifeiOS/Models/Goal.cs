using System.ComponentModel.DataAnnotations;

namespace LifeiOS.Models
{
    public class Goal
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string GoalTitle { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
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