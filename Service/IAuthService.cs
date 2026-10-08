using TaskManagement.DTO;

namespace TaskManagement.Service
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> RegsiterUserAsync(RegisterDTO register);
        Task<AuthResponseDTO> LoginUserAsync(LoginDTO login);
        Task<AuthResponseDTO> RefreshTokenAsync(string refreshToken);
        Task<AuthResponseDTO> LogoutAsync(string refreshToken);
    }
}
