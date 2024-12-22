using System.ComponentModel.DataAnnotations;

namespace webAPI.Dtos.Stock;

public class UpdateStockRequestDto
{
    [Required]
    [MaxLength(10, ErrorMessage = "Symbol must be at most 10 characters long")]
    public string Symbol { get; set; } = string.Empty;
    [Required]
    [MaxLength(10, ErrorMessage = "CompanyName must be at most 10 characters long")]
    public string CompanyName { get; set; } = string.Empty;
    [Required]
    [Range(1, 1000000000)]
    public decimal Purchase { get; set; }
    [Required]
    [Range(0.001, 100)]
    public decimal LastDiv { get; set; }
    [Required]
    [MaxLength(10, ErrorMessage = "Industry must be at most 10 characters long")]
    public string Industry { get; set; } = string.Empty;
    [Range(1, 1000000000)]
    public long MarketCap { get; set; }
}