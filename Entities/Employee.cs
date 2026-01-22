 using System.ComponentModel.DataAnnotations;

namespace FactoryManagementSystem.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string FullName { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string NationalId { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string JobTitle { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Department { get; set; } = null!;

        [Required]
        [MaxLength(30)]
        public string Phone { get; set; } = null!;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
