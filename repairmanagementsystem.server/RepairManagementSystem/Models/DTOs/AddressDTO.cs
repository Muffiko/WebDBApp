using System.ComponentModel.DataAnnotations;

namespace RepairManagementSystem.Models.DTOs
{
    public class AddressDTO
    {
        [Required(ErrorMessage = "Country is required.")]
        [MaxLength(3, ErrorMessage = "Country code must be 3 characters long.")]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required.")]
        [MaxLength(32, ErrorMessage = "City cannot be longer than 32 characters.")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Postal code is required.")]
        [MaxLength(9, ErrorMessage = "Postal code cannot be longer than 9 characters.")]
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Postal code must be in the format XX-XXX.")]
        public string PostalCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Street is required.")]
        [MaxLength(64, ErrorMessage = "Street cannot be longer than 64 characters.")]
        public string Street { get; set; } = string.Empty;

        [MaxLength(8, ErrorMessage = "Apartment number cannot be longer than 10 characters.")]
        public string? ApartNumber { get; set; } = string.Empty;

        [MaxLength(8, ErrorMessage = "House number cannot be longer than 10 characters.")]
        public string? HouseNumber { get; set; } = string.Empty;
    }
}