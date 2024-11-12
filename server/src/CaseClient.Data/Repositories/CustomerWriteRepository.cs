using CaseClient.Data.CustomerContext;
using CaseClient.Domain.Abstractions;
using CaseClient.Domain.Entities;

namespace CaseClient.Data.Repositories;

public class CustomerWriteRepository : ICustomerWriteRepository
{
    private readonly PostgreSqlDbContext _postgreSqlDbContext;

    public CustomerWriteRepository(PostgreSqlDbContext postgreSqlDbContext)
    {
        _postgreSqlDbContext = postgreSqlDbContext;
    }

    public async Task CreateCustomer(Customer Customer)
    {
        await _postgreSqlDbContext.Customers.AddAsync(Customer);
    }

    public async Task RemoveCustomer(Customer Customer)
    {
        _postgreSqlDbContext.Customers.Remove(Customer);
    }

    public async Task UpdateCustomer(Customer Customer)
    {
        _postgreSqlDbContext.Customers.Update(Customer);
    }
}
    