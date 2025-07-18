using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.VehicleTypes.Models;
using Vehicles.Domain.VehicleTypes.Models.VehicleModels;
using DomainVehicle = Vehicles.Domain.VehicleTypes.Models.Vehicle;

namespace Vehicles.Application.Requests.Vehicles.Trucks.Commands;

public record UpdateTruck(Truck Truck) : IRequest<DomainVehicle>;

public class UpdateTruckHandler : IRequestHandler<UpdateTruck, DomainVehicle>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateTruckHandler> _logger;

    public UpdateTruckHandler(IUnitOfWork unitOfWork, ILogger<UpdateTruckHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    private async Task UpdateTruckAsync(Truck truck, UpdateTruck request)
    {
        Brand brand = new Brand() { Name = request.Truck.Brand };
        Model model = new Model() { Name = request.Truck.Model };
        Year year = new Year() { YearNum = request.Truck.Year };

        if (!await _unitOfWork.ModelRepository.ExistsAsync(brand, model, year)) throw new ArgumentException("This model does not exist");
        
        truck.Brand = request.Truck.Brand;
        truck.Model = request.Truck.Model;
        truck.Year = request.Truck.Year;
        truck.MaxSpeed = request.Truck.MaxSpeed;
        truck.TransmissionType = request.Truck.TransmissionType;
        truck.EnginePower = request.Truck.EnginePower;
        truck.EngineVolume = request.Truck.EngineVolume;
        truck.FuelType = request.Truck.FuelType;
        truck.FuelConsumption = request.Truck.FuelConsumption;
        truck.Color = request.Truck.Color;
        truck.Mileage = request.Truck.Mileage;
        truck.CabinType = request.Truck.CabinType;
        truck.LoadCapacity = request.Truck.LoadCapacity;
    }

    public async Task<DomainVehicle> Handle(UpdateTruck request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("UpdateTruck was called");
        ArgumentNullException.ThrowIfNull(request);

        var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync<Truck>(request.Truck.Id);
        if (vehicle is null) throw new KeyNotFoundException($"No vehicle with id {request.Truck.Id} exists.");

        if (vehicle is Truck truck) await UpdateTruckAsync(truck, request);
        else throw new ArgumentException("This vehicle isn't a truck");
        
        await _unitOfWork.ExecuteTransactionAsync(async () =>
        {
            _unitOfWork.VehicleRepository.Update<Truck>(truck);
            await _unitOfWork.SaveAsync();
        });
        
        return truck;
    }
}