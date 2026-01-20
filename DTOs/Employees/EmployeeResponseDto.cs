namespace FactoryManagementSystem.DTOs.Employees
{
    public class EmployeeResponseDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string JobTitle { get; set; } = null!;
        public string Department { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
