using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Vehicles.Cars.Responses;
using Vehicles.Domain.VehicleTypes.Models;
using Vehicles.Domain.VehicleTypes.Models.VehicleModels;

namespace Vehicles.Application.Vehicles.Cars.Commands;

public record CreateCar(Car Car) : IRequest<CarDto>;

public class CreateCarHandler : IRequestHandler<CreateCar, CarDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCarHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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

    public async Task<CarDto> Handle(CreateCar request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        Car car = await CreateCarAsync(request);

        try
        {
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.VehicleRepository.CreateAsync(car);
                await _unitOfWork.SaveAsync();
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        return CarDto.FromCar(car);
    }
}