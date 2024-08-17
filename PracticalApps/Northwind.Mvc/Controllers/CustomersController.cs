using Microsoft.AspNetCore.Mvc;
using Northwind.EntityModels;

namespace Northwind.Mvc.Controllers
{
    public class CustomersController : Controller
    {
        private readonly string _httpClientName = "Northwind.WebApi";
        private readonly IHttpClientFactory _httpClientFactory;

        public CustomersController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: CustomersController
        public async Task<IActionResult> Index(string country)
        {
            string uri;

            if (string.IsNullOrWhiteSpace(country))
            {
                ViewData["Title"] = "All Customers Worldwide";
                uri = "api/customers";
            }
            else
            {
                ViewData["Title"] = $"Customers in {country}";
                uri = $"api/customers?country={country}";
            }

            HttpClient httpClient = _httpClientFactory.CreateClient(
                name: "Northwind.WebApi");

            HttpRequestMessage request = new(
                method: HttpMethod.Get, requestUri: uri);

            HttpResponseMessage response = await httpClient.SendAsync(request);

            IEnumerable<Customer>? model = await response.Content
                .ReadFromJsonAsync<IEnumerable<Customer>>();

            return View(model);
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "Create Customer";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomersCreateViewModel model)
        {
            HttpRequestMessage request = new(HttpMethod.Post, "api/Customers")
            {
                Content = JsonContent.Create(model)
            };

            HttpClient httpClient = _httpClientFactory.CreateClient(_httpClientName);

            HttpResponseMessage response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            HttpRequestMessage request = new(HttpMethod.Delete, $"api/Customers/{id}");

            HttpClient httpClient = _httpClientFactory.CreateClient(_httpClientName);

            HttpResponseMessage response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
