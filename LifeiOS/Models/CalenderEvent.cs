using System.ComponentModel.DataAnnotations;

namespace LifeiOS.Models
{
    public class CalenderEvent
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Title  { get; set; } = string.Empty;
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

    }
}
