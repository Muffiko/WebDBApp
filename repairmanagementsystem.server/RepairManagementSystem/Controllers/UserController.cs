using Microsoft.AspNetCore.Mvc;
using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Services.Interfaces;

namespace RepairManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCreateDTO userCreateDto)
        {
            if (userCreateDto == null)
                return BadRequest("User data is required");
            await _userService.RegisterUserAsync(userCreateDto);
            return Ok(userCreateDto);
        }
    }
}
