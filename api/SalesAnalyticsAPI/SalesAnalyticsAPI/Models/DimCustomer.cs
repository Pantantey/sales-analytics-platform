using System.ComponentModel.DataAnnotations;

namespace SalesAnalyticsAPI.Models
{
    public class DimCustomer
    {
        [Key]
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
}