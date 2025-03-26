using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepairManagementSystem.Models
{
    [Table("RepairsObjectsTypes")]
    public class RepairObjectType
    {
        [Key]
        public int RepairObjectTypeId { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; } = string.Empty;

        public ICollection<RepairRequest> RepairsRequests { get; set; } = new List<RepairRequest>();
    }
}
