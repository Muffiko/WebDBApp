using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepairManagementSystem.Models
{
    [Table("RepairsActivitiesTypes")]
    public class RepairActivityType
    {
        [Key]
        [MaxLength(16)]
        public string RepairActivityTypeId { get; set; } = string.Empty;

        [Required]
        [MaxLength(64)]
        public string Name { get; set; } = string.Empty;

        public ICollection<RepairActivity> RepairActivities { get; set; } = new List<RepairActivity>();
    }
}
