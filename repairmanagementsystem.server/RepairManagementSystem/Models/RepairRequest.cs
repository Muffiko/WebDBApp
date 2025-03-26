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
        [MaxLength(64)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int RepairObjectTypeId { get; set; }

        [ForeignKey(nameof(RepairObjectTypeId))]
        public RepairObjectType RepairObjectType { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }

        [Required]
        public bool IsPaid { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime StartedAt { get; set; }

        [Required]
        public DateTime FinishedAt { get; set; }

        public ICollection<RepairTask> RepairsTasks { get; set; } = new List<RepairTask>();
    }
}
