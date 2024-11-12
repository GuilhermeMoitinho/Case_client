using CaseClient.Core.Data;
using CaseClient.Domain.Entities;

namespace CaseClient.Domain.Abstractions;

public interface ICustomerWriteRepository : IRepository<Customer>
{
    Task CreateCustomer(Customer Customer);

    Task UpdateCustomer(Customer Customer);

    Task RemoveCustomer(Customer Customer);
}
