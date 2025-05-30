using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RepairManagementSystem.Models
{
    [Owned]
    public class Address
    {
        [JsonIgnore]
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

        //[Required] OK?
        [MaxLength(8)]
        public string? ApartNumber { get; set; } = string.Empty;

        //[Required] OK?
        [MaxLength(8)]
        public string? HouseNumber { get; set; } = string.Empty;
    }
}
