using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RepairManagementSystem.Models
{
    [Table("RepairsActivities")]
    public class RepairActivity
    {
        [Key]
        public int RepairActivityId { get; set; }

        [Required]
        public string name { get; set; } = string.Empty;

        [Required]
        [JsonIgnore]
        public string RepairActivityTypeId { get; set; } = string.Empty;

        [ForeignKey(nameof(RepairActivityTypeId))]
        public RepairActivityType RepairActivityType { get; set; } = null!;

        [Required]
        public int SequenceNumber { get; set; }

        [Required]
        [MaxLength(256)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        public string Result { get; set; } = string.Empty;

        [Required]
        [MaxLength(6)]
        public string Status { get; set; } = string.Empty;

        [Required]
        [JsonIgnore]
        public int RepairRequestId { get; set; }

        [ForeignKey(nameof(RepairRequestId))]
        [JsonIgnore]
        public RepairRequest RepairRequest { get; set; } = null!;

        [Required]
        public int WorkerId { get; set; }

        [ForeignKey(nameof(WorkerId))]
        [JsonIgnore]
        public Worker Worker { get; set; } = null!;

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? StartedAt { get; set; } = null;

        public DateTime? FinishedAt { get; set; } = null;
    }
}
