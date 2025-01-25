using System.ComponentModel.DataAnnotations;

namespace ProductApi.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; } // Unique 6-digit ID for the product

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; } // Name of the product

        [Required]
        [MaxLength(500)]
        public required string Description { get; set; } // Description of the product

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; } // Price of the product

        [Required]
        [Range(0, int.MaxValue)]
        public int StockAvailable { get; set; } // Available stock for the product
    }
}