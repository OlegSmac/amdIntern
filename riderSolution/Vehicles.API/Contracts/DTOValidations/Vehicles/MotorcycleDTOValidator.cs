using FluentValidation;
using Vehicles.API.Contracts.DTOs.Vehicles;

namespace Vehicles.API.Contracts.DTOValidations.Vehicles;

public class MotorcycleDTOValidator : AbstractValidator<MotorcycleDTO>
{
    public MotorcycleDTOValidator()
    {
        Include(new VehicleDTOValidator());
    }
}
