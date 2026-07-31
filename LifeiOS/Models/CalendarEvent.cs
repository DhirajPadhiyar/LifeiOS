using System.ComponentModel.DataAnnotations;

namespace LifeiOS.Models
{
    public class CalendarEvent
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public bool IsAllDay { get; set; }

        [StringLength(50)]
        public string? Color { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}