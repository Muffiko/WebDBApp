using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepairManagementSystem.Helpers;
using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Services.Interfaces;
using AutoMapper;
using RepairManagementSystem.Extensions;

namespace RepairManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
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
                return this.ToApiResponse(Result.Fail(400, "Validation failed."));
            }
            int? userId = User.GetUserId();
            if (userId == null)
            {
                return this.ToApiResponse(Result.Fail(401, "User is not authenticated."));
            }
            var response = await _userService.ResetPasswordAsync(userId.Value, request);
            return this.ToApiResponse(response);
        }

        [HttpPatch("update")]
        [Authorize]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UserInfoUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return this.ToApiResponse(Result.Fail(400, "Validation failed."));
            }
            int? userId = User.GetUserId();
            if (userId == null)
            {
                return this.ToApiResponse(Result.Fail(401, "User is not authenticated."));
            }
            var response = await _userService.UpdateUserInfoAsync(userId.Value, request);
            return this.ToApiResponse(response);
        }

        [HttpPatch("address")]
        [Authorize]
        public async Task<IActionResult> UpdateAddress([FromBody] UpdateAddressRequest request)
        {
            if (!ModelState.IsValid)
            {
                return this.ToApiResponse(Result.Fail(400, "Validation failed."));
            }
            int? userId = User.GetUserId();
            if (userId == null)
            {
                return this.ToApiResponse(Result.Fail(401, "User is not authenticated."));
            }
            var response = await _userService.UpdateAddressAsync(userId.Value, request);
            return this.ToApiResponse(response);
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMyInfo()
        {
            int? userId = User.GetUserId();
            if (userId == null)
            {
                return this.ToApiResponse(Result.Fail(401, "User is not authenticated."));
            }
            var user = await _userService.GetUserEntityByIdAsync(userId.Value);
            if (user == null)
            {
                return this.ToApiResponse(Result.Fail(404, "User not found."));
            }
            var response = _mapper.Map<UserMyInfoResponse>(user);
            return Ok(response);
        }
    }
}
