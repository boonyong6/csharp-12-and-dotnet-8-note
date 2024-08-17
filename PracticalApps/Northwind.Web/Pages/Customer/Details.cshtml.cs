using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Northwind.EntityModels;

namespace Northwind.Web.Pages.Customer
{
    public class DetailsModel : PageModel
    {
        private NorthwindContext _db;

        public DetailsModel(NorthwindContext db)
        {
            _db = db;
        }

        public EntityModels.Customer Customer { get; set; } = null!;

        public async Task<IActionResult> OnGet(string? customerId)
        {
            if (customerId is null)
            {
                return NotFound();
            }

            EntityModels.Customer? customer = await _db.Customers
                .Include(c => c.Orders)
                .ThenInclude(o => o.OrderDetails)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (customer is null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Customer Details";

            Customer = customer;

            return Page();
        }

        public string GetFullAddress(EntityModels.Customer customer)
        {
            string?[] addressParts =
            [
                customer.Address,
                customer.City,
                customer.Region,
                customer.PostalCode,
                customer.Country,
            ];

            string fullAddress = string.Join(", ", addressParts
                .Where(p => !string.IsNullOrWhiteSpace(p)));

            return fullAddress;
        }
    }
}
