using System.ComponentModel.DataAnnotations;

namespace RepairManagementSystem.Models.DTOs
{
    public class AddressUpdateRequest
    {

        [MaxLength(3)]
        public string? Country { get; set; }

        [MaxLength(32)]
        public string? City { get; set; }
        [MaxLength(9)]
        public string? PostalCode { get; set; } 

        [MaxLength(64)]
        public string? Street { get; set; }

        [MaxLength(8)]
        public string? ApartNumber { get; set; }

        [MaxLength(8)]
        public string? HouseNumber { get; set; }
    }
}
