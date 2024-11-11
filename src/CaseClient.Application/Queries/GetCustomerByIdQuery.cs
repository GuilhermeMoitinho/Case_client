using CaseClient.Core.Abstractions;
using CaseClient.Domain.Abstractions;
using CaseClient.Domain.Entities;

namespace CaseClient.Application.Queries;

public record GetCustomerByIdQuery(Guid Id) : IQuery<Customer>;

public class GetCustomerByIdQueryHandler : IQueryHandler<GetCustomerByIdQuery, Customer>
{
    private readonly ICustomerRepository _CustomerRepository;

    public GetCustomerByIdQueryHandler(ICustomerRepository CustomerRepository) => _CustomerRepository = CustomerRepository;

    public async Task<Customer> Handle(GetCustomerByIdQuery query, CancellationToken cancellationToken)
    {
        return await _CustomerRepository.GetCustomerById(query.Id);
    }
}
