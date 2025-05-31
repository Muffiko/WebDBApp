using RepairManagementSystem.Models;

namespace RepairManagementSystem.Repositories.Interfaces
{
    public interface IUserTokenRepository
    {
        Task<UserToken?> GetUserTokenByIdAsync(int userTokenId);
        Task<IEnumerable<UserToken>> GetAllUserTokensAsync();
        Task AddUserTokenAsync(UserToken userToken);
        Task UpdateUserTokenAsync(UserToken userToken);
        Task DeleteUserTokenAsync(int userTokenId);
        Task<bool> RefreshTokenExists(string hashedRefreshToken);
        Task<int> GetUserIdByRefreshToken(string hashedRefreshToken);
        Task<UserToken?> GetUserTokenByUserIdAsync(int userId);
    }
}
