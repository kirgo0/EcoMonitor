using EcoMonitor.Model.DTO.UserServiceDTO;

namespace EcoMonitor.Model.DTO.UserService
{
    public class LoginResposeDTO : UserDTO
    {
        public string Token { get; set; }
    }
}
