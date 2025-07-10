using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.VehicleTypes.Models;
using Vehicles.Domain.VehicleTypes.Models.VehicleModels;
using DomainVehicle = Vehicles.Domain.VehicleTypes.Models.Vehicle;

namespace Vehicles.Application.Vehicles.Motorcycles.Commands;

public record CreateMotorcycle(Motorcycle Motorcycle) : IRequest<Motorcycle>;

public class CreateMotorcycleHandler : IRequestHandler<CreateMotorcycle, DomainVehicle>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateMotorcycleHandler> _logger;

    public CreateMotorcycleHandler(IUnitOfWork unitOfWork, ILogger<CreateMotorcycleHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    private async Task<Motorcycle> CreateMotorcycleAsync(CreateMotorcycle request)
    {
        Brand brand = new Brand() { Name = request.Motorcycle.Brand };
        Model model = new Model() { Name = request.Motorcycle.Model };
        Year year = new Year() { YearNum = request.Motorcycle.Year };

        if (!await _unitOfWork.ModelRepository.ExistsAsync(brand, model, year)) throw new ArgumentException("This model does not exist");
        
        return new Motorcycle()
        {
            Brand = request.Motorcycle.Brand,
            Model = request.Motorcycle.Model,
            Year = request.Motorcycle.Year,
            TransmissionType = request.Motorcycle.TransmissionType,
            EngineVolume = request.Motorcycle.EngineVolume,
            EnginePower = request.Motorcycle.EnginePower,
            FuelType = request.Motorcycle.FuelType,
            FuelConsumption = request.Motorcycle.FuelConsumption,
            Color = request.Motorcycle.Color,
            Mileage = request.Motorcycle.Mileage,
            MaxSpeed = request.Motorcycle.MaxSpeed,
            HasSidecar = request.Motorcycle.HasSidecar
        };
    }
    
    public async Task<DomainVehicle> Handle(CreateMotorcycle request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CreateMotorcycle was called");
        ArgumentNullException.ThrowIfNull(request);

        Motorcycle motorcycle = await CreateMotorcycleAsync(request);
        
        await _unitOfWork.ExecuteTransactionAsync(async () =>
        {
            _unitOfWork.VehicleRepository.Add<Motorcycle>(motorcycle);
            await _unitOfWork.SaveAsync();
        });
        
        return motorcycle;
    }
}