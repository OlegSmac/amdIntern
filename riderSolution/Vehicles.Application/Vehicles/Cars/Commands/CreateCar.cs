using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.VehicleTypes.Models;
using Vehicles.Domain.VehicleTypes.Models.VehicleModels;

namespace Vehicles.Application.Vehicles.Cars.Commands;

public record CreateCar(Car Car) : IRequest<Car>;

public class CreateCarHandler : IRequestHandler<CreateCar, Car>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateCarHandler> _logger;

    public CreateCarHandler(IUnitOfWork unitOfWork, ILogger<CreateCarHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    private async Task<Car> CreateCarAsync(CreateCar request)
    {
        Brand brand = new Brand() { Name = request.Car.Brand };
        Model model = new Model() { Name = request.Car.Model };
        Year year = new Year() { YearNum = request.Car.Year };

        if (!await _unitOfWork.ModelRepository.ExistsAsync(brand, model, year)) throw new ArgumentException("This model does not exist");
        
        return new Car()
        {
            Brand = request.Car.Brand,
            Model = request.Car.Model,
            Year = request.Car.Year,
            TransmissionType = request.Car.TransmissionType,
            EngineVolume = request.Car.EngineVolume,
            EnginePower = request.Car.EnginePower,
            FuelType = request.Car.FuelType,
            FuelConsumption = request.Car.FuelConsumption,
            Color = request.Car.Color,
            Mileage = request.Car.Mileage,
            MaxSpeed = request.Car.MaxSpeed,
            BodyType = request.Car.BodyType,
            Seats = request.Car.Seats,
            Doors = request.Car.Doors
        };
    }

    public async Task<Car> Handle(CreateCar request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CreateCar was called");
        ArgumentNullException.ThrowIfNull(request);
        
        try
        {
            Car car = await CreateCarAsync(request);
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.VehicleRepository.CreateAsync(car);
                await _unitOfWork.SaveAsync();
            });
            
            return car;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }
}