using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

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
            var result = await _authService.AuthenticateAsync(loginRequest);
            if (!result.Success || result.Response == null || string.IsNullOrEmpty(result.Response.RefreshToken))
            {
                return Unauthorized(result.ErrorMessage);
            }

            Response.Cookies.Append("refreshToken", result.Response.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false, // Set to true in production (requires HTTPS)
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(30)
            });

            return Ok(new {
                token = result.Response.Token,
                email = result.Response.Email,
                role = result.Response.Role,
                firstName = result.Response.FirstName
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            var result = await _authService.RegisterAsync(registerRequest);
            if (!result.Success || result.Response == null || string.IsNullOrEmpty(result.Response.RefreshToken))
            {
                return BadRequest(result.ErrorMessage);
            }

            Response.Cookies.Append("refreshToken", result.Response.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false, // Set to true in production (requires HTTPS)
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(30)
            });

            return Ok(new {
                token = result.Response.Token,
                email = result.Response.Email,
                role = result.Response.Role,
                firstName = result.Response.FirstName
            });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            var response = await _authService.RefreshTokenAsync(refreshTokenRequest);
            if (response == null)
            {
                return Unauthorized();
            }

            Response.Cookies.Append("refreshToken", response.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false, // Set to true in production (requires HTTPS)
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(30)
            });

            return Ok(new { token = response.Token });
        }
    }
}
