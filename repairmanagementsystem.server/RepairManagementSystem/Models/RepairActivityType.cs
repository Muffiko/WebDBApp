using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RepairManagementSystem.Models
{
    [Table("RepairsActivitiesTypes")]
    public class RepairActivityType
    {
        [Key]
        [MaxLength(16)]
        public required string RepairActivityTypeId { get; set; }

        [Required]
        [MaxLength(64)]
        public required string Name { get; set; }

        [JsonIgnore]
        public ICollection<RepairActivity> RepairActivities { get; set; } = new List<RepairActivity>();
    }
}
