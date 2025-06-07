using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Authorization;
using RepairManagementSystem.Helpers;
using RepairManagementSystem.Extensions;

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

        private void SetRefreshTokenCookie(string refreshToken)
        {
            Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false, // Set to true in production (requires HTTPS)
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(30)
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var result = await _authService.AuthenticateAsync(loginRequest);
            if (result.IsSuccess && result.Data != null && !string.IsNullOrEmpty(result.Data.RefreshToken))
            {
                SetRefreshTokenCookie(result.Data.RefreshToken);
                result.Data.RefreshToken = null;
            }
            return this.ToApiResponse(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            var result = await _authService.RegisterAsync(registerRequest);
            if (result.IsSuccess && result.Data != null && !string.IsNullOrEmpty(result.Data.RefreshToken))
            {
                SetRefreshTokenCookie(result.Data.RefreshToken);
                result.Data.RefreshToken = null;
            }
            return this.ToApiResponse(result);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
            {
                var failResult = Result<AuthResponse>.Fail(401, "Refresh token is missing.");
                return this.ToApiResponse(failResult);
            }

            var result = await _authService.RefreshTokenAsync(refreshToken);
            if (result.IsSuccess && result.Data != null && !string.IsNullOrEmpty(result.Data.RefreshToken))
            {
                SetRefreshTokenCookie(result.Data.RefreshToken);
                result.Data.RefreshToken = null;
            }
            return this.ToApiResponse(result);
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
