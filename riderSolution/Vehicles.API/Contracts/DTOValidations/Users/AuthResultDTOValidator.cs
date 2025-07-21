using FluentValidation;
using Vehicles.API.Contracts.DTOs.Users;

namespace Vehicles.API.Contracts.DTOValidations.Users;

public class AuthResultDTOValidator : AbstractValidator<AuthResultDTO>
{
    public AuthResultDTOValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role is required.");

        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Token is required.");
    }
}
