using LmsApi.Helpers;
using LmsApi.Models.DTOs.User;
using LmsApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LmsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }
        [HttpGet]
        public ActionResult<ServiceResult<List<GetUserDto>>> GetAllUsers()
        {
            var result = _userService.GetAllUsers();
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }
        [HttpGet("{email}")]
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
        public ActionResult<ServiceResult<GetUserDto>> GetUserById(int id)
        {
            var result = _userService.GetUserById(id);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }
    }
}
