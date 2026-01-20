using FactoryManagementSystem.Data;
using FactoryManagementSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace FactoryManagementSystem.Services
{
    public class ProductionOrderService
    {
        private readonly AppDbContext _context;

        public ProductionOrderService(AppDbContext context)
        {
            _context = context;
        }

       
        public async Task<List<ProductionOrder>> GetAllAsync()
        {
            return await _context.ProductionOrders
                .Include(p => p.Product)
                .ToListAsync();
        }

        
        public async Task<ProductionOrder?> GetByIdAsync(int id)
        {
            return await _context.ProductionOrders
                .Include(p => p.Product)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        
        public async Task<ProductionOrder> CreateAsync(ProductionOrder order)
        {
            _context.ProductionOrders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

       
        public async Task UpdateAsync(ProductionOrder order)
        {
            _context.ProductionOrders.Update(order);
            await _context.SaveChangesAsync();
        }

        
        public async Task UpdateStatusAsync(int orderId, string status)
        {
            var order = await _context.ProductionOrders.FindAsync(orderId);
            if (order != null)
            {
                order.Status = status;
                await _context.SaveChangesAsync();
            }
        }

       
        public async Task DeleteAsync(ProductionOrder order)
        {
            _context.ProductionOrders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}
