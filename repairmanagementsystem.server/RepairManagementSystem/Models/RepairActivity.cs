using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepairManagementSystem.Models
{
    [Table("RepairsActivities")]
    public class RepairActivity
    {
        [Key]
        public int RepairActivityId { get; set; }

        [Required]
        public int RepairActivityTypeId { get; set; }

        [ForeignKey(nameof(RepairActivityTypeId))]
        public RepairActivityType RepairActivityType { get; set; }

        [Required]
        [MaxLength(16)]
        public string SequenceNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        public string Result { get; set; } = string.Empty;

        [Required]
        [MaxLength(3)]
        public string Status { get; set; } = string.Empty;

        [Required]
        public int RepairTaskId { get; set; }

        [ForeignKey(nameof(RepairTaskId))]
        public RepairTask RepairTask { get; set; }

        [Required]
        public int WorkerId { get; set; }
        public Worker Worker { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime StartedAt { get; set; }

        [Required]
        public DateTime FinishedAt { get; set; }
    }
}
