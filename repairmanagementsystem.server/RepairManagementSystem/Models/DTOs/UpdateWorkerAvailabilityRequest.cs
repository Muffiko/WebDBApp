using System.ComponentModel.DataAnnotations;

namespace RepairManagementSystem.Models.DTOs
{
    public class UpdateWorkerAvailabilityRequest
    {
        [Required]
        [RegularExpression("^(True|False)$", ErrorMessage = "Availability must be True or False.")]
        public bool IsAvailable { get; set; }
    }
}