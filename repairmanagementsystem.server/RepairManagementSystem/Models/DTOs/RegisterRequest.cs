using System.ComponentModel.DataAnnotations;
namespace RepairManagementSystem.Models.DTOs
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [MaxLength(256, ErrorMessage = "Email cannot exceed 256 characters.")]
        public string? Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "The new password must be at least 8 characters long.")]
        [MaxLength(30, ErrorMessage = "The new password cannot exceed 30 characters.")]
        public string? Password { get; set; }

        [Required]
        [MaxLength(32)]
        public string FirstName { get; set; } = String.Empty;

        [Required]
        [MaxLength(32)]
        public string LastName { get; set; } = String.Empty; 

        [Required]
        [MaxLength(16)]
        public string Number { get; set; } = String.Empty;

        [Required]
        public Address Address { get; set; } = new Address();

    }
}

