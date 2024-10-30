using Azure.Core;
using ProductProvider.Infrastructure.Data.Entities;
using ProductProvider.Infrastructure.Models;

namespace ProductProvider.Infrastructure.Factories;

public static class ProductFactory
{
    public static ProductEntity Create(ProductCreateRequest request)
    {
        return new ProductEntity
        {
            Brand = request.Brand,
            Name = request.Name,
            Rating = request.Rating,
            Sizes = request.Sizes,
            Description = request.Description,
            Price = request.Price,
            Category = request.Category,
            CategoryName = request.Category.CategoryName, //Separat kategorinamn för Partition Key i CosmosDB
            StockQuantity = request.StockQuantity,
            ImageURL = request.ImageURL,
            IsNewArrival = request.IsNewArrival,
        };
    }

    public static ProductEntity Update(ProductUpdateRequest request)
    {
        return new ProductEntity
        {
            Id = request.Id,
            Brand = request.Brand,
            Name = request.Name,
            Rating = request.Rating,
            Sizes = request.Sizes,
            Description = request.Description,
            Price = request.Price,
            Category = request.Category,
            CategoryName = request.Category.CategoryName, //Separat kategorinamn för Partition Key i CosmosDB
            StockQuantity = request.StockQuantity,
            ImageURL = request.ImageURL,
            IsNewArrival = request.IsNewArrival,
        };
    }

    public static Product Create(ProductEntity entity)
    {
        return new Product
        {
            Id = entity.Id,
            Brand = entity.Brand,
            Name = entity.Name,
            Rating = entity.Rating,
            Sizes = entity.Sizes,
            Description = entity.Description,
            Price = entity.Price,
            Category = entity.Category,
            CategoryName = entity.Category.CategoryName, //Separat kategorinamn för Partition Key i CosmosDB
            StockQuantity = entity.StockQuantity,
            ImageURL = entity.ImageURL,
            IsNewArrival = entity.IsNewArrival,
        };
    }

}
