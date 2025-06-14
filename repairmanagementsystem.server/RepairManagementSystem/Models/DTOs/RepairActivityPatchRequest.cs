using System.ComponentModel.DataAnnotations;

namespace RepairManagementSystem.Models.DTOs
{
    public class RepairActivityPatchRequest
    {
        public string? RepairActivityTypeId { get; set; }
        public string? SequenceNumber { get; set; }
        public string? Description { get; set; }
        public int? RepairRequestId { get; set; }
        public int? WorkerId { get; set; }
        public string? Result { get; set; }
        public string? Status { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
    }
}
