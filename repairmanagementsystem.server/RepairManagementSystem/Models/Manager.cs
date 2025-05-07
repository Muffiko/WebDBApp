using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepairManagementSystem.Models
{
    [Table("Managers")]
    public class Manager
    {
        [Key, ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public User User { get; set; }

        [Required]
        [MaxLength(8)]
        public string Expertise { get; set; } = string.Empty;

        [Required]
        public int ActiveRepairsCount { get; set; }

        public ICollection<RepairRequest> RepairRequests { get; set; } = new List<RepairRequest>();
    }
}
