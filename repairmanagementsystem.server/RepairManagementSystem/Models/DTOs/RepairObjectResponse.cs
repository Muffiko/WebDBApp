using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepairManagementSystem.Models
{
    public class RepairObjectResponse
    {
        public int RepairObjectId { get; set; }

        public string Name { get; set; } = string.Empty;

        public RepairObjectType RepairObjectType { get; set; } = null!;

        public int CustomerId { get; set; }
    }
}
