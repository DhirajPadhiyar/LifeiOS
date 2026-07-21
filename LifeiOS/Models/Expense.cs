using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LifeiOS.Models
{
    public class Expense
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        [Required]
        public decimal Amount { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Category { get; set; } = string.Empty;
        [Required]
        public DateTime ExpenseDate { get; set; }
    }
}
