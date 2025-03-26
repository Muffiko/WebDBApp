namespace RepairManagementSystem.Models.DTOs
{
    public class CustomerDTO : UserDTO
    {
        public string PaymentMethod { get; set; } = string.Empty;
        public ICollection<RepairRequestDTO> RepairsRequestsDTO { get; set; } = new List<RepairRequestDTO>();
    }
}
