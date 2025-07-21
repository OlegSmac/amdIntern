using FluentValidation;
using Vehicles.API.Contracts.DTOs.Posts;

namespace Vehicles.API.Contracts.DTOValidations.Posts;

public class PostDTOValidator : AbstractValidator<PostDTO>
{
    public PostDTOValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(500);

        RuleFor(x => x.Body)
            .NotEmpty().WithMessage("Body is required.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be 0 or higher.");

        RuleFor(x => x.Company)
            .NotNull().WithMessage("Company information is required.");

        RuleFor(x => x.Vehicle)
            .NotNull().WithMessage("Vehicle information is required.");
    }
}
