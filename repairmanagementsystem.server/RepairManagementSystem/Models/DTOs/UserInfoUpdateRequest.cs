using System.ComponentModel.DataAnnotations;

namespace RepairManagementSystem.Models.DTOs
{
    public class UserInfoUpdateRequest
    {
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string? Email { get; set; } 

        [MaxLength(32, ErrorMessage = "First name cannot exceed 32 characters.")]
        public string? FirstName { get; set; }

        [MaxLength(32, ErrorMessage = "Last name cannot exceed 32 characters.")]
        public string? LastName { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string? PhoneNumber { get; set; }
    }
}
