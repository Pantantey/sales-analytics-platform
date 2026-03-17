using System.ComponentModel.DataAnnotations;

namespace SalesAnalyticsAPI.Models
{
    public class FactSales
    {
        [Key]
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int DateId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
    }
}