using ProductProvider.Infrastructure.Data.Entities;
using ProductProvider.Infrastructure.Models;
using ProductProvider.Infrastructure.Services;

namespace ProductProvider.Infrastructure.GraphQL.Queries;

public class ProductQuery(IProductService productService)
{
    private readonly IProductService _productService = productService;

    [GraphQLName("getProductById")]
    public async Task<Product> GetProductByIdAsync(string id)
    {
        return await _productService.GetProductByIdAsync(id);
    }

    [GraphQLName("getProductsByCategory")]
    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(CategoryEntity categoryEntity)
    {
        return await _productService.GetProductsByCategoryAsync(categoryEntity);
    }


    [GraphQLName("getAllProducts")]
    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _productService.GetAllProductsAsync();
    }
}
