﻿using CaseClient.Application.Commands;
using CaseClient.Application.Queries;
using CaseClient.Application.ViewModels;
using CaseClient.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CaseClient.Application.Mappers;
using CaseClient.WebAPI.Controllers.Base;
using CaseClient.Application.Notifications.Abstractions;

namespace CaseClient.WebAPI.Controllers.Customer;

/// <summary>
/// Manages customer-related operations
/// </summary>
[Route("api/v1/customers")]
[ApiController]
[Produces("application/json")]
[Tags("Customers")]
public class CustomersController : MainController
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomersController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator for handling requests.</param>
    public CustomersController(IMediator mediator, INotifier notifier) : base(notifier) => _mediator = mediator;

    /// <summary>
    /// Creates a new customer.
    /// </summary>
    /// <param name="request">The customer creation request details.</param>
    /// <returns>A response indicating the status of the operation.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCustomer(CustomerRequest request)
    {
        var command = new CreateCustomerCommand(request.CompanyName, request.CompanySize);

        var customerId = await _mediator.Send(command);

        if (!OperationIsValid())
        {
            return NotFound(new Response(ReturnErrors(), false, null, null));
        }

        return CreatedAtAction(nameof(GetCustomerById), new { id = customerId }, new { id = customerId });
    }

    /// <summary>
    /// Retrieves all customers with pagination.
    /// </summary>
    /// <param name="limit">The maximum number of customers to return.</param>
    /// <param name="offset">The number of customers to skip before starting to return results.</param>
    /// <returns>A paginated list of customers.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CustomerViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllCustomers(int offset = 0, int limit = 15)
    {
        var query = new GetAllCustomersQuery(new Pagination(limit, offset));

        var Customers = await _mediator.Send(query);

        if (!OperationIsValid())
        {
            return NotFound(new Response(ReturnErrors(), false, null, null));
        }

        return Ok(new Response(Customers.EntitiesToVms(), true, limit, offset));
    }

    /// <summary>
    /// Retrieves a customer by its unique ID.
    /// </summary>
    /// <param name="id">The unique identifier of the customer.</param>
    /// <returns>The customer details.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CustomerViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCustomerById(Guid id)
    {
        var query = new GetCustomerByIdQuery(id);

        var Customer = await _mediator.Send(query);

        if (!OperationIsValid())
        {
            return NotFound(new Response(ReturnErrors(), false, null, null));
        }

        return Ok(new Response(Customer.EntityToVm(), true, null, null));
    }

    /// <summary>
    /// Updates an existing customer.
    /// </summary>
    /// <param name="id">The unique identifier of the customer to update.</param>
    /// <param name="request">The customer update request details.</param>
    /// <returns>A response indicating the result of the update operation.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCustomer(Guid id, CustomerRequest request)
    {
        var command = new UpdateCustomerCommand(id, request.CompanyName, request.CompanySize);
        await _mediator.Send(command);

        if (!OperationIsValid())
        {
            return NotFound(new Response(ReturnErrors(), false, null, null));
        }

        return NoContent();
    }

    /// <summary>
    /// Deletes a customer by its unique ID.
    /// </summary>
    /// <param name="id">The unique identifier of the customer to delete.</param>
    /// <returns>A response indicating the result of the delete operation.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCustomer(Guid id)
    {
        var command = new RemoveCustomerCommand(id);
        await _mediator.Send(command);

        if (!OperationIsValid())
        {
            return NotFound(new Response(ReturnErrors(), false, null, null));
        }

        return NoContent();
    }
}
