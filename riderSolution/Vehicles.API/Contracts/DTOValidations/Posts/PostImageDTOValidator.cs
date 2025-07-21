using FluentValidation;
using Vehicles.API.Contracts.DTOs.Posts;

namespace Vehicles.API.Contracts.DTOValidations.Posts;

public class PostImageDTOValidator : AbstractValidator<PostImageDTO>
{
    public PostImageDTOValidator()
    {
        RuleFor(x => x.Url)
            .NotEmpty().WithMessage("Image URL is required.")
            .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
            .WithMessage("Invalid image URL format.");
    }
}
