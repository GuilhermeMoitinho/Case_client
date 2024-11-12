using CaseClient.Core;
using CaseClient.Data.CustomerContext;
using CaseClient.Domain.Abstractions;
using CaseClient.Domain.Entities;
using MongoDB.Driver;

namespace CaseClient.Data.Repositories;

public class CustomerReadRepository : ICustomerReadRepository
{
    private readonly MongoDbContext _mongoDbContext;

    public CustomerReadRepository(MongoDbContext mongoDbContext)
    {
        _mongoDbContext = mongoDbContext;
    }

    public async Task<IEnumerable<Customer>> GetAllCustomer(Pagination pagination)
    {
        var filter = Builders<Customer>.Filter.Empty;
        var Customers = await _mongoDbContext.Customers
            .Find(filter)
            .Skip(pagination.Offset)
            .Limit(pagination.Limit)
            .ToListAsync();

        return Customers;
    }

    public async Task<Customer> GetCustomerById(Guid id)
    {
        var filter = Builders<Customer>.Filter.Eq(c => c.Id, id);
        var Customer = await _mongoDbContext.Customers
            .Find(filter)
            .FirstOrDefaultAsync();

        return Customer;
    }

    public async Task CreateCustomer(Customer Customer)
    {
        await _mongoDbContext.Customers.InsertOneAsync(Customer);
    }

    public async Task RemoveCustomer(Customer Customer)
    {
        var filter = Builders<Customer>.Filter.Eq(c => c.Id, Customer.Id);
        await _mongoDbContext.Customers.DeleteOneAsync(filter);
    }

    public async Task UpdateCustomer(Customer Customer)
    {
        var filter = Builders<Customer>.Filter.Eq(c => c.Id, Customer.Id);
        await _mongoDbContext.Customers.ReplaceOneAsync(filter, Customer);
    }
}
