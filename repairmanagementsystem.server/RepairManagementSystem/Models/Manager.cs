using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepairManagementSystem.Models
{
    [Table("Managers")]
    public class Manager : User
    {
        [Required]
        [MaxLength(8)]
        public string Expertise { get; set; } = string.Empty;

        [Required]
        public int ActiveRepairsCount { get; set; }

        public ICollection<RepairRequest> RepairsRequests { get; set; } = new List<RepairRequest>();
    }
}
