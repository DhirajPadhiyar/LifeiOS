using System.ComponentModel.DataAnnotations;

namespace LifeiOS.Models
{
    public class CalendarEvent
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public bool IsAllDay { get; set; }

        [MaxLength(50)]
        public string? Color { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}