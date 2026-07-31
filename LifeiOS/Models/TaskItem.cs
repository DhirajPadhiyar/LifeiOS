using System.ComponentModel.DataAnnotations;

namespace LifeiOS.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Task title is required.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 150 characters.")]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        [DataType(DataType.MultilineText)]
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