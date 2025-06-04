using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Authorization;
using RepairManagementSystem.Helpers;

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
                return ErrorResponseHelper.CreateProblemDetails(
                    HttpContext,
                    "https://tools.ietf.org/html/rfc9110#section-15.5.2",
                    "Authentication failed",
                    401,
                    new { Login = new[] { result.ErrorMessage ?? "Login failed" } }
                );
            }

            Response.Cookies.Append("refreshToken", result.Response.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false, // Set to true in production (requires HTTPS)
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(30)
            });

            return Ok(new
            {
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
                return ErrorResponseHelper.CreateProblemDetails(
                    HttpContext,
                    "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                    "Registration failed",
                    400,
                    new { Register = new[] { result.ErrorMessage ?? "Registration failed" } }
                );
            }

            Response.Cookies.Append("refreshToken", result.Response.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false, // Set to true in production (requires HTTPS)
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(30)
            });

            return Ok(new
            {
                token = result.Response.Token,
                email = result.Response.Email,
                role = result.Response.Role,
                firstName = result.Response.FirstName
            });
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized();
            }

            var response = await _authService.RefreshTokenAsync(refreshToken);
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
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (!string.IsNullOrEmpty(refreshToken))
            {
                await _authService.DeleteRefreshTokenAsync(refreshToken);
            }
            Response.Cookies.Delete("refreshToken");
            return Ok(new { message = "Logged out successfully" });
        }
    }
}
