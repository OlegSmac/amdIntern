using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Vehicles.Trucks.Responses;
using Vehicles.Domain.VehicleTypes.Models;
using Vehicles.Domain.VehicleTypes.Models.VehicleModels;

namespace Vehicles.Application.Vehicles.Trucks.Commands;

public record UpdateTruck(Truck Truck) : IRequest<TruckDto>;

public class UpdateTruckHandler : IRequestHandler<UpdateTruck, TruckDto>
{
    public IUnitOfWork _unitOfWork;

    public UpdateTruckHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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

    public async Task<TruckDto> Handle(UpdateTruck request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(request.Truck.Id);
        if (vehicle is null) throw new KeyNotFoundException($"No vehicle with id {request.Truck.Id} exists.");

        if (vehicle is Truck truck) await UpdateTruckAsync(truck, request);
        else throw new ArgumentException("This vehicle isn't a truck");

        try
        {
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.VehicleRepository.UpdateAsync(truck);
                await _unitOfWork.SaveAsync();
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        return TruckDto.FromTruck(truck);
    }
}