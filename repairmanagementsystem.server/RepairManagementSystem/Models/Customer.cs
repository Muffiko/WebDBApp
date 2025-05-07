using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepairManagementSystem.Models
{
    [Table("Customers")]
    public class Customer
    {
        [Key, ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public User User { get; set; }

        [Required]
        [MaxLength(32)]
        public string PaymentMethod { get; set; } = string.Empty;

        public ICollection<RepairObject> RepairObjects { get; set; } = new List<RepairObject>();
    }
}
