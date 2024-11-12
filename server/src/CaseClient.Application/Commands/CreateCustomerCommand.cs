using CaseClient.Core.Abstractions;
using CaseClient.Core.Data;
using CaseClient.Domain.Abstractions;
using CaseClient.Domain.Entities;
using CaseClient.Domain.Events;
using MediatR;
using static CaseClient.Domain.Entities.Customer;

namespace CaseClient.Application.Commands;

public record CreateCustomerCommand(string CompanyName, SizeCategory CompanySize) : ICommand<Guid>;

public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand, Guid>
{
    private readonly ICustomerWriteRepository _customerRepository;
    private readonly IMediator _mediator;
    private IUnitOfWork _unitOfWork;

    public CreateCustomerCommandHandler(ICustomerWriteRepository customerRepository, IUnitOfWork unitOfWork, IMediator mediator)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Customer(request.CompanyName, request.CompanySize);
        await _customerRepository.CreateCustomer(customer);

        await _mediator.Publish(new CustomerCreatedEvent(customer));

        await _unitOfWork.Commit();


        return customer.Id;
    }
}
