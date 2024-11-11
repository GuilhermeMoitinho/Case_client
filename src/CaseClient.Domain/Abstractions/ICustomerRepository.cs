using CaseClient.Core;
using CaseClient.Core.Data;
using CaseClient.Domain.Entities;

namespace CaseClient.Domain.Abstractions;

public interface ICustomerRepository : IRepository<Customer>
{
    Task CreateCustomer(Customer Customer);

    Task UpdateCustomer(Customer Customer);

    Task RemoveCustomer(Customer Customer);

    Task<IEnumerable<Customer>> GetAllCustomer(Pagination pagination);

    Task<Customer> GetCustomerById(Guid id);
}
