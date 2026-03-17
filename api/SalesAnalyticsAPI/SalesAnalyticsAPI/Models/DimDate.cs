using System;
using System.ComponentModel.DataAnnotations;

namespace SalesAnalyticsAPI.Models
{
    public class DimDate
    {
        [Key]
        public int DateId { get; set; }
        public DateTime Date { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
    }
}