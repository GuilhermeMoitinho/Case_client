using CaseClient.Application.ViewModels;
using CaseClient.Domain.Entities;

namespace CaseClient.Application.Mappers;

public static class CustomerMapper
{
    public static CustomerViewModel EntityToVm(this Customer entity) => new CustomerViewModel(entity.Id, entity.CompanyName, entity.CompanySize);

    public static IEnumerable<CustomerViewModel> EntitiesToVms(this IEnumerable<Customer> entity) => entity.Select(viewModel => viewModel.EntityToVm());
}
