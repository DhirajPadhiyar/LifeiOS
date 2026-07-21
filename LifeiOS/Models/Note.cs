using System.ComponentModel.DataAnnotations;

namespace LifeiOS.Models
{
    public class Note
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
