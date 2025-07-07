using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [MaxLength(16)]
        public string Status { get; set; } = string.Empty;

        [Required]
        [JsonIgnore]
        public int RepairObjectId { get; set; }

        [ForeignKey(nameof(RepairObjectId))]
        public RepairObject RepairObject { get; set; } = null!;

        public int? ManagerId { get; set; } = null;

        [ForeignKey(nameof(ManagerId))] 
        [JsonIgnore]
        public Manager Manager { get; set; } = null!;

        [Required]
        public bool IsPaid { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? StartedAt { get; set; } = null;

        public DateTime? FinishedAt { get; set; } = null;

        public ICollection<RepairActivity> RepairActivities { get; set; } = new List<RepairActivity>();
    }
}
