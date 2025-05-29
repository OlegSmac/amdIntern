using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Vehicles.Motorcycles.Responses;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.Application.Vehicles.Motorcycles.Commands;

public record CreateMotorcycle(string Brand, string Model, int Year, int MaxSpeed, string TransmissionType, 
    double EngineVolume, int EnginePower, string FuelType, double FuelConsumption, string Color, int Mileage, 
    bool HasSidecar) : IRequest<MotorcycleDto>;

public class CreateMotorcycleHandler : IRequestHandler<CreateMotorcycle, MotorcycleDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateMotorcycleHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<MotorcycleDto> Handle(CreateMotorcycle request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var motorcycle = new Motorcycle()
        {
            Brand = request.Brand,
            Model = request.Model,
            Year = request.Year,
            TransmissionType = request.TransmissionType,
            EngineVolume = request.EngineVolume,
            EnginePower = request.EnginePower,
            FuelType = request.FuelType,
            FuelConsumption = request.FuelConsumption,
            Color = request.Color,
            Mileage = request.Mileage,
            MaxSpeed = request.MaxSpeed,
            HasSidecar = request.HasSidecar
        };

        await _unitOfWork.VehicleRepository.CreateAsync(motorcycle);
        await _unitOfWork.SaveAsync();
        await _unitOfWork.CommitTransactionAsync();
        
        return MotorcycleDto.FromMotorcycle(motorcycle);
    }
}