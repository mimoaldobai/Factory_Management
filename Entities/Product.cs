 using System.ComponentModel.DataAnnotations;

namespace FactoryManagementSystem.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Code { get; set; } = null!;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Range(typeof(decimal), "0", "999999999")]
        public decimal UnitCost { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
