using EcoMonitor.Model.APIResponses;
using EcoMonitor.Model.DTO.UserService;
using EcoMonitor.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace EcoMonitor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        protected APIResponse _response;
        private IUserService _userService;

        public AuthController(IUserService userService)
        {
            _response = new APIResponse();
            _userService = userService;
        }

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> RegisterAsync([FromBody] RegisterUserDTO userDto)
        {
            if(!ModelState.IsValid)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false; 
                foreach (var modelError in ModelState.Values)
                {
                    foreach (ModelError error in modelError.Errors)
                    {
                        _response.ErrorMessages.Add(error.ErrorMessage);
                    }
                }
                return BadRequest(_response);
            }

            var result = await _userService.RergisterUserAsync(userDto);

            if(result == null)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                return StatusCode(500, result);
            }

            switch(result.StatusCode) {
                case HttpStatusCode.Created: return Created("https://localhost:7001/api/auth/Login",result); 
                case HttpStatusCode.BadRequest: return BadRequest(result);
                case HttpStatusCode.Conflict: return Conflict(result);
                default: return StatusCode(500);
            }
        }


        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> LoginAsync([FromBody] LoginUserDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                foreach (var modelError in ModelState.Values)
                {
                    foreach (ModelError error in modelError.Errors)
                    {
                        _response.ErrorMessages.Add(error.ErrorMessage);
                    }
                }
                return BadRequest(_response);
            }

            if(userDto.UserName.IsNullOrEmpty() && userDto.Email.IsNullOrEmpty())
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("You must specify user name or user's email to login!");
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var result = await  _userService.LoginUserAsync(userDto);

            switch(result.StatusCode)
            {
                case HttpStatusCode.OK: return Ok(result);
                case HttpStatusCode.BadRequest: return BadRequest(result);
                default : return StatusCode(500);
            }
        }
    }
}
