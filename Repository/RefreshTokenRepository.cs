using Microsoft.EntityFrameworkCore;
using TaskManagement.Context;
using TaskManagement.Model;

namespace TaskManagement.Repository
{
    public class RefreshTokenRepository:IRefreshTokenRepository
    {
        private readonly TaskManagementDbContext _context;
        public RefreshTokenRepository(TaskManagementDbContext context) { 
        _context = context;
        }

        public async Task AddAsync(RefreshToken refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken> GetRefreshTokenAsync(string refreshToken)
        {
            var token = await _context.RefreshTokens.AsNoTracking().FirstOrDefaultAsync(x=>x.Token == refreshToken);
            return token;
        }


        public async Task UpdateTokenAsync(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Update(refreshToken);

            await _context.SaveChangesAsync();
        }



    }
}
