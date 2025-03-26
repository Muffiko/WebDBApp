using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepairManagementSystem.Models
{
    [Table("Workers")]
    public class Worker : User
    {
        [Required]
        [MaxLength(8)]
        public string Expertise { get; set; } = string.Empty;

        [Required]
        public bool IsAvailable { get; set; }

        public ICollection<RepairActivity> RepairsActivities { get; set; } = new List<RepairActivity>();
    }
}
