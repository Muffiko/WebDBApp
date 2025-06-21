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
        [Authorize(Roles = "Admin, Manager")]
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

        [HttpPatch("{userId:int}/role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeUserRole(int userId, [FromBody] ChangeUserRoleRequest request)
        {
            var result = await _userService.ChangeUserRoleAsync(userId, request);
            return this.ToApiResponse(result);
        }

        [HttpPatch("workers/{workerId:int}/availability")]
        public async Task<IActionResult> UpdateWorkerAvailability(int workerId, [FromBody] UpdateWorkerAvailabilityRequest request)
        {
            var result = await _userService.UpdateWorkerAvailabilityAsync(workerId, request);
            return this.ToApiResponse(result);
        }

        [HttpGet("workers")]
        public async Task<IActionResult> GetWorkers()
        {
            var result = await _userService.GetAllWorkersAsync();
            return Ok(result);
        }

        [HttpGet("managers")]
        public async Task<IActionResult> GetManagers()
        {
            var result = await _userService.GetAllManagersAsync();
            return Ok(result);
        }

        [HttpGet("customers")]
        public async Task<IActionResult> GetCustomers()
        {
            var result = await _userService.GetAllCustomersAsync();
            return Ok(result);
        }
    }
}
