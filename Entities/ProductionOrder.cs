 using System.ComponentModel.DataAnnotations;

namespace FactoryManagementSystem.Entities
{
    public class ProductionOrder
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = "Pending";

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
