using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RepairManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var response = await _authService.AuthenticateAsync(loginRequest);

            if (response == null)
            {
                return Unauthorized();
            }

            return Ok(response);
        }
    }
}
