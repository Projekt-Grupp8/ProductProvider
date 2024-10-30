﻿using ProductProvider.Infrastructure.Data.Entities;

namespace ProductProvider.Infrastructure.Models;

public class Product
{
    public string Id { get; set; } = null!;
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