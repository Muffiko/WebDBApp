using System.ComponentModel.DataAnnotations;
namespace RepairManagementSystem.Models.DTOs
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [MinLength(8)]
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

