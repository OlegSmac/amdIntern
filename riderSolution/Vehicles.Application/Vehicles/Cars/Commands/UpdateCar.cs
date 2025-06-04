using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Vehicles.Cars.Responses;
using Vehicles.Domain.VehicleTypes.Models;
using Vehicles.Domain.VehicleTypes.Models.VehicleModels;

namespace Vehicles.Application.Vehicles.Cars.Commands;

public record UpdateCar(Car Car) : IRequest<CarDto>;

public class UpdateCarHandler : IRequestHandler<UpdateCar, CarDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCarHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private async Task UpdateCarAsync(Car car, UpdateCar request)
    {
        Brand brand = new Brand() { Name = request.Car.Brand };
        Model model = new Model() { Name = request.Car.Model };
        Year year = new Year() { YearNum = request.Car.Year };

        if (!await _unitOfWork.ModelRepository.ExistsAsync(brand, model, year)) throw new ArgumentException("This model does not exist");
        
        car.Brand = request.Car.Brand;
        car.Model = request.Car.Model;
        car.Year = request.Car.Year;
        car.MaxSpeed = request.Car.MaxSpeed;
        car.TransmissionType = request.Car.TransmissionType;
        car.EnginePower = request.Car.EnginePower;
        car.EngineVolume = request.Car.EngineVolume;
        car.FuelType = request.Car.FuelType;
        car.FuelConsumption = request.Car.FuelConsumption;
        car.Color = request.Car.Color;
        car.Mileage = request.Car.Mileage;
        car.BodyType = request.Car.BodyType;
        car.Seats = request.Car.Seats;
        car.Doors = request.Car.Doors;
    }

    public async Task<CarDto> Handle(UpdateCar request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(request.Car.Id);
        if (vehicle is null) throw new KeyNotFoundException($"No vehicle with id {request.Car.Id} exists.");

        if (vehicle is Car car) await UpdateCarAsync(car, request);
        else throw new ArgumentException("This vehicle isn't a car.");

        try
        {
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.VehicleRepository.UpdateAsync(car);
                await _unitOfWork.SaveAsync();
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
        
        return CarDto.FromCar(car);
    }
}