using System.ComponentModel.DataAnnotations;

namespace LifeiOS.ViewModels
{
    public class ProfileViewModel
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
