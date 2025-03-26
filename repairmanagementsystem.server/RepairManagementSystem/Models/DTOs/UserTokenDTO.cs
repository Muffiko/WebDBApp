namespace RepairManagementSystem.Models.DTOs
{
    public class UserTokenDTO
    {
        public int UserTokenId { get; set; } //Required?
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ValidUntil { get; set; } = DateTime.Now;
        public UserDTO UserDTO { get; set; }
    }
}
