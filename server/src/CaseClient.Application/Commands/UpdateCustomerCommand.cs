using CaseClient.Application.Notifications.Abstractions;
using CaseClient.Application.Notifications;
using CaseClient.Core.Abstractions;
using CaseClient.Core.Data;
using CaseClient.Domain.Abstractions;
using CaseClient.Domain.Entities;
using CaseClient.Domain.Events;
using MediatR;
using static CaseClient.Domain.Entities.Customer;

namespace CaseClient.Application.Commands;

public record UpdateCustomerCommand(Guid Id, string CompanyName, SizeCategory CompanySize) : ICommand;
public class UpdateCustomerCommandHandler : ICommandHandler<UpdateCustomerCommand>
{
    private readonly ICustomerWriteRepository _customerWriteRepository;
    private readonly ICustomerReadRepository _customerReadRepository;
    private readonly IMediator _mediator;
    private IUnitOfWork _unitOfWork;
    private readonly INotifier _notifier;  

    public UpdateCustomerCommandHandler(ICustomerWriteRepository customerWriteRepository,
                                        IUnitOfWork unitOfWork,
                                        ICustomerReadRepository customerReadRepository,
                                        IMediator mediator,
                                        INotifier notifier)
    {
        _customerWriteRepository = customerWriteRepository;
        _customerReadRepository = customerReadRepository;
        _unitOfWork = unitOfWork;
        _mediator = mediator;
        _notifier = notifier; 
    }

    public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerReadRepository.GetCustomerById(request.Id);
        if (customer == null)
        {
            _notifier.Add(new Notification("Customer not found."));
            return;  
        }

        customer.Update(request.CompanyName, request.CompanySize);
        await _customerWriteRepository.UpdateCustomer(customer);

        await _mediator.Publish(new CustomerUpdatedEvent(customer));

        await _unitOfWork.Commit();
    }
}

