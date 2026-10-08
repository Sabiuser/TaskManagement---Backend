using Microsoft.AspNetCore.Mvc;
using TaskManagement.DTO;
using TaskManagement.Service;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;


        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Regsiter(RegisterDTO register)
        {
            var result = await _authService.RegsiterUserAsync(register);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            var result = await _authService.LoginUserAsync(login);

            if (!result.Success)
            {
                return Unauthorized("Invalid Token");
            }

            return Ok(result);
        }

        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenDTO refreshToken)
        {
            var result = await _authService.RefreshTokenAsync(refreshToken.RefreshToken);
            if (!result.Success)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(RefreshTokenDTO refreshToken)
        {
            var result = await _authService.LogoutAsync(refreshToken.RefreshToken);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}