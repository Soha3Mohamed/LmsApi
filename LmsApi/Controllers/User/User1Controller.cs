using LmsApi.Models.DTOs.User;
using LmsApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LmsApi.Controllers.User
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class User1Controller : ControllerBase
    {
        private readonly IUserService _userService;

        public User1Controller(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = _userService.GetUserById(userId);
            if (!result.Success)
                return NotFound(result.ErrorMessage);

            return Ok(result.Data);
        }

        [HttpPut("change-email")]
        public IActionResult ChangeEmail(string newEmail)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = _userService.ChangeEmail(userId, newEmail);
            if (!result.Success)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Data);
        }

        [HttpPut("change-password")]
        public IActionResult ChangePassword(string oldPassword, string newPassword)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = _userService.ChangePassword(userId, oldPassword, newPassword);
            if (!result.Success)
                return BadRequest(result.ErrorMessage);

            return Ok("Password updated.");
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = _userService.Logout(userId);
            if (!result.Success)
                return BadRequest(result.ErrorMessage);

            return NoContent();
        }
    }
}
