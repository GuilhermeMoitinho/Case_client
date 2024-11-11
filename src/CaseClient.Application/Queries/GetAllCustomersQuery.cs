using CaseClient.Core;
using CaseClient.Core.Abstractions;
using CaseClient.Domain.Abstractions;
using CaseClient.Domain.Entities;

namespace CaseClient.Application.Queries;

public record GetAllCustomersQuery(Pagination pagination) : IQuery<IEnumerable<Customer>>;

public class GetAllCustomersQueryHandler : IQueryHandler<GetAllCustomersQuery, IEnumerable<Customer>>
{
    private readonly ICustomerRepository _CustomerRepository;

    public GetAllCustomersQueryHandler(ICustomerRepository CustomerRepository) => _CustomerRepository = CustomerRepository;

    public async Task<IEnumerable<Customer>> Handle(GetAllCustomersQuery query, CancellationToken cancellationToken)
    {
        return await _CustomerRepository.GetAllCustomer(query.pagination);
    }
}

