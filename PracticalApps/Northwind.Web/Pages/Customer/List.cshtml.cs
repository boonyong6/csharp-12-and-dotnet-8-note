using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Northwind.EntityModels;

namespace Northwind.Web.Pages.Customer;

public class ListModel : PageModel
{
    private readonly NorthwindContext _db;

    public ListModel(NorthwindContext db)
    {
        _db = db;
    }

    public IGrouping<string?, EntityModels.Customer>[] CustomerGroups { get; set; } = null!;
    public int CustomerCount { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPageNumber { get; set; } = 1;

    public IActionResult OnGet(int pageNumber = 1, int pageSize = 10)
    {
        if (pageNumber < 1)
        {
            return NotFound();
        }

        CustomerGroups = _db.Customers
            .OrderBy(c => c.Country).ThenBy(c => c.CompanyName)
            .Skip((pageNumber - 1) * pageSize).Take(pageSize)
            .GroupBy(c => c.Country)
            .ToArray();

        if (CustomerGroups.Length == 0)
        {
            return NotFound();
        }

        CustomerCount = _db.Customers.Count();
        TotalPages = (int)Math.Ceiling((double)CustomerCount / pageSize);
        CurrentPageNumber = pageNumber;

        ViewData["Title"] = "List of Customers Grouped by Country";

        return Page();
    }
}
