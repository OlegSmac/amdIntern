using FluentValidation;
using Vehicles.API.Contracts.DTOs.Statistics;

namespace Vehicles.API.Contracts.DTOValidations.Statistics;

public class MonthCountDTOValidator : AbstractValidator<MonthCountDTO>
{
    public MonthCountDTOValidator()
    {
        RuleFor(x => x.Month)
            .InclusiveBetween(1, 12)
            .WithMessage("Month must be between 1 and 12.");

        RuleFor(x => x.Count)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Count must be 0 or greater.");
    }
}
