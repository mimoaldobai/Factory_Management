using System.ComponentModel.DataAnnotations;

namespace FactoryManagementSystem.DTOs.Products
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Code { get; set; } = null!;

        public string? Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal UnitCost { get; set; }
    }
}
