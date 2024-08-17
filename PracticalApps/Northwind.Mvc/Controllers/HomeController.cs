using System.Diagnostics; // To use Activity.
using Microsoft.AspNetCore.Authorization; // To use [Authorize].
using Microsoft.AspNetCore.Mvc; // To use Controller, IActionResult.
using Microsoft.EntityFrameworkCore; // To use Include and ToListAsync extension methods.
using Northwind.EntityModels; // To use NorthwindContext.
using Northwind.Mvc.Models; // To use ErrorViewModel.

namespace Northwind.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly NorthwindContext _db;
    private readonly IHttpClientFactory _httpClientFactory;

    public HomeController(
        ILogger<HomeController> logger, 
        NorthwindContext db,
        IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _db = db;
        _httpClientFactory = httpClientFactory;
    }

    [ResponseCache(Duration = 10 /* seconds */, Location = ResponseCacheLocation.Any)]
    public async Task<IActionResult> Index()
    {
        _logger.LogError("This is a serious error (not really!)");
        _logger.LogWarning("This is your first warning!");
        _logger.LogWarning("Second warning!");
        _logger.LogInformation("I am in the index method of the HomeController.");

        HomeIndexViewModel model = new
        (
            VisitorCount: Random.Shared.Next(1, 1001),
            Categories: await _db.Categories.ToListAsync(),
            Products: await _db.Products.ToListAsync()
        );

        try
        {
            HttpClient httpClient = _httpClientFactory.CreateClient(
                name: "Northwind.MinimalApi");

            HttpRequestMessage request = new(
                method: HttpMethod.Get, requestUri: "todos");

            HttpResponseMessage response = await httpClient.SendAsync(request);

            ViewData["todos"] = await response.Content
                .ReadFromJsonAsync<Todo[]>();
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"The Northwind.MinimalApi service is not responding. Exception: {ex.Message}");

            ViewData["todos"] = Array.Empty<Todo>();
        }

        return View(model);
    }

    [Route("private")]
    [Authorize(Roles = "Administrators")]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public async Task<IActionResult> ProductDetail(int? id, string alertStyle = "success")
    {
        ViewData["alertStyle"] = alertStyle;

        if (!id.HasValue)
        {
            return BadRequest("You must pass a product ID in the route, for example, /Home/ProductDetail/21");
        }

        Product? model = await _db.Products.Include(p => p.Category)
            .SingleOrDefaultAsync(p => p.ProductId == id);

        if (model is null)
        {
            return NotFound($"ProductId {id} not found.");
        }

        return View(model); // Pass model to view and then return result.
    }

    // This action method will handle GET and other requests except POST.
    public IActionResult ModelBinding()
    {
        return View();
    }

    [HttpPost] // This action method will handle POST requests.
    [ValidateAntiForgeryToken]
    public IActionResult ModelBinding(Thing thing)
    {
        HomeModelBindingViewModel model = new(
            Thing: thing, HasErrors: !ModelState.IsValid,
            ValidationErrors: ModelState.Values
                .SelectMany(state => state.Errors)
                .Select(error => error.ErrorMessage)
        );

        return View(model); // Show the model bound thing.
    }

    public IActionResult ProductsThatCostMoreThan(decimal? price)
    {
        if (!price.HasValue)
        {
            return BadRequest("You must pass a product price in the query string, for example, /Home/ProductsThatCostMoreThan?price=50");
        }

        IEnumerable<Product> model = _db.Products
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .Where(p => p.UnitPrice > price);

        if (!model.Any())
        {
            return NotFound($"No products cost more than {price:C}.");
        }

        ViewData["MaxPrice"] = price.Value.ToString("C");

        return View(model);
    }

    public async Task<IActionResult> CategoryDetail(int? id)
    {
        if (!id.HasValue)
        {
            return BadRequest("You must pass a category ID in the route, for example, /Home/CategoryDetail/6");
        }

        Category? model = await _db.Categories.Include(c => c.Products)
            .SingleOrDefaultAsync(c => c.CategoryId == id);

        if (model is null)
        {
            return NotFound($"CategoryId {id} not found.");
        }

        return View(model); // Pass model to view and then return result.
    }
}
