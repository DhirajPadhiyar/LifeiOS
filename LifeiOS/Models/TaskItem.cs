using System.ComponentModel.DataAnnotations;

namespace LifeiOS.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;

        public bool IsCompleted { get; set; } = false;

        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }
    }

    public enum TaskPriority
    {
        Low,
        Medium,
        High
    }
}