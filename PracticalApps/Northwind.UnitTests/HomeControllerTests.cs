using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Northwind.EntityModels;
using Northwind.Mvc.Controllers;
using Northwind.Mvc.Models;

namespace Northwind.UnitTests;

/*
- A controller unit test avoids scenarios such as filters, routing, and model binding.
- Interactions among components are handled by integration tests.

SQLite in-memory
- A new database is created whenever a low-level connection is opened, and that 
  it's deleted when that connection is closed.
- In normal usage, EF Core's DbContext opens and closes database connections as 
  needed, such as every time a query is executed.
*/

public class HomeControllerTests : IDisposable
{
    private readonly DbConnection _connection;
    private readonly DbContextOptions<NorthwindContext> _contextOptions;
    private readonly ILogger<HomeController> _logger = new Mock<ILogger<HomeController>>().Object;

    public HomeControllerTests()
    {
        // Create and open a connection. This creates the SQLite in-memory 
        // database, which will persist until the connection is closed at the 
        // end of the test (see Dispose below).
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        // These options will be used by the context instances in this test 
        // suite, including the connection opened above.
        _contextOptions = new DbContextOptionsBuilder<NorthwindContext>()
            .UseSqlite(_connection)
            .Options;

        // Create the schema and seed some data
        using NorthwindContext db = new(_contextOptions);
        db.Database.EnsureCreated();

        Category[] categories =
        [
            new() { CategoryId = 1, CategoryName = "Grains" },
            new() { CategoryId = 2, CategoryName = "Meat" },
        ];
        Product[] products =
        [
            new() { ProductId = 1, ProductName = "Filo Mix", CategoryId = 1  },
            new() { ProductId = 2, ProductName = "Alice Mutton", CategoryId = 2  },
            new() { ProductId = 3, ProductName = "Perth Pasties", CategoryId = 2  },
        ];

        db.Categories.AddRange(categories);
        db.Products.AddRange(products);

        db.SaveChanges();
    }

    private NorthwindContext CreateNorthwindContext() => new NorthwindContext(_contextOptions);

    public void Dispose() => _connection.Dispose();

    [Fact]
    public async Task Index_ReturnsAViewResult_WithAListOfCategoriesAndProducts()
    {
        // Arrange
        using NorthwindContext db = CreateNorthwindContext();
        HomeController controller = new(_logger, db);

        // Act
        IActionResult actionResult = await controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(actionResult);
        var model = Assert.IsAssignableFrom<HomeIndexViewModel>(viewResult.Model);
        Assert.Equal(2, model.Categories.Count);
        Assert.Equal(3, model.Products.Count);
    }

    [Fact]
    public async Task ProductDetail_ReturnsBadRequestResult_WhenIdParameterHasNoValue()
    {
        using NorthwindContext db = CreateNorthwindContext();
        HomeController controller = new(_logger, db);

        IActionResult actionResult = await controller.ProductDetail(null);

        Assert.IsType<BadRequestObjectResult>(actionResult);
    }

    [Fact]
    public async Task ProductDetail_ReturnsNotFoundResult_WhenProductIsNotFound()
    {
        using NorthwindContext db = CreateNorthwindContext();
        HomeController controller = new(_logger, db);

        IActionResult actionResult = await controller.ProductDetail(6);

        Assert.IsType<NotFoundObjectResult>(actionResult);
    }

    [Fact]
    public async Task ProductDetail_ReturnsViewResult_WhenProductIsFound()
    {
        using NorthwindContext db = CreateNorthwindContext();
        HomeController controller = new(_logger, db);
        string expectedProductName = "Perth Pasties";

        IActionResult actionResult = await controller.ProductDetail(3);

        var viewResult = Assert.IsType<ViewResult>(actionResult);
        var model = Assert.IsAssignableFrom<Product>(viewResult.Model);
        Assert.Equal(expectedProductName, model.ProductName);
    }
}
