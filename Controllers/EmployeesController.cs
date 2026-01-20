using FactoryManagementSystem.DTOs.Employees;
using FactoryManagementSystem.Entities;
using FactoryManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FactoryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/employees")]
    [Authorize] // أي مستخدم مسجل الدخول يمكنه الوصول للقراءة، بينما التعديل للـ Admin فقط
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeesController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _employeeService.GetAllAsync();
            var result = employees.Select(e => new EmployeeResponseDto
            {
                Id = e.Id,
                FullName = e.FullName,
                JobTitle = e.JobTitle,
                Department = e.Department,
                Phone = e.Phone,
                IsActive = e.IsActive,
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null) return NotFound();
            return Ok(new EmployeeResponseDto
            {
                Id = employee.Id,
                FullName = employee.FullName,
                JobTitle = employee.JobTitle,
                Department = employee.Department,
                Phone = employee.Phone,
                IsActive = employee.IsActive,
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateEmployeeDto dto)
        {
            var employee = new Employee
            {
                FullName = dto.FullName,
                NationalId = dto.NationalId,
                JobTitle = dto.JobTitle,
                Department = dto.Department,
                Phone = dto.Phone,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
            await _employeeService.CreateAsync(employee);
            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, new EmployeeResponseDto
            {
                Id = employee.Id,
                FullName = employee.FullName,
                JobTitle = employee.JobTitle,
                Department = employee.Department,
                Phone = employee.Phone,
                IsActive = employee.IsActive,
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, UpdateEmployeeDto dto)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null) return NotFound();

            employee.FullName = dto.FullName;
            employee.NationalId = dto.NationalId;
            employee.JobTitle = dto.JobTitle;
            employee.Department = dto.Department;
            employee.Phone = dto.Phone;
            employee.IsActive = dto.IsActive;

            await _employeeService.UpdateAsync(employee);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null) return NotFound();
            await _employeeService.DeleteAsync(employee);
            return NoContent();
        }
    }
}
