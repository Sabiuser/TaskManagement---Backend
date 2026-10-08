using TaskManagement.Model;

namespace TaskManagement.Repository
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken refreshToken);
        Task<RefreshToken> GetRefreshTokenAsync(string  refreshToken);
        Task UpdateTokenAsync(RefreshToken refreshToken);
    }
}
