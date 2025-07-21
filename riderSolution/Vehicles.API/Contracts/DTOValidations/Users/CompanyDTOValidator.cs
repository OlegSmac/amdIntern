using FluentValidation;
using Vehicles.API.Contracts.DTOs.Users;

namespace Vehicles.API.Contracts.DTOValidations.Users;

public class CompanyDTOValidator : AbstractValidator<CompanyDTO>
{
    public CompanyDTOValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description cannot exceed 500 characters.");
    }
}
