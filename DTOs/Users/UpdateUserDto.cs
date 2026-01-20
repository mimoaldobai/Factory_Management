using System.ComponentModel.DataAnnotations;

namespace FactoryManagementSystem.DTOs.Users
{
    public class UpdateUserDto
    {
        [Range(1, int.MaxValue)]
        public int EmployeeId { get; set; }

        [Required]
        public string Username { get; set; } = null!;

        public string? Password { get; set; }

        [Required]
        public string Role { get; set; } = "User";

        public bool IsActive { get; set; } = true;
    }
}
