using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesAnalyticsAPI.Data;
using SalesAnalyticsAPI.Models;

namespace SalesAnalyticsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly SalesDbContext _context;

        public SalesController(SalesDbContext context)
        {
            _context = context;
        }

        // GET: api/sales
        [HttpGet]
        public async Task<IActionResult> GetSales()
        {
            // Traer ventas uniendo dimensiones para un resultado legible
            var sales = await _context.FactSales
                .Join(_context.Products, f => f.ProductId, p => p.ProductId, (f, p) => new { f, p })
                .Join(_context.Customers, fp => fp.f.CustomerId, c => c.CustomerId, (fp, c) => new { fp.f, fp.p, c })
                .Join(_context.Dates, fpc => fpc.f.DateId, d => d.DateId, (fpc, d) => new
                {
                    fpc.f.SaleId,
                    ProductName = fpc.p.ProductName,
                    Category = fpc.p.Category,
                    Price = fpc.p.Price,
                    CustomerName = fpc.c.CustomerName,
                    City = fpc.c.City,
                    Country = fpc.c.Country,
                    SaleDate = d.Date,
                    fpc.f.Quantity,
                    fpc.f.TotalAmount
                })
                .ToListAsync();

            return Ok(sales);
        }

        [HttpGet("revenue-by-product")]
        public async Task<IActionResult> GetRevenueByProduct()
        {
            var result = await _context.FactSales
                .Join(_context.Products,
                    f => f.ProductId,
                    p => p.ProductId,
                    (f, p) => new { f, p })
                .GroupBy(x => x.p.ProductName)
                .Select(g => new
                {
                    ProductName = g.Key,
                    TotalRevenue = g.Sum(x => x.f.TotalAmount)
                })
                .OrderByDescending(x => x.TotalRevenue)
                .ToListAsync();

            return Ok(result);
        }

        [HttpGet("revenue-by-month")]
        public async Task<IActionResult> GetRevenueByMonth()
        {
            var result = await _context.FactSales
                .Join(_context.Dates,
                    f => f.DateId,
                    d => d.DateId,
                    (f, d) => new { f, d })
                .GroupBy(x => new { x.d.Year, x.d.Month })
                .Select(g => new
                {
                    g.Key.Year,
                    g.Key.Month,
                    TotalRevenue = g.Sum(x => x.f.TotalAmount)
                })
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .ToListAsync();

            return Ok(result);
        }

        [HttpGet("top-customers")]
        public async Task<IActionResult> GetTopCustomers()
        {
            var result = await _context.FactSales
                .Join(_context.Customers,
                    f => f.CustomerId,
                    c => c.CustomerId,
                    (f, c) => new { f, c })
                .GroupBy(x => x.c.CustomerName)
                .Select(g => new
                {
                    CustomerName = g.Key,
                    TotalSpent = g.Sum(x => x.f.TotalAmount)
                })
                .OrderByDescending(x => x.TotalSpent)
                .Take(5)
                .ToListAsync();

            return Ok(result);
        }
    }
}