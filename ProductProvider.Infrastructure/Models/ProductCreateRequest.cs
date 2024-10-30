using ProductProvider.Infrastructure.Data.Entities;

namespace ProductProvider.Infrastructure.Models;

public class ProductCreateRequest
{
    public string Brand { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Rating { get; set; }
    public List<SizeEntity>? Sizes { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; } = null!;
    public int StockQuantity { get; set; }
    public string? ImageURL { get; set; }
    public bool IsNewArrival { get; set; }
}

