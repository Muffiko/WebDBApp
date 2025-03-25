using System.ComponentModel.DataAnnotations;
namespace RepairManagementSystem.Models.DTOs
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [MinLength(8)]
        public string? Password { get; set; }
    }
}

