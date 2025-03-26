using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RepairManagementSystem.Models
{
    [Owned]
    public class Address
    {
        [Key]
        public int AddressId { get; set; }

        [Required]
        [MaxLength(3)]
        public string Country { get; set; } = string.Empty;

        [Required]
        [MaxLength(32)]
        public string City { get; set; } = string.Empty;

        [Required]
        [MaxLength(9)]
        public string PostalCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(64)]
        public string Street { get; set; } = string.Empty;

        [Required]
        [MaxLength(16)]
        public string ApartNumber { get; set; } = string.Empty;
    }
}
