using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RepairManagementSystem.Models
{
    [Table("RepairsObjectsTypes")]
    public class RepairObjectType
    {
        [Key]
        [MaxLength(16)]
        public required string RepairObjectTypeId { get; set; } 

        [Required]
        [MaxLength(64)]
        public required string Name { get; set; }

        [JsonIgnore]
        public ICollection<RepairObject> RepairObjects { get; set; } = new List<RepairObject>();
    }
}
