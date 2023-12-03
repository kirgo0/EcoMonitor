using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO.UserService
{
    public class LoginUserDTO
    {
        [StringLength(50, MinimumLength = 4)]
        public string? UserName { get; set; }
        [EmailAddress]
        [StringLength(50)]
        public string? Email { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; }
    }
}
