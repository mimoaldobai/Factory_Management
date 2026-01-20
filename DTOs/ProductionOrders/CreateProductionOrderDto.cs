using System.ComponentModel.DataAnnotations;

namespace FactoryManagementSystem.DTOs.ProductionOrders
{
    public class CreateProductionOrderDto
    {
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
