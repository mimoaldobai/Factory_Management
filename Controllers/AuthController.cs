using FactoryManagementSystem.DTOs.Auth;
using FactoryManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FactoryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly PasswordHasher _passwordHasher;
        private readonly JwtService _jwtService;

        public AuthController(UserService userService, PasswordHasher passwordHasher, JwtService jwtService)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            var user = await _userService.GetByUsernameAsync(dto.Username);
            if (user == null) return Unauthorized("Invalid username or password");

            bool isPasswordValid = _passwordHasher.VerifyPassword(user.PasswordHash, dto.Password);
            if (!isPasswordValid) return Unauthorized("Invalid username or password");

            var token = _jwtService.GenerateToken(user.Id, user.Username, user.Role);

            return Ok(new LoginResponseDto
            {
                Username = user.Username,
                Role = user.Role,
                Token = token
            });
        }
    }
}
