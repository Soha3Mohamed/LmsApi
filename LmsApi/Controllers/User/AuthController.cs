using LmsApi.Models.DTOs.User;
using LmsApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LmsApi.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register(AddUserDto userDto, string password)
        {
            var result = _userService.Register(userDto, password);
            if (!result.Success)
                return BadRequest(result.ErrorMessage);

            // You could use CreatedAtAction if GetUserById is public
            return Ok(result.Data);
        }

        [HttpPost("login")]
        public IActionResult Login(AuthRequestDto loginDto)
        {
            var result = _userService.Authenticate(loginDto);
            if (!result.Success)
                return Unauthorized(); // or BadRequest based on error

            return Ok(result.Data);
        }

        [HttpPost("refresh")]
        public IActionResult Refresh(RefreshRequestDto refreshDto)
        {
            var result = _userService.RefreshToken(refreshDto);
            if (!result.Success)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Data);
        }
    }
}
