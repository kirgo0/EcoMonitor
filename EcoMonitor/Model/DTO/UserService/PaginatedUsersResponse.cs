using EcoMonitor.Model.DTO.NewsService;
using EcoMonitor.Model.DTO.UserServiceDTO;

namespace EcoMonitor.Model.DTO.UserService
{
    public class PaginatedUsersResponse
    {
        public int remainingRowsCount { get; set; }
        public List<UserDTO> selectedUsers { get; set; }
    }
}
