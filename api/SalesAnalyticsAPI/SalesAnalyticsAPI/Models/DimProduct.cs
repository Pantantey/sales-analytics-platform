using System.ComponentModel.DataAnnotations;

namespace SalesAnalyticsAPI.Models
{
    public class DimProduct
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string Category { get; set; } = null!;
        public decimal Price { get; set; }
    }
}