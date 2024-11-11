using CaseClient.Domain.Entities;
using FluentValidation;

namespace CaseClient.Application.Validations;

public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(c => c.CompanyName)
            .NotEmpty().WithMessage("Company name is required.")
            .MinimumLength(3).WithMessage("Company name must be at least 3 characters long.")
            .MaximumLength(100).WithMessage("Company name must be less than 100 characters.");

        RuleFor(c => c.CompanySize)
            .IsInEnum().WithMessage("Invalid company size.");
    }
}
