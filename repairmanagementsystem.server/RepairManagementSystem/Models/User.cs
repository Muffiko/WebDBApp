using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepairManagementSystem.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(32)]
        public string FirstName { get; set; } = String.Empty;

        [Required]
        [MaxLength(32)]
        public string LastName { get; set; } = String.Empty; 

        [Required]
        [MaxLength(64)]
        public string Email { get; set; } = String.Empty;

        [Required]
        public string PasswordHash { get; set; } = String.Empty;

        [Required]
        [MaxLength(16)]
        public string Number { get; set; } = String.Empty;

        [Required]
        public Address Address { get; set; } = new Address();

        [Required]
        [MaxLength(32)]
        public string Role { get; set; } = String.Empty;

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime LastLoginAt { get; set; }

        public UserToken UserToken { get; set; }
    }
}
