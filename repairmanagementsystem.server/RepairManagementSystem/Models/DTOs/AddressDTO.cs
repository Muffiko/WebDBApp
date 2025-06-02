namespace RepairManagementSystem.Models.DTOs
{
    public class AddressDTO
    {
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string? ApartNumber { get; set; } = string.Empty;
        public string? HouseNumber { get; set; } = string.Empty;
    }
}
