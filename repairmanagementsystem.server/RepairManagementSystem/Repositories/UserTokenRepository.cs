using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RepairManagementSystem.Data;
using RepairManagementSystem.Models;
using RepairManagementSystem.Repositories.Interfaces;

namespace RepairManagementSystem.Repositories
{
    public class UserTokenRepository : IUserTokenRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserTokenRepository> _logger;

        public UserTokenRepository(ApplicationDbContext context, ILogger<UserTokenRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<UserToken?> GetUserTokenByIdAsync(int userTokenId)
        {
            var userToken = await _context.UserTokens.FindAsync(userTokenId);

            return userToken;
        }

        public async Task<UserToken?> GetUserTokenByUserIdAsync(int userId)
        {
            var userToken = await _context.UserTokens
                .FirstOrDefaultAsync(ut => ut.UserId == userId);

            if (userToken == null)
            {
                return null;
            }
            else
            {
                return userToken;
            }
        }

        public async Task<int> GetUserIdByRefreshToken(string hashedRefreshToken)
        {
            var userToken = await _context.UserTokens
                .FirstOrDefaultAsync(ut => ut.RefreshToken == hashedRefreshToken);

            return userToken?.UserId ?? -1;
        }

        public async Task<bool> RefreshTokenExists(string hashedRefreshToken)
        {
            return await _context.UserTokens.AnyAsync(ut => ut.RefreshToken == hashedRefreshToken);
        }

        public async Task<IEnumerable<UserToken>> GetAllUserTokensAsync()
        {
            return await _context.UserTokens.ToListAsync();
        }

        public async Task AddUserTokenAsync(UserToken userToken)
        {
            _context.UserTokens.Add(userToken);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserTokenAsync(UserToken userToken)
        {
            _context.UserTokens.Update(userToken);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserTokenAsync(int userTokenId)
        {
            var userToken = await GetUserTokenByIdAsync(userTokenId);

            if (userToken != null)
            {
                _context.UserTokens.Remove(userToken);
                await _context.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning($"Tried to delete a non-existing user token with ID {userTokenId}.");
            }
        }
        public async Task<UserToken?> GetUserTokenByRefreshToken(string hashedRefreshToken)
        {
            return await _context.UserTokens
                .FirstOrDefaultAsync(ut => ut.RefreshToken == hashedRefreshToken);
        }
    }
}
