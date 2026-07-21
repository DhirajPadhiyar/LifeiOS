using System.ComponentModel.DataAnnotations;

namespace LifeiOS.Models
{
    public class Habit
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public bool IsCompletedToday { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
