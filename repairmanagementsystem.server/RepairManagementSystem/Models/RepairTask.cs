using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepairManagementSystem.Models
{
    [Table("RepairsTasks")]
    public class RepairTask
    {
        [Key]
        public int RepairTaskId { get; set; }

        [Required]
        [MaxLength(256)]
        public string Description { get; set; } = String.Empty;

        [Required]
        [MaxLength(32)]
        public string Result { get; set; } = String.Empty;

        [Required]
        [MaxLength(3)]
        public string Status { get; set; } = String.Empty;

        [Required]
        public int RepairRequestId { get; set; }

        [ForeignKey(nameof(RepairRequestId))]
        public RepairRequest RepairRequest { get; set; }

        [Required]
        public int ManagerId { get; set; }

        [ForeignKey(nameof(ManagerId))]
        public Manager Manager { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime StartedAt { get; set; }

        [Required]
        public DateTime FinishedAt { get; set; }

        public ICollection<RepairActivity> RepairsActivities { get; set; } = new List<RepairActivity>();
    }
}
