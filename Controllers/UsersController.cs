using FactoryManagementSystem.DTOs.Users;
using FactoryManagementSystem.Entities;
using FactoryManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FactoryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize(Roles = "Admin")] // فقط Admin يستطيع إدارة المستخدمين
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly PasswordHasher _passwordHasher;

        public UsersController(UserService userService, PasswordHasher passwordHasher)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            var result = users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                Username = u.Username,
                Role = u.Role,
                IsActive = u.IsActive,
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role,
                IsActive = user.IsActive,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto dto)
        {
            var hashedPassword = _passwordHasher.HashPassword(dto.Password);

            var user = new User
            {
                EmployeeId = dto.EmployeeId,
                Username = dto.Username,
                PasswordHash = hashedPassword,
                Role = dto.Role ?? "User",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _userService.CreateAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role,
                IsActive = user.IsActive,
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateUserDto dto)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            user.EmployeeId = dto.EmployeeId;
            user.Username = dto.Username;
            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                user.PasswordHash = _passwordHasher.HashPassword(dto.Password);
            }
            user.Role = dto.Role;
            user.IsActive = dto.IsActive;

            await _userService.UpdateAsync(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            await _userService.DeleteAsync(user);
            return NoContent();
        }
    }
}
