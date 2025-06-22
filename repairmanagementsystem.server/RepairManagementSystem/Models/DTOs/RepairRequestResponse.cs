using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;

namespace RepairManagementSystem.Models.DTOs
{
    public class RepairRequestResponse
    {
        public int RepairRequestId { get; set; }

        public string Description { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public string RepairObjectName { get; set; }  = string.Empty;

        public int ManagerId { get; set; }

        public string Status { get; set; } = string.Empty;

        public required RepairObjectType RepairObjectType { get; set; }

        public List<RepairActivityResponse> RepairActivities { get; set; } = new();
    }
}
