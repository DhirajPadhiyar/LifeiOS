using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LifeiOS.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
