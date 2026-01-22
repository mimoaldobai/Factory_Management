 using System.ComponentModel.DataAnnotations;

namespace FactoryManagementSystem.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue)]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = null!;

        [Required]
        [MaxLength(500)]
        public string PasswordHash { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string Role { get; set; } = "User";

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
