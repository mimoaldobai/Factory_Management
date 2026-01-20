using FactoryManagementSystem.DTOs.Products;
using FactoryManagementSystem.Entities;
using FactoryManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FactoryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/products")]
    [Authorize] // أي مستخدم مسجل الدخول يمكنه الوصول، لكن سنفصل الصلاحيات داخل الخدمة إذا لزم
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            var result = products.Select(p => new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Code = p.Code,
                Description = p.Description,
                UnitCost = p.UnitCost,
                IsActive = p.IsActive,
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Code = product.Code,
                Description = product.Description,
                UnitCost = product.UnitCost,
                IsActive = product.IsActive,
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")] // فقط Admin يمكنه إضافة منتجات
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Code = dto.Code,
                Description = dto.Description,
                UnitCost = dto.UnitCost,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _productService.CreateAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Code = product.Code,
                Description = product.Description,
                UnitCost = product.UnitCost,
                IsActive = product.IsActive,
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, CreateProductDto dto)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();

            product.Name = dto.Name;
            product.Code = dto.Code;
            product.Description = dto.Description;
            product.UnitCost = dto.UnitCost;

            await _productService.UpdateAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();

            await _productService.DeleteAsync(product);
            return NoContent();
        }
    }
}
