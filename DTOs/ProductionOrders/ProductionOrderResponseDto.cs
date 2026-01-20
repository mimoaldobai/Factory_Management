namespace FactoryManagementSystem.DTOs.ProductionOrders
{
    public class ProductionOrderResponseDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int Quantity { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
