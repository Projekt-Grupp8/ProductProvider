using System.ComponentModel.DataAnnotations;

namespace ProductProvider.Infrastructure.Data.Entities;

public class ProductEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Brand { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Rating { get; set; }
    public List<SizeEntity>? Sizes { get; set; }
    public string? Description { get; set; }    
    public decimal Price { get; set; }
    public CategoryEntity Category { get; set; } = null!;
    public string? CategoryName { get; set; } //Separat kategorinamn för Partition Key i CosmosDB
    public int StockQuantity { get; set; }
    public string? ImageURL { get; set; }
    public bool IsNewArrival { get; set; }

}
