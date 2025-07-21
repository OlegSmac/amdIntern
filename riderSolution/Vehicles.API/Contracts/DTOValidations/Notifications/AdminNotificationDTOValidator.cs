using FluentValidation;
using Vehicles.API.Contracts.DTOs.Notifications;

namespace Vehicles.API.Contracts.DTOValidations.Notifications;

public class AdminNotificationDTOValidator : AbstractValidator<AdminNotificationDTO>
{
    public AdminNotificationDTOValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(500);

        RuleFor(x => x.Body)
            .NotEmpty().WithMessage("Body is required.");

        RuleFor(x => x.AdminId)
            .NotEmpty().WithMessage("AdminId is required.");
    }
}
