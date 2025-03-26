using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepairManagementSystem.Models
{
    [Table("UsersTokens")]
    public class UserToken
    {
        [Key]
        public int UserTokenId { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public string RefreshToken { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public DateTime ValidUntil { get; set; } = DateTime.Now;

        public User User { get; set; }
    }
}
