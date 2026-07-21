using System.ComponentModel.DataAnnotations;

namespace LifeiOS.Models
{
    public class Goal
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string GoalTitle { get; set; } = string.Empty;
        [Required]
        public DateTime TargetDate { get; set; }
        [Range(0, 100)]
        public int Progress { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
