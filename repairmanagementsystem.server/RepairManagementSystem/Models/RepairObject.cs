using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


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
        [JsonIgnore]
        public string RepairObjectTypeId { get; set; } = string.Empty;

        [ForeignKey(nameof(RepairObjectTypeId))]
        public RepairObjectType RepairObjectType { get; set; } = null!;

        [Required]
        public int CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [JsonIgnore]
        public Customer Customer { get; set; } = null!;

    }
}
