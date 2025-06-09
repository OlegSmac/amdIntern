using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.VehicleTypes.Models;
using Vehicles.Domain.VehicleTypes.Models.VehicleModels;

namespace Vehicles.Application.Vehicles.Trucks.Commands;

public record CreateTruck(Truck Truck) : IRequest<Truck>;

public class CreateTruckHandler : IRequestHandler<CreateTruck, Truck>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateTruckHandler> _logger;

    public CreateTruckHandler(IUnitOfWork unitOfWork, ILogger<CreateTruckHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    private async Task<Truck> CreateTruckAsync(CreateTruck request)
    {
        Brand brand = new Brand() { Name = request.Truck.Brand };
        Model model = new Model() { Name = request.Truck.Model };
        Year year = new Year() { YearNum = request.Truck.Year };

        if (!await _unitOfWork.ModelRepository.ExistsAsync(brand, model, year)) throw new ArgumentException("This model does not exist");
        
        return new Truck()
        {
            Brand = request.Truck.Brand,
            Model = request.Truck.Model,
            Year = request.Truck.Year,
            TransmissionType = request.Truck.TransmissionType,
            EngineVolume = request.Truck.EngineVolume,
            EnginePower = request.Truck.EnginePower,
            FuelType = request.Truck.FuelType,
            FuelConsumption = request.Truck.FuelConsumption,
            Color = request.Truck.Color,
            Mileage = request.Truck.Mileage,
            MaxSpeed = request.Truck.MaxSpeed,
            CabinType = request.Truck.CabinType,
            LoadCapacity = request.Truck.LoadCapacity
        };
    }

    public async Task<Truck> Handle(CreateTruck request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CreateTruck was called");
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            Truck truck = await CreateTruckAsync(request);
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.VehicleRepository.CreateAsync(truck);
                await _unitOfWork.SaveAsync();
            });
            
            return truck;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }
}