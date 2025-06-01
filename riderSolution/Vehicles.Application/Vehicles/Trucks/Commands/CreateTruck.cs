using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Vehicles.Trucks.Responses;
using Vehicles.Domain.VehicleTypes.Models;
using Vehicles.Domain.VehicleTypes.Models.VehicleModels;

namespace Vehicles.Application.Vehicles.Trucks.Commands;

public record CreateTruck(string Brand, string Model, int Year, int MaxSpeed, string TransmissionType, 
    double EngineVolume, int EnginePower, string FuelType, double FuelConsumption, string Color, int Mileage,
    string CabinType, int LoadCapacity, int TotalWeight) : IRequest<TruckDto>;

public class CreateTruckHandler : IRequestHandler<CreateTruck, TruckDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateTruckHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private async Task<Truck> CreateTruckAsync(CreateTruck request)
    {
        Brand brand = new Brand() { Name = request.Brand };
        Model model = new Model() { Name = request.Model };
        Year year = new Year() { YearNum = request.Year };

        if (await _unitOfWork.ModelRepository.ExistsAsync(brand, model, year))
        {
            return new Truck()
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
                CabinType = request.CabinType,
                LoadCapacity = request.LoadCapacity
            };
        }
        
        throw new ArgumentException("This model does not exist");
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