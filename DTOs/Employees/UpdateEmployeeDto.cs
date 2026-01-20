using System.ComponentModel.DataAnnotations;

namespace FactoryManagementSystem.DTOs.Employees
{
    public class UpdateEmployeeDto
    {
        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        public string NationalId { get; set; } = null!;

        [Required]
        public string JobTitle { get; set; } = null!;

        [Required]
        public string Department { get; set; } = null!;

        [Required]
        public string Phone { get; set; } = null!;

        public bool IsActive { get; set; } = true;
    }
}
