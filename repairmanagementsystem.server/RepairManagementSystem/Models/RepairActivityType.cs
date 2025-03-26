using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepairManagementSystem.Models
{
    [Table("RepairsActivitiesTypes")]
    public class RepairActivityType
    {
        [Key]
        public int RepairActivityTypeId { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; } = string.Empty;

        public ICollection<RepairActivity> RepairsActivities { get; set; } = new List<RepairActivity>();
    }
}
