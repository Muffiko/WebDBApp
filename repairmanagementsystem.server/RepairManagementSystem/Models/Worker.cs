using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepairManagementSystem.Models
{
    [Table("Workers")]
    public class Worker
    {
        [Key, ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public User User { get; set; } = null!;

        [Required]
        [MaxLength(8)]
        public string Expertise { get; set; } = string.Empty;

        [Required]
        public bool IsAvailable { get; set; }

        public ICollection<RepairActivity> RepairActivities { get; set; } = new List<RepairActivity>();
    }
}
