using System.ComponentModel.DataAnnotations;

namespace RepairManagementSystem.Models.DTOs
{
    public class UpdateAddressResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
