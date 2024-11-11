using CaseClient.Core;
using CaseClient.Data.CustomerContext;
using CaseClient.Domain.Abstractions;
using CaseClient.Domain.Entities;
using MongoDB.Driver;

namespace CaseClient.Data.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly PostgreSqlDbContext _postgreSqlDbContext;
    private readonly MongoDbContext _mongoDbContext;

    public CustomerRepository(PostgreSqlDbContext PostgreSqlDbContext, MongoDbContext mongoDbContext)
    {
        _postgreSqlDbContext = PostgreSqlDbContext;
        _mongoDbContext = mongoDbContext;
    }

    public async Task CreateCustomer(Customer Customer)
    {
        await _postgreSqlDbContext.Customers.AddAsync(Customer);
        await _postgreSqlDbContext.SaveChangesAsync(); 

        await _mongoDbContext.Customers.InsertOneAsync(Customer);
    }

    public async Task RemoveCustomer(Customer Customer)
    {
        _postgreSqlDbContext.Customers.Remove(Customer);
        await _postgreSqlDbContext.SaveChangesAsync(); 

        var filter = Builders<Customer>.Filter.Eq(c => c.Id, Customer.Id);
        await _mongoDbContext.Customers.DeleteOneAsync(filter);
    }

    public async Task UpdateCustomer(Customer Customer)
    {
        _postgreSqlDbContext.Customers.Update(Customer);
        await _postgreSqlDbContext.SaveChangesAsync(); 

        var filter = Builders<Customer>.Filter.Eq(c => c.Id, Customer.Id);
        await _mongoDbContext.Customers.ReplaceOneAsync(filter, Customer);
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
}
