using System.ComponentModel.DataAnnotations;

namespace RepairManagementSystem.Models.DTOs
{
    public class RepairActivityRequest
    {
        [Required(ErrorMessage = "Repair Activity Type ID is required.")]
        public string RepairActivityTypeId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Sequence Number is required.")]
        public int SequenceNumber { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(256, ErrorMessage = "Description cannot exceed 256 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Repair Request ID is required")]
        public int RepairRequestId { get; set; }

        public int WorkerId { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
