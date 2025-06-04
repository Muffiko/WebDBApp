using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepairManagementSystem.Models
{
    [Table("RepairsRequests")]
    public class RepairRequest
    {
        [Key]
        public int RepairRequestId { get; set; }

        [Required]
        [MaxLength(256)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(32)]
        public string Result { get; set; } = string.Empty;

        [Required]
        [MaxLength(3)]
        public string Status { get; set; } = string.Empty;

        [Required]
        public int RepairObjectId { get; set; }

        [ForeignKey(nameof(RepairObjectId))]
        public RepairObject RepairObject { get; set; } = null!;

        [Required]
        public int ManagerId { get; set; }

        [ForeignKey(nameof(ManagerId))]
        public Manager Manager { get; set; } = null!;

        [Required]
        public bool IsPaid { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime StartedAt { get; set; }

        [Required]
        public DateTime FinishedAt { get; set; }

        public ICollection<RepairActivity> RepairActivities { get; set; } = new List<RepairActivity>();
    }
}
