using FactoryManagementSystem.Data;
using FactoryManagementSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace FactoryManagementSystem.Services
{
    public class EmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

       
        public async Task<List<Employee>> GetAllAsync() =>
            await _context.Employees.ToListAsync();

        
        public async Task<Employee?> GetByIdAsync(int id) =>
            await _context.Employees.FindAsync(id);

       
        public async Task<Employee> CreateAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        
        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        
        public async Task DeleteAsync(Employee employee)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }
}
