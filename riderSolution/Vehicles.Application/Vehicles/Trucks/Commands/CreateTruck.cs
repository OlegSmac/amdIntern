using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Vehicles.Trucks.Responses;
using Vehicles.Domain.VehicleTypes.Models;
using Vehicles.Domain.VehicleTypes.Models.VehicleModels;

namespace Vehicles.Application.Vehicles.Trucks.Commands;

public record CreateTruck(Truck Truck) : IRequest<TruckDto>;

public class CreateTruckHandler : IRequestHandler<CreateTruck, TruckDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateTruckHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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

    public async Task<TruckDto> Handle(CreateTruck request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        Truck truck = await CreateTruckAsync(request);

        try
        {
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.VehicleRepository.CreateAsync(truck);
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