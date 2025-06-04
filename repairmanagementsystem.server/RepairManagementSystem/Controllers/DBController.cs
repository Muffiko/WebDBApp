//ONLY FOR DB TESTS!!!
using Microsoft.AspNetCore.Mvc;
using RepairManagementSystem.Helpers;
using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Repositories.Interfaces;

namespace RepairManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DBController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public DBController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("Users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("Users/{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                return ErrorResponseHelper.CreateProblemDetails(
                    HttpContext,
                    "https://tools.ietf.org/html/rfc9110#section-15.5.5",
                    "User not found",
                    404,
                    new { User = new[] { $"User with ID {userId} not found." } }
                );
            }

            return Ok(user);
        }

        [HttpPost("Users")]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            if (user == null)
            {
                return ErrorResponseHelper.CreateProblemDetails(
                    HttpContext,
                    "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                    "Invalid user",
                    400,
                    new { User = new[] { "User cannot be null." } }
                );
            }

            await _userRepository.AddUserAsync(user);
            return CreatedAtAction(nameof(GetUser), new { userId = user.UserId }, user);
        }

        [HttpPut("Users/{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] User updatedUser)
        {
            if (updatedUser == null || updatedUser.UserId != userId)
            {
                return ErrorResponseHelper.CreateProblemDetails(
                    HttpContext,
                    "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                    "User ID mismatch",
                    400,
                    new { User = new[] { "User ID mismatch." } }
                );
            }

            var existingUser = await _userRepository.GetUserByIdAsync(userId);

            if (existingUser == null)
            {
                return ErrorResponseHelper.CreateProblemDetails(
                    HttpContext,
                    "https://tools.ietf.org/html/rfc9110#section-15.5.5",
                    "User not found",
                    404,
                    new { User = new[] { $"User with ID {userId} not found." } }
                );
            }

            existingUser.FirstName = updatedUser.FirstName;
            existingUser.LastName = updatedUser.LastName;
            existingUser.Email = updatedUser.Email;
            existingUser.PasswordHash = updatedUser.PasswordHash;
            existingUser.Number = updatedUser.Number;
            existingUser.Address = updatedUser.Address; 
            existingUser.Role = updatedUser.Role;
            existingUser.CreatedAt = updatedUser.CreatedAt;
            existingUser.LastLoginAt = updatedUser.LastLoginAt;
            existingUser.IsActive = updatedUser.IsActive;

            await _userRepository.UpdateUserAsync(existingUser);

            return NoContent();
        }

        [HttpDelete("Users/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(userId);

            if (existingUser == null)
            {
                return ErrorResponseHelper.CreateProblemDetails(
                    HttpContext,
                    "https://tools.ietf.org/html/rfc9110#section-15.5.5",
                    "User not found",
                    404,
                    new { User = new[] { $"User with ID {userId} not found." } }
                );
            }

            await _userRepository.DeleteUserAsync(userId);
            return NoContent();
        }
    }
}
//ONLY FOR DB TESTS!!!
