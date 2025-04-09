using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepairManagementSystem.Models
{
    [Table("RepairsObjects")]
    public class RepairObject
    {
        [Key]
        public int RepairObjectId { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string RepairObjectTypeId { get; set; }

        [ForeignKey(nameof(RepairObjectTypeId))]
        public RepairObjectType RepairObjectType { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }

        public ICollection<RepairRequest> RepairRequests { get; set; } = new List<RepairRequest>();
    }
}
