using FactoryManagementSystem.DTOs.ProductionOrders;
using FactoryManagementSystem.Entities;
using FactoryManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FactoryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/production-orders")]
    [Authorize]
    public class ProductionOrdersController : ControllerBase
    {
        private readonly ProductionOrderService _orderService;

        public ProductionOrdersController(ProductionOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetAllAsync();
            var result = orders.Select(o => new ProductionOrderResponseDto
            {
                Id = o.Id,
                ProductId = o.ProductId,
                ProductName = o.Product.Name,
                Quantity = o.Quantity,
                Status = o.Status,
                StartDate = o.StartDate,
                EndDate = o.EndDate,
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(new ProductionOrderResponseDto
            {
                Id = order.Id,
                ProductId = order.ProductId,
                ProductName = order.Product.Name,
                Quantity = order.Quantity,
                Status = order.Status,
                StartDate = order.StartDate,
                EndDate = order.EndDate,
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateProductionOrderDto dto)
        {
            var order = new ProductionOrder
            {
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                Status = "Pending",
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                CreatedAt = DateTime.UtcNow
            };

            await _orderService.CreateAsync(order);
            var created = await _orderService.GetByIdAsync(order.Id);
            if (created == null) return StatusCode(StatusCodes.Status500InternalServerError);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, new ProductionOrderResponseDto
            {
                Id = created.Id,
                ProductId = created.ProductId,
                ProductName = created.Product.Name,
                Quantity = created.Quantity,
                Status = created.Status,
                StartDate = created.StartDate,
                EndDate = created.EndDate,
            });
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateProductionOrderStatusDto dto)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null) return NotFound();

            order.Status = dto.Status;
            await _orderService.UpdateAsync(order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null) return NotFound();

            await _orderService.DeleteAsync(order);
            return NoContent();
        }
    }
}
