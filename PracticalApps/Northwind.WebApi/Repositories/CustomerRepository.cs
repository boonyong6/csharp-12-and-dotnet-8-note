using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Caching.Memory;
using Northwind.EntityModels;

namespace Northwind.WebApi.Repositories;

public class CustomerRepository : ICustomerRepository
{
    // Use an instance data context field because it should not be 
    // cached due to the data context having internal caching.
    private readonly NorthwindContext _db; // !!
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<CustomerRepository> _logger;
    private readonly MemoryCacheEntryOptions _cacheEntryOptions = new()
    {
        SlidingExpiration = TimeSpan.FromMinutes(30),
    };

    public CustomerRepository(NorthwindContext db, IMemoryCache memoryCache, ILogger<CustomerRepository> logger)
    {
        _db = db;
        _memoryCache = memoryCache;
        _logger = logger;
    }
    
    public async Task<Customer?> CreateAsync(Customer customer)
    {
        customer.CustomerId = customer.CustomerId.ToUpper(); // Normalize to uppercase.

        // Add to database using EF Core.
        EntityEntry<Customer> added = await _db.Customers.AddAsync(customer);
        try
        {
            int affected = await _db.SaveChangesAsync();
            if (affected == 1)
            {
                // If saved to database then store in cache.
                _memoryCache.Set(customer.CustomerId, customer, _cacheEntryOptions);
                return customer;
            }
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"===> ex.Message");
            return null;
        }
        return null;
    }

    public Task<Customer[]> RetrieveAllAsync()
    {
        return _db.Customers.ToArrayAsync();
    }

    public async Task<Customer?> RetrieveAsync(string id)
    {
        id = id.ToUpper(); // Normalize to uppercase.

        // Try to get from the cache first.
        if (_memoryCache.TryGetValue(id, out Customer? fromCache))
        {
            return fromCache;
        }

        // If not in the cache, then try to get it from the database.
        Customer? fromDb = await _db.Customers.AsNoTracking()
            .FirstOrDefaultAsync(c => c.CustomerId == id);

        // If in the database, then store in the cache and return customer.
        if (fromDb is not null)
        {
            _memoryCache.Set(fromDb.CustomerId, fromDb, _cacheEntryOptions);
        }

        return fromDb;
    }

    public async Task<Customer?> UpdateAsync(Customer customer)
    {
        customer.CustomerId = customer.CustomerId.ToUpper();

        _db.Customers.Update(customer);
        int affected = await _db.SaveChangesAsync();
        if (affected == 1)
        {
            _memoryCache.Set(customer.CustomerId, customer, _cacheEntryOptions);
            return customer;
        }
        return null;
    }

    public async Task<bool?> DeleteAsync(string id)
    {
        id = id.ToUpper();

        Customer? customer = await _db.Customers.FindAsync(id);
        
        if (customer is null) return null;

        _db.Customers.Remove(customer);
        int affected = await _db.SaveChangesAsync();
        if (affected == 1)
        {
            _memoryCache.Remove(id);
            return true;
        }
        return null;
    }
}
