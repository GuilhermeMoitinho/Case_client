using CaseClient.Core.Abstractions;
using CaseClient.Domain.Abstractions;
using static CaseClient.Domain.Entities.Customer;

namespace CaseClient.Application.Commands;

public record UpdateCustomerCommand(Guid Id, string CompanyName, SizeCategory CompanySize) : ICommand;

public class UpdateCustomerCommandHandler : ICommandHandler<UpdateCustomerCommand>
{
    private readonly ICustomerRepository _CustomerRepository;

    public UpdateCustomerCommandHandler(ICustomerRepository CustomerRepository) => _CustomerRepository = CustomerRepository;

    public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var Customer = await _CustomerRepository.GetCustomerById(request.Id);

        Customer.Update(request.CompanyName, request.CompanySize);

        await _CustomerRepository.UpdateCustomer(Customer);
    }
}
