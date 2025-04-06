using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepairManagementSystem.Models
{
    [Table("Customers")]
    public class Customer : User
    {
        [Required]
        [MaxLength(32)]
        public string PaymentMethod { get; set; } = string.Empty;

        public ICollection<RepairObject> RepairsObjects { get; set; } = new List<RepairObject>();
    }
}
