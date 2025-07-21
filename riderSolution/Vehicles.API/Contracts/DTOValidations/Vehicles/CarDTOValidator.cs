using FluentValidation;
using Vehicles.API.Contracts.DTOs.Vehicles;

namespace Vehicles.API.Contracts.DTOValidations.Vehicles;

public class CarDTOValidator : AbstractValidator<CarDTO>
{
    public CarDTOValidator()
    {
        Include(new VehicleDTOValidator());
        
        RuleFor(x => x.BodyType)
            .NotEmpty().WithMessage("BodyType is required.");

        RuleFor(x => x.Seats)
            .GreaterThan(0).WithMessage("Seats must be greater than zero.");

        RuleFor(x => x.Doors)
            .GreaterThan(0).WithMessage("Doors must be greater than zero.");
    }
}
