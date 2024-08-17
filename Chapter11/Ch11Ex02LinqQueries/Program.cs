using Ch11Ex02LinqQueries.AutoGenModels;

using NorthwindDb db = new();

string[] cities = db.Customers.Select(c => c.City).Distinct().ToArray()!;
WriteLine(string.Join(", ", cities));

Write("Enter the name of a city: ");
string city = ReadLine()!;

List<Customer> customers = db.Customers
            .Where(c => c.City == city).ToList();

WriteLine($"There are {customers.Count} customers in {city}:");
foreach (Customer customer in customers)
{
    WriteLine($"  {customer.CompanyName}");
}
