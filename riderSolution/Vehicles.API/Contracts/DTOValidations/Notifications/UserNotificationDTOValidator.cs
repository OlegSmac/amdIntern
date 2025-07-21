using FluentValidation;
using Vehicles.API.Contracts.DTOs.Notifications;

namespace Vehicles.API.Contracts.DTOValidations.Notifications;

public class UserNotificationDTOValidator : AbstractValidator<UserNotificationDTO>
{
    public UserNotificationDTOValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(500);

        RuleFor(x => x.Body)
            .NotEmpty().WithMessage("Body is required.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");
    }
}
