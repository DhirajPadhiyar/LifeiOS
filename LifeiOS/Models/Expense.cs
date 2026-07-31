using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LifeiOS.Models
{
    public class Expense
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 9999999999999999.99)]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(100)]
        public string Category { get; set; } = string.Empty;

        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string? Notes { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ExpenseDate { get; set; } = DateTime.Today;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}