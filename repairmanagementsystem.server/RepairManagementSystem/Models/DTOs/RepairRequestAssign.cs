using System.ComponentModel.DataAnnotations;

namespace RepairManagementSystem.Models.DTOs
{
    public class RepairRequestAssign
    {

        [Required(ErrorMessage = "Manager ID is required.")]
        public int ManagerId { get; set; }
    }
}