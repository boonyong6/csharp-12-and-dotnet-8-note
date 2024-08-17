using System.ComponentModel.DataAnnotations;

namespace Northwind.Mvc;

public record CustomersCreateViewModel
{
    [Display(Name = "Customer Id")]
    [RegularExpression("[a-zA-Z]{5}", ErrorMessage = "Must be 5 letters long.")]
    public required string CustomerId { get; init; }
    [Display(Name = "Company Name")]
    public required string CompanyName { get; init; }
    [StringLength(15)]
    public string? Country { get; set; }
};
