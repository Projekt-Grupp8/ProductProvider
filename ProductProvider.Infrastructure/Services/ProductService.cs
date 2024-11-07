using Microsoft.EntityFrameworkCore;
using ProductProvider.Infrastructure.Data.Contexts;
using ProductProvider.Infrastructure.Data.Entities;
using ProductProvider.Infrastructure.Factories;
using ProductProvider.Infrastructure.Models;

namespace ProductProvider.Infrastructure.Services;

public interface IProductService
{
    Task<Product> CreateProductAsync(ProductCreateRequest request);
    Task<Product> GetProductByIdAsync(string id);
    Task<IEnumerable<Product>> GetProductsByCategoryAsync (string category);
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product> UpdateProductAsync(ProductUpdateRequest request);
    Task<bool> DeleteProductAsync(string id);

}
public class ProductService(IDbContextFactory<DataContext> contextFactory) : IProductService
{
    private readonly IDbContextFactory<DataContext> _contextFactory = contextFactory;





    public async Task<Product> CreateProductAsync(ProductCreateRequest request)
    {
        await using var context = _contextFactory.CreateDbContext();
        
        var productEntity = ProductFactory.Create(request);
        context.Products.Add(productEntity);
        await context.SaveChangesAsync();

        return ProductFactory.Create(productEntity);
    }

    public async Task<bool> DeleteProductAsync(string id)
    {
        await using var context = _contextFactory.CreateDbContext();
        var productEntity = await context.Products.FirstOrDefaultAsync(c => c.Id == id);
        if (productEntity == null) return false;

        context.Products.Remove(productEntity);
        await context.SaveChangesAsync();
        return true;

       
    }

    public async Task<Product> GetProductByIdAsync(string id)
    {
        await using var context = _contextFactory.CreateDbContext();
        var productEntity = await context.Products.FirstOrDefaultAsync(c => c.Id == id);

        return productEntity == null ? null! : ProductFactory.Create(productEntity);
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category)
    {
        await using var context = _contextFactory.CreateDbContext();
        var productEntities = await context.Products
            .Where(c => c.Category == category)
            .ToListAsync();
            ;

        return productEntities.Select(ProductFactory.Create);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        await using var context = _contextFactory.CreateDbContext();
        var productEntities = await context.Products.ToListAsync();

        return productEntities.Select(ProductFactory.Create);
    }

    public async Task<Product> UpdateProductAsync(ProductUpdateRequest request)
    {
        await using var context = _contextFactory.CreateDbContext();
        var existingProduct = await context.Products.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (existingProduct == null) 
            return null;

        var updatedProductEntity = ProductFactory.Update(request);
        updatedProductEntity.Id = existingProduct.Id;
        context.Entry(existingProduct).CurrentValues.SetValues(updatedProductEntity);

        await context.SaveChangesAsync();
        return ProductFactory.Create(existingProduct);

    }
}
