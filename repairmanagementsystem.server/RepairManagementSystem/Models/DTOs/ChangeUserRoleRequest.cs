using System.ComponentModel.DataAnnotations;

namespace RepairManagementSystem.Models.DTOs
{
    public class ChangeUserRoleRequest
    {
        [Required]
        public string role = string.Empty;
    }
}
