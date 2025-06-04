using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepairManagementSystem.Helpers;
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPost("password-reset")]
        [Authorize]
        public async Task<IActionResult> ResetPassword([FromBody] PasswordResetRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ErrorResponseHelper.CreateProblemDetails(
                    HttpContext,
                    "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                    "Validation failed",
                    400,
                    ModelState
                );
            }

            int? userId = User.GetUserId();
            if (userId == null)
            {
                return ErrorResponseHelper.CreateProblemDetails(
                    HttpContext,
                    "https://tools.ietf.org/html/rfc9110#section-15.5.3",
                    "Unauthorized",
                    401,
                    new { User = new[] { "User is not authenticated." } }
                );
            }

            var response = await _userService.ResetPasswordAsync(userId.Value, request);
            if (!response.Success)
            {
                return ErrorResponseHelper.CreateProblemDetails(
                    HttpContext,
                    "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                    "Password reset failed",
                    400,
                    new { Password = new[] { response.Message } }
                );
            }
            return Ok(response.Message);
        }

        [HttpPatch("update")]
        [Authorize]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UserInfoUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ErrorResponseHelper.CreateProblemDetails(
                    HttpContext,
                    "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                    "Validation failed",
                    400,
                    ModelState
                );
            }

            int? userId = User.GetUserId();
            if (userId == null)
            {
                return ErrorResponseHelper.CreateProblemDetails(
                    HttpContext,
                    "https://tools.ietf.org/html/rfc9110#section-15.5.3",
                    "Unauthorized",
                    401,
                    new { User = new[] { "User is not authenticated." } }
                );
            }

            var response = await _userService.UpdateUserInfoAsync(userId.Value, request);
            if (!response.Success)
            {
                return ErrorResponseHelper.CreateProblemDetails(
                    HttpContext,
                    "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                    "User info update failed",
                    400,
                    new { User = new[] { response.Message } }
                );
            }
            return Ok(response.Message);
        }

        [HttpPatch("address")]
        [Authorize]
        public async Task<IActionResult> UpdateAddress([FromBody] UpdateAddressRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ErrorResponseHelper.CreateProblemDetails(
                    HttpContext,
                    "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                    "Validation failed",
                    400,
                    ModelState
                );
            }

            int? userId = User.GetUserId();
            if (userId == null)
            {
                return ErrorResponseHelper.CreateProblemDetails(
                    HttpContext,
                    "https://tools.ietf.org/html/rfc9110#section-15.5.3",
                    "Unauthorized",
                    401,
                    new { User = new[] { "User is not authenticated." } }
                );
            }

            var response = await _userService.UpdateAddressAsync(userId.Value, request);
            if (!response.Success)
            {
                return ErrorResponseHelper.CreateProblemDetails(
                    HttpContext,
                    "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                    "Address update failed",
                    400,
                    new { Address = new[] { response.Message } }
                );
            }
            return Ok(response.Message);
        }
    }
}
