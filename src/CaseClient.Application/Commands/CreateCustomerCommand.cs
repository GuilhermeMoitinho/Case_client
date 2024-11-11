using CaseClient.Core.Abstractions;
using CaseClient.Domain.Abstractions;
using CaseClient.Domain.Entities;
using static CaseClient.Domain.Entities.Customer;

namespace CaseClient.Application.Commands;

public record CreateCustomerCommand(string CompanyName, SizeCategory CompanySize) : ICommand;

public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand>
{
    private readonly ICustomerRepository _CustomerRepository;

    public CreateCustomerCommandHandler(ICustomerRepository CustomerRepository) => _CustomerRepository = CustomerRepository;

    public async Task Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        await _CustomerRepository.CreateCustomer(new Customer(request.CompanyName, request.CompanySize));
    }
}
