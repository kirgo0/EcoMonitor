using EcoMonitor.Model;
using EcoMonitor.Model.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace EcoMonitor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserDataController : ControllerBase
    {
        private APIResponse _response;
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public UserDataController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _response = new APIResponse();
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet("GetRoles")]
        public ActionResult<APIResponse> GetAllRoles()
        {
            var roles = _roleManager.Roles;
            if (roles == null || roles.Count() == 0)
            {
                return NoContent();
            }
            _response.Result = roles.ToList();
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpPost("CreateRole")]
        public async Task<ActionResult<APIResponse>> CreateRole([FromQuery] string roleName)
        {
            if (roleName.IsNullOrEmpty())
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("The role name you entered is empty");
                return BadRequest(_response);
            }

            if (await _roleManager.RoleExistsAsync(roleName))
            {
                _response.StatusCode = HttpStatusCode.Conflict;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add($"Role {roleName} is already exists!");
                return Conflict(_response);
            }

            var newRole = new IdentityRole(roleName);
            var result = await _roleManager.CreateAsync(newRole);

            if (result.Succeeded)
            {
                _response.StatusCode = HttpStatusCode.Created;
                return Ok(_response);
            } else
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode(500, _response);
            }
        } 

        [HttpDelete("DeleteRole")]
        public async Task<ActionResult<APIResponse>> DeleteRole([FromQuery] string roleId) 
        {
            if(roleId.IsNullOrEmpty())
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("The role Id you entered is empty");
                return BadRequest(_response);
            }
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if(result.Succeeded)
                {
                    _response.StatusCode=HttpStatusCode.NoContent;
                    return Ok(_response);
                } else
                {
                    _response.StatusCode = HttpStatusCode.InternalServerError;
                    _response.IsSuccess = false;
                    return StatusCode(500, _response);
                }

            } else
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<APIResponse>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();

            if(users.Count() == 0)
            {
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }

            var userList = new List<UserDTO>();
            foreach (var user in users)
            {
                userList.Add(
                    new UserDTO() {
                        Id = user.Id,
                        UserName = user.UserName,
                        Email = user.Email,
                        Role = await _userManager.GetRolesAsync(user)
                    }
                );
            }
            
            _response.Result = userList;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpGet("GetUser")]
        public async Task<ActionResult<APIResponse>> GetUser([FromQuery] string userId = null, [FromQuery] string userName = null)
        {
            if(userId.IsNullOrEmpty() && userName.IsNullOrEmpty())
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("You must specify the user's Id or user name!");
                return BadRequest(_response);
            }

            IdentityUser user;

            if(!userId.IsNullOrEmpty())
            {
                user = await _userManager.FindByIdAsync(userId);
            } else
            {
                user = await _userManager.FindByNameAsync(userName);
            }

            if(user != null)
            {
                _response.Result = new UserDTO()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = await _userManager.GetRolesAsync(user)
                };
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }

            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.NotFound;
            return NotFound(_response);
        }

        [HttpDelete("DeleteUser")]
        public async Task<ActionResult<APIResponse>> DeleteUser([FromQuery] string userId)
        {
            if (userId.IsNullOrEmpty())
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("You must specify the user's Id!");
                return BadRequest(_response);
            }

            var user = await _userManager.FindByIdAsync(userId);

            if(user == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }

            var result = await _userManager.DeleteAsync(user);

            if(result.Succeeded)
            {
                return NoContent();
            } else
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                return StatusCode(500, _response);
            }
        }

        [HttpPut("AddUserRole")]
        public async Task<ActionResult<APIResponse>> AddUserRole([FromQuery] string userId, [FromQuery] string userRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var role = await _roleManager.FindByNameAsync(userRole);

            if(user == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add($"No user with this id:{userId} was found!");
                _response.IsSuccess = false;
                return NotFound(_response);
            }

            if (role == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add($"Role {userRole} doesn't exists!");
                _response.IsSuccess = false;
                return NotFound(_response);
            }

            var result = await _userManager.AddToRoleAsync(user, userRole);

            if (result.Succeeded) 
            {
                return NoContent();
            } 
            else
            {
                _response.StatusCode = HttpStatusCode.Conflict;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add($"User is already {userRole}");
                return Conflict(_response);
            }
        }

        [HttpPut("DeleteUserRole")]
        public async Task<ActionResult<APIResponse>> DeleteUserRole([FromQuery] string userId, [FromQuery] string userRole)
        {

            var user = await _userManager.FindByIdAsync(userId);
            var role = await _roleManager.FindByNameAsync(userRole);

            if (user == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add($"No user with this id:{userId} was found!");
                _response.IsSuccess = false;
                return NotFound(_response);
            }

            if (role == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add($"Role {userRole} doesn't exists!");
                _response.IsSuccess = false;
                return NotFound(_response);
            }

            var result = await _userManager.RemoveFromRoleAsync(user, userRole);

            if (result.Succeeded)
            {
                return NoContent();
            }
            else
            {
                _response.StatusCode = HttpStatusCode.Conflict;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add($"User does not have a role {userRole}");
                return Conflict(_response);
            }
        }
    }
}
