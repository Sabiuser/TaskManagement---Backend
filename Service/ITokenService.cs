using TaskManagement.Model;

namespace TaskManagement.Service
{
    public interface ITokenService
    {
        string GenerateToken(User user);
        string GenerateRefreshToken();
    }
}
