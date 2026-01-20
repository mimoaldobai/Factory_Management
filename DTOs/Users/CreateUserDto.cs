using System.ComponentModel.DataAnnotations;

namespace FactoryManagementSystem.DTOs.Users
{
    public class CreateUserDto
    {
        [Range(1, int.MaxValue)]
        public int EmployeeId { get; set; }

        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string Role { get; set; } = "User";
    }
}
