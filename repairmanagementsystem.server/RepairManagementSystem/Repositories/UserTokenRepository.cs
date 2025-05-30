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
            var userToken = await _context.UserTokens.FindAsync(userTokenId);

            if (userToken == null)
            {
                throw new KeyNotFoundException($"User token with ID {userTokenId} not found.");
            }
            else
            {
                return userToken;
            }
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
                throw new KeyNotFoundException($"User token with ID {userTokenId} not found.");
            }
        }
    }
}
