using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs.Stock
{
    public class CreateStockRequestDTO
    {
        [Required]
        [Length(10, 20, ErrorMessage = "Symbol must be between 10 and 20 characters long.")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [Length(10, 30, ErrorMessage = "Company name must be between 10 and 30 characters long.")]
        public string CompanyName { get; set; } = string.Empty;
        [Range(10, 10000000000)]
        [Required]
        public decimal Purchase { get; set; }
        [Range(0.001, 100)]
        [Required]
        public decimal LastDiv { get; set; }
        [Required]
        [Length(10, 20, ErrorMessage = "Industry name must be between 10 and 20 characters long.")]
        public string Industry { get; set; } = string.Empty;
        [Required]
        [Range(1, 999999999999999)]
        public long MarketCap { get; set; }
    }
}
