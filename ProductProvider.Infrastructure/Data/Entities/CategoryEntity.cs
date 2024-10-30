using System.ComponentModel.DataAnnotations;

namespace ProductProvider.Infrastructure.Data.Entities;

public class CategoryEntity
{
    [Key]
    //public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = null!;
    public string? CategoryDescription { get; set; }

}
