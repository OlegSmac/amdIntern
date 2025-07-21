using FluentValidation;
using Vehicles.API.Contracts.DTOs.Vehicles;

namespace Vehicles.API.Contracts.DTOValidations.Vehicles;

public class VehicleDTOValidator : AbstractValidator<VehicleDTO>
{
    public VehicleDTOValidator()
    {
        RuleFor(x => x.Brand)
            .NotEmpty().WithMessage("Brand is required.")
            .MaximumLength(100);

        RuleFor(x => x.Model)
            .NotEmpty().WithMessage("Model is required.")
            .MaximumLength(100);

        RuleFor(x => x.Year)
            .InclusiveBetween(1886, DateTime.Now.Year)
            .WithMessage("Year must be between 1886 and the current year.");

        RuleFor(x => x.TransmissionType)
            .NotEmpty().WithMessage("TransmissionType is required.");

        RuleFor(x => x.EngineVolume)
            .GreaterThan(0).WithMessage("EngineVolume must be greater than zero.");

        RuleFor(x => x.EnginePower)
            .GreaterThan(0).WithMessage("EnginePower must be greater than zero.");

        RuleFor(x => x.FuelType)
            .NotEmpty().WithMessage("FuelType is required.");

        RuleFor(x => x.FuelConsumption)
            .GreaterThanOrEqualTo(0).WithMessage("FuelConsumption must be zero or greater.");

        RuleFor(x => x.Color)
            .NotEmpty().WithMessage("Color is required.");

        RuleFor(x => x.Mileage)
            .GreaterThanOrEqualTo(0).WithMessage("Mileage must be zero or greater.");

        RuleFor(x => x.MaxSpeed)
            .GreaterThan(0).WithMessage("MaxSpeed must be greater than zero.");

        RuleFor(x => x.VehicleType)
            .NotEmpty().WithMessage("VehicleType is required.");
    }
}
