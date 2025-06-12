using System.ComponentModel.DataAnnotations;

namespace RepairManagementSystem.Models.DTOs
{
    public class RepairActivityRequest
    {
        [Required(ErrorMessage = "Repair Activity Type ID is required.")]
        public string RepairActivityTypeId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Sequence Number is required.")]
        [MaxLength(16, ErrorMessage = "Sequence Number cannot exceed 16 characters.")]
        public string SequenceNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(256, ErrorMessage = "Description cannot exceed 256 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Repair Request ID is required")]
        public int RepairRequestId { get; set; }

        public int WorkerId { get; set; }
    }
}
