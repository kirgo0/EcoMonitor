using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO.UserServiceDTO
{
    public class UserDTO
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public IEnumerable<string> Role { get; set; }

    }
}
