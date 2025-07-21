using FluentValidation;
using Vehicles.API.Contracts.DTOs.Vehicles;

namespace Vehicles.API.Contracts.DTOValidations.Vehicles;

public class TruckDTOValidator : AbstractValidator<TruckDTO>
{
    public TruckDTOValidator()
    {
        Include(new VehicleDTOValidator());

        RuleFor(x => x.CabinType)
            .NotEmpty().WithMessage("CabinType is required.");

        RuleFor(x => x.LoadCapacity)
            .GreaterThan(0).WithMessage("LoadCapacity must be greater than zero.");
    }
}
