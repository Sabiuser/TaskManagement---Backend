using Microsoft.AspNetCore.Http.HttpResults;
using TaskManagement.DTO;
using TaskManagement.Model;
using TaskManagement.Repository;

namespace TaskManagement.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _user;
        private readonly ITokenService _token;
        private readonly IRefreshTokenRepository _refreshToken;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository user,ITokenService token, IRefreshTokenRepository refreshToken, IConfiguration config)
        {
            _user = user;   
            _token = token;
            _refreshToken = refreshToken;
            _config = config;
        }

        public async Task<AuthResponseDTO> RegsiterUserAsync(RegisterDTO register)
        {
            var exisitingUser = await _user.GetByEmailAsync(register.UserEmail);
            if (exisitingUser!=null)
            {
                return new AuthResponseDTO
                {
                    Success = false,
                    Message = "User already exists"
                };
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(register.Password);

            var user = new User
            {
                UserEmail = register.UserEmail,
                PasswordHash = passwordHash,
                Role = "User"
            };

            await _user.AddAsync(user);
            return new AuthResponseDTO
            {
                Success = true,
                Message = "User registered successfully"
            };
        }

        public async Task<AuthResponseDTO> LoginUserAsync(LoginDTO login)
        {
            var existingUser = await _user.GetByEmailAsync(login.UserEmail);

            if (existingUser == null)
            {
                return new AuthResponseDTO
                {
                    Success = false,
                    Message = "Invalid email or password"
                };
            }

            bool isPassword = BCrypt.Net.BCrypt.Verify(login.Password, existingUser.PasswordHash);
            if (!isPassword)
            {
                return new AuthResponseDTO
                {
                    Success = false,
                    Message = "Incorrect Password"
                };
            }

            var token = _token.GenerateToken(existingUser);
            var refreshToken = _token.GenerateRefreshToken();
            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(
    double.Parse(_config["Jwt:RefreshTokenExpiryDays"]!)
),
                IsRevoked = false,
                UserId = existingUser.Id
            };

            await _refreshToken.AddAsync(refreshTokenEntity);

            return new AuthResponseDTO
            {
                Success = true,
                Message = "Login Successfully",
                Token = token,
                RefreshToken = refreshToken
            };
        }

        public async Task<AuthResponseDTO> RefreshTokenAsync(string refreshToken)
        {
            var storedToken = await _refreshToken.GetRefreshTokenAsync(refreshToken);

            if (storedToken == null)
            {
                return new AuthResponseDTO
                {
                    Success = false,
                    Message = "Invalid refresh token"
                };
            }

            if (storedToken.IsRevoked)
            {
                return new AuthResponseDTO
                {
                    Success = false,
                    Message = "Refresh token has been revoked"
                };
            }

            if (storedToken.ExpiresAt <= DateTime.UtcNow)
            {
                return new AuthResponseDTO
                {
                    Success = false,
                    Message = "Refresh token has expired"
                };
            }

            var user = await _user.GetByIdAsync(storedToken.UserId);

            if (user == null)
            {
                return new AuthResponseDTO
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            var newAccessToken = _token.GenerateToken(user);
            var newRefreshToken = _token.GenerateRefreshToken();
            storedToken.IsRevoked = true;
            await _refreshToken.UpdateTokenAsync(storedToken);

            var newRefreshTokenEntity = new RefreshToken
            {
                Token = newRefreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(
        double.Parse(_config["Jwt:RefreshTokenExpiryDays"]!)
    ),
                IsRevoked = false,
                UserId = user.Id
            };
            await _refreshToken.AddAsync(newRefreshTokenEntity);

            return new AuthResponseDTO
            {
                Success = true,
                Message = "Token refreshed successfully",
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }
        public async Task<AuthResponseDTO> LogoutAsync(string refreshToken)
        {
            var storedToken = await _refreshToken.GetRefreshTokenAsync(refreshToken);

            if (storedToken == null)
            {
                return new AuthResponseDTO
                {
                    Success = false,
                    Message = "Invalid refresh token"
                };
            }

            storedToken.IsRevoked = true;

            await _refreshToken.UpdateTokenAsync(storedToken);
            return new AuthResponseDTO
            {
                Success = true,
                Message = "Logout successful"
            };
        }
    }


}
