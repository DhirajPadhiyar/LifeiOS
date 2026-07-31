using System.ComponentModel.DataAnnotations;

namespace LifeiOS.Models
{
    public class Note
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(150, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Content is required.")]
        [StringLength(5000)]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; } = string.Empty;

        [StringLength(50)]
        public string? Category { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }
    }
}
