using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO.UserService
{
    public class NarrowUserDTO
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
