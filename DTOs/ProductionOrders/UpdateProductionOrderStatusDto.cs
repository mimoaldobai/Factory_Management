using System.ComponentModel.DataAnnotations;

namespace FactoryManagementSystem.DTOs.ProductionOrders
{
    public class UpdateProductionOrderStatusDto
    {
        [Required]
        public string Status { get; set; } = null!;
    }
}
