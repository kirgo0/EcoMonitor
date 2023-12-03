﻿using EcoMonitor.Model;
using EcoMonitor.Model.DTO.UserService;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace EcoMonitor.Services
{
    public interface IUserService
    {
        Task<APIResponse> RergisterUserAsync(RegisterUserDTO userDto);
        Task<APIResponse> LoginUserAsync(LoginUserDTO userDto);

    }

    public class UserService : IUserService
    {
        private APIResponse _response;
        private readonly string DEFAULT_USER_ROLE = "User";
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        private IConfiguration _configuration;

        public UserService(UserManager<IdentityUser> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _response = new APIResponse();
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        public async Task<APIResponse> RergisterUserAsync(RegisterUserDTO userDto)
        {
            if(userDto == null) throw new NullReferenceException("Accepted register model is null");

            if(userDto.Password != userDto.ConfirmPassword)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Confirm password doesn't match the password");
                return _response;
            }

            var identityUser = new IdentityUser
            {
                Email = userDto.Email,
                UserName = userDto.UserName
            };

            var result = await _userManager.CreateAsync(identityUser, userDto.Password);

            if(result.Succeeded)
            {
                //if(await _roleManager.FindByNameAsync(DEFAULT_USER_ROLE) == null)
                //{
                //    var roleResult = await _roleManager.CreateAsync(new IdentityRole(DEFAULT_USER_ROLE));
                //    if(!roleResult.Succeeded)
                //    {
                //        _response.IsSuccess = false;
                //        _response.StatusCode = HttpStatusCode.InternalServerError;
                //        return _response;
                //    } else
                //    {
                        var user = await _userManager.FindByNameAsync(userDto.UserName);
                        if (user != null)
                        {
                            //var userToRole = await _userManager.AddToRoleAsync(user, DEFAULT_USER_ROLE);
                            //if(userToRole.Succeeded)
                            //{
                                _response.IsSuccess = true;
                                _response.StatusCode = HttpStatusCode.Created;
                            } else
                            {
                                _response.IsSuccess = false;
                                _response.StatusCode = HttpStatusCode.InternalServerError;
                            }
                            return _response;
                        //}
                //    }
                //}
            } else
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.Conflict;
                _response.ErrorMessages.AddRange(result.Errors.Select(e => e.Description));
            }
            return _response;
        }

        public async Task<APIResponse> LoginUserAsync([FromBody] LoginUserDTO userDto)
        {
            var user = await _userManager.FindByNameAsync(userDto.UserName);

            if(user == null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Password or login are wrong");
                return _response;
            }

            var result = await _userManager.CheckPasswordAsync(user, userDto.Password);

            if(!result)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Password or login are wrong");
                return _response;
            }

            var claims = new[]
            {
                new Claim("Email", user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            _response.Result = tokenAsString;
            _response.StatusCode = HttpStatusCode.OK;
            return _response;
        }
    }
}