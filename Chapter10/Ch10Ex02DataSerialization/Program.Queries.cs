using Ch10Ex02DataSerialization.AutoGenModels;
using Microsoft.EntityFrameworkCore.ChangeTracking;

partial class Program
{
    private static ICollection<Category> QueryAllCategories()
    {
        using NorthwindDb db = new();

        return db.Categories.ToList();
    }

    private static ICollection<Product> QueryAllProducts()
    {
        using NorthwindDb db = new();

        return db.Products.ToList();
    }
}
