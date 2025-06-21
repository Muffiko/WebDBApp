using System.ComponentModel.DataAnnotations;

namespace RepairManagementSystem.Models.DTOs
{
    public class RepairRequestChangeStatusRequest
    {
        [Required]
        public string NewStatus { get; set; } = string.Empty;
        public string? Result { get; set; } = null;
    }
}