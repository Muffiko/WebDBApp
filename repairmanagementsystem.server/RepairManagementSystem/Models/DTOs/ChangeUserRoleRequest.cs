using System.ComponentModel.DataAnnotations;

namespace RepairManagementSystem.Models.DTOs
{
    public class ChangeUserRoleRequest
    {
        [Required]
        public string Role { get; set; } = string.Empty;
    }
}
