// To use [Route], [ApiController], ControllerBase and so on.
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Northwind.EntityModels;
using Northwind.WebApi.Repositories; // To use ICustomerRepository.

namespace Northwind.WebApi.Controllers
{
    // Base address: api/customers
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _repo;

        // Constructor injects repository registered in Program.cs.
        public CustomersController(ICustomerRepository repo)
        {
            _repo = repo;
        }

        // GET: api/customers
        // GET: api/customers/?country=[country]
        // this will always return a list of customers (but it might be empty)
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Customer>))]
        public async Task<IEnumerable<Customer>> GetCustomers(string? country)
        {
            Customer[] customers = await _repo.RetrieveAllAsync();

            if (string.IsNullOrWhiteSpace(country))
            {
                return customers;
            }
            else
            {
                return customers.Where(c => c.Country == country);
            }
        }

        // GET: api/customers/<id>
        [HttpGet("{id}", Name = nameof(GetCustomer))] // Named route.
        [ProducesResponseType(200, Type = typeof(Customer))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCustomer(string id)
        {
            Customer? customer = await _repo.RetrieveAsync(id);
            if (customer is null)
            {
                return NotFound(); // 404 Resource not found.
            }
            return Ok(customer); // 200 OK with customer in body.
        }

        // POST: api/customers
        // BODY: Customer (JSON, XML)
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Customer))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            if (customer is null)
            {
                return BadRequest(); // 400 Bad request.
            }
            
            Customer? addedCustomer = await _repo.CreateAsync(customer);
            if (addedCustomer is null)
            {
                ProblemDetails problemDetails = new()
                {
                    Type = "https://localhost:5151/customers/failed-to-create",
                    Title = $"Failed to create customer.",
                    Instance = HttpContext.Request.Path,
                };
                return BadRequest(problemDetails);
            }
            else
            {
                return CreatedAtRoute( // 201 Created.
                    routeName: nameof(GetCustomer), 
                    routeValues: new { id = addedCustomer.CustomerId.ToLower() },
                    value: customer);
            }
        }

        // PUT: api/customers/<id>
        // BODY: Customer (JSON, XML)
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(
            string id, [FromBody] Customer customer)
        {
            id = id.ToUpper();
            customer.CustomerId = customer.CustomerId.ToUpper();
            if (customer is null || customer.CustomerId != id)
            {
                return BadRequest(); // 400 Bad request.
            }
            Customer? existing = await _repo.RetrieveAsync(id);
            if (existing is null)
            {
                return NotFound(); // 404 Resource not found.
            }
            await _repo.UpdateAsync(customer);
            return new NoContentResult(); // 204 No content.
        }

        // DELETE: api/customers/<id>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(string id)
        {
            // Take control of problem details.
            if (id == "bad")
            {
                ProblemDetails problemDetails = new()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Type = "https://localhost:5151/customers/failed-to-delete",
                    Title = $"Customer ID {id} found but failed to delete.",
                    Detail = "More details like Company Name, Country and so on.",
                    Instance = HttpContext.Request.Path
                };
                return BadRequest(problemDetails); // 400 Bad Request
            }

            Customer? existing = await _repo.RetrieveAsync(id);
            if (existing is null)
            {
                return NotFound(); // 404 Resource not found.
            }
            bool? deleted = await _repo.DeleteAsync(id);
            if (deleted.HasValue && deleted.Value)
            {
                return new NoContentResult(); // 204 No content.
            }
            else
            {
                return BadRequest( // 400 Bad request.
                    $"Customer {id} was found but failed to delete.");
            }
        }
    }
}
