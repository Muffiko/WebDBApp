using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepairManagementSystem.Models
{
    [Table("RepairsObjectsTypes")]
    public class RepairObjectType
    {
        [Key]
        [MaxLength(16)]
        public string RepairObjectTypeId { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; } = string.Empty;

        public ICollection<RepairObject> RepairObjects { get; set; } = new List<RepairObject>();
    }
}
