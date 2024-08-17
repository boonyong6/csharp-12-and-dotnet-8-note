using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Northwind.Web.Pages
{
    public class TimesTable
    {
        public byte Number { get; set; } = 5;
        public byte Size { get; set; } = 6;
        public string[] Result { get; set; } = Array.Empty<string>();
    }

    public class Tax
    {
        [Display(Name = "Taxable Amount")]
        public decimal TaxableAmount { get; set; } = 6_666.66M;
        [Display(Name = "Two Letter Region Code")]
        public string? TwoLetterRegionCode { get; set; } = "FR";
        public decimal Amount { get; set; }
    }

    public class Factorial
    {
        public int Number { get; set; } = 3;
        public int Result { get; set; }
    }

    public class Fibonacci
    {
        public uint Number { get; set; } = 16;
        public int Result { get; set; }
    }

    public class FunctionsModel : PageModel
    {
        [BindProperty]
        public TimesTable TimesTable { get; set; } = new();

        [BindProperty]
        public Tax Tax { get; set; } = new();

        [BindProperty]
        public Factorial Factorial { get; set; } = new();

        [BindProperty]
        public Fibonacci Fibonacci { get; set; } = new();

        public void OnGet()
        {
        }

        public void OnPost()
        {
            CalculateTimesTable();
            CalculateTax();
            Factorial.Result = CalculateFactorial(Factorial.Number);
            Fibonacci.Result = CalculateFibonacci(Fibonacci.Number);
        }

        private void CalculateTimesTable()
        {
            TimesTable.Result = new string[TimesTable.Size];

            for (int row = 1; row <= TimesTable.Size; row++)
            {
                TimesTable.Result[row - 1] = $"{row} x {TimesTable.Number} = {row * TimesTable.Number}";
            }
        }

        private void CalculateTax()
        {
            decimal rate = Tax.TwoLetterRegionCode switch
            {
                "CH" => 0.08M, // Switzerland
                "DK" or "NO" => 0.25M, // Denmark, Norway
                "GB" or "FR" => 0.2M, // UK, France
                "HU" => 0.27M, // Hungary
                "OR" or "AK" or "MT" => 0.0M, // Oregon, Alaska, Montana
                "ND" or "WI" or "ME" or "VA" => 0.05M,
                "CA" => 0.0825M, // California
                _ => 0.06M // Most other states.
            };

            Tax.Amount = Tax.TaxableAmount * rate;
        }

        private int CalculateFactorial(int number)
        {
            if (number < 0)
            {
                throw new ArgumentOutOfRangeException(
                    message: $"The factorial function is defined for non-negative integers only. Input: {number}",
                    paramName: nameof(number));
            }
            else if (number == 0)
            {
                return 1;
            }
            else
            {
                checked // check for overflow
                {
                    return number * CalculateFactorial(number - 1);
                }
            }
        }

        private int CalculateFibonacci(uint term) => term switch
        {
            0 => throw new ArgumentOutOfRangeException(),
            1 => 0,
            2 => 1,
            _ => CalculateFibonacci(term - 1) + CalculateFibonacci(term - 2)
        };
    }
}
