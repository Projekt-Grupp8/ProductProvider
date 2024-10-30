using Microsoft.EntityFrameworkCore;
using ProductProvider.Infrastructure.Data.Entities;

namespace ProductProvider.Infrastructure.Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<ProductEntity> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductEntity>().ToContainer("Products");
        modelBuilder.Entity<ProductEntity>().HasPartitionKey(c => c.CategoryName);
        modelBuilder.Entity<ProductEntity>().OwnsOne(c => c.Category);

        //Behöver vi en egen container för kategorier för att göra dem sökbara, eller räcker det med HasPartitionKey ovan?
        //modelBuilder.Entity<CategoryEntity>().ToContainer("Categories");
        //modelBuilder.Entity<CategoryEntity>().OwnsMany(c => c.Products);
    }
}
