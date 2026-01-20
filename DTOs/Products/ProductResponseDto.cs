namespace FactoryManagementSystem.DTOs.Products
{
    public class ProductResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public decimal UnitCost { get; set; }
        public bool IsActive { get; set; }
    }
}
