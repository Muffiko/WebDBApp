using System.ComponentModel.DataAnnotations;
namespace RepairManagementSystem.Models.DTOs
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string? Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "The new password must be at least 8 characters long.")]
        [MaxLength(30, ErrorMessage = "The new password cannot exceed 30 characters.")]
        public string? Password { get; set; }

        [Required]
        [MaxLength(32, ErrorMessage = "First name cannot exceed 32 characters.")]
        public string FirstName { get; set; } = String.Empty;

        [Required]
        [MaxLength(32, ErrorMessage = "Last name cannot exceed 32 characters.")]
        public string LastName { get; set; } = String.Empty; 

        [Required]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; } = String.Empty;

        [Required]
        public Address Address { get; set; } = new Address();

    }
}

