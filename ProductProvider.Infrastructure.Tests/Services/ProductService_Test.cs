using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Moq;
using ProductProvider.Infrastructure.Data.Contexts;
using ProductProvider.Infrastructure.Data.Entities;
using ProductProvider.Infrastructure.Factories;
using ProductProvider.Infrastructure.Models;
using ProductProvider.Infrastructure.Services;

namespace ProductProvider.Infrastructure.Tests.Services;



public class DbContextFactory : IDbContextFactory<DataContext>
{
    public DataContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseInMemoryDatabase("TestDatabase_" + Guid.NewGuid());

        return new DataContext(optionsBuilder.Options);
    }
}



public class ProductService_Test
{
    private readonly IDbContextFactory<DataContext> _contextFactory;    

    public ProductService_Test()
    {
        
        _contextFactory = new DbContextFactory();
    }


    [Fact]
    public async Task CreateProductAsync_ShouldCreateOneProduct()
    {        
        
        using var context = _contextFactory.CreateDbContext();

        //Arrange
        var productService = new ProductService(_contextFactory);
        var productCreateRequest = new ProductCreateRequest
        {
            Brand = "Test Brand",
            Name = "Test Name",
            Rating = 5,
            Sizes = null,
            Description = "Test Description",
            Price = 100,
            Category = "Test Category",
            StockQuantity = 1,
            ImageURL = "Test URL",
            IsNewArrival = false,
        };

        //Act
        var result = await productService.CreateProductAsync(productCreateRequest);
     

        //Assert
        Assert.NotNull(result);
        Assert.Equal("Test Name", result.Name);

    }
}
