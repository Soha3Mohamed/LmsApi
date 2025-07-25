using LmsApi.Helpers;
using LmsApi.Models.DTOs.User;
using LmsApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using System.Security.Claims;

namespace LmsApi.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("me")]
        [Authorize]
        public ActionResult<ServiceResult<GetUserDto>> GetCurrentUser()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = _userService.GetUserById(userId);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<ServiceResult<List<GetUserDto>>> GetAllUsers()
        {
            var result = _userService.GetAllUsers();
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }
        [HttpGet("user/{email}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<ServiceResult<GetUserDto>> GetUserByEmail(string email)
        {
            var result = _userService.GetUserByEmail(email);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<ServiceResult<GetUserDto>> GetUserById(int id)
        {
            var result = _userService.GetUserById(id);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

        [HttpPost]
        public ActionResult<ServiceResult<AuthResponseDto>> Register(AddUserDto userDto, string password)
        {
            var result = _userService.Register(userDto, password);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }
        [HttpPut("{userId}")]
        [Authorize]
        //i need to seperate controllers and make a me CRUD and other endpoints AND other controller for admin and instructor
        public ActionResult<ServiceResult<GetUserDto>> UpdateUser(int userId, AddUserDto userDto)
        {
            if (User.IsInRole("Admin"))
            {
                var result = _userService.UpdateUser(userId, userDto);
                if (!result.Success)
                {
                    return BadRequest(result.ErrorMessage);
                }
                return Ok(result.Data);
            }
            return Unauthorized();
        }

        [HttpDelete]
        [Authorize]
        public ActionResult<ServiceResult<string>> DeleteUser(int userId)
        {
            var result = _userService.DeleteUser(userId);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }
        [HttpPost("login")] //it is not a get 
        public ActionResult<ServiceResult<AuthResponseDto>> Login(AuthRequestDto authRequestDto)
        {
            var result = _userService.Authenticate(authRequestDto);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }
        [HttpPut("change-mail")]
        [Authorize]
        public ActionResult<ServiceResult<GetUserDto>> ChangeEmail(int userId, string newEmail)
        {
            var result = _userService.ChangeEmail(userId, newEmail);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }
        [HttpPut("change-password")]
        [Authorize]
        public ActionResult<ServiceResult<string>> ChangePassword(int userId, string oldPassword, string newPassword)
        {
            var result = _userService.ChangePassword(userId, oldPassword, newPassword);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

        [HttpPost("refresh")]
        public ActionResult<ServiceResult<AuthResponseDto>> Refresh(RefreshRequestDto refreshDto)
        {
            var result = _userService.RefreshToken(refreshDto);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = _userService.Logout(userId);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);

        }
    }
}
