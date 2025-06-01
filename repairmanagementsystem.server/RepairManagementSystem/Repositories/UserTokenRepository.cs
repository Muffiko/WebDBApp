using Microsoft.EntityFrameworkCore;
using RepairManagementSystem.Data;
using RepairManagementSystem.Models;
using RepairManagementSystem.Repositories.Interfaces;

namespace RepairManagementSystem.Repositories
{
    public class UserTokenRepository : IUserTokenRepository
    {
        private readonly ApplicationDbContext _context;

        public UserTokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserToken> GetUserTokenByIdAsync(int userTokenId)
        {
            return await _context.UserTokens.FindAsync(userTokenId);
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
            if (userToken == null)
            {
                return;
            }
            _context.UserTokens.Update(userToken);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserTokenAsync(int userTokenId)
        {
            var userToken = await GetUserTokenByIdAsync(userTokenId);
            if (userToken == null)
            {
                return;
            }
            _context.UserTokens.Remove(userToken);
            await _context.SaveChangesAsync();
        }
    }
}
