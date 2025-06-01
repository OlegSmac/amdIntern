using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Vehicles.Cars.Responses;
using Vehicles.Domain.VehicleTypes.Models;
using Vehicles.Domain.VehicleTypes.Models.VehicleModels;

namespace Vehicles.Application.Vehicles.Cars.Commands;

public record UpdateCar(int Id, string Brand, string Model, int Year, int MaxSpeed, string TransmissionType, 
    double EngineVolume, int EnginePower, string FuelType, double FuelConsumption, string Color, int Mileage,
    string BodyType, int Seats, int Doors) : IRequest<CarDto>;

public class UpdateCarHandler : IRequestHandler<UpdateCar, CarDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCarHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private async Task UpdateCarAsync(Car car, UpdateCar request)
    {
        Brand brand = new Brand() { Name = request.Brand };
        Model model = new Model() { Name = request.Model };
        Year year = new Year() { YearNum = request.Year };

        if (await _unitOfWork.ModelRepository.ExistsAsync(brand, model, year))
        {
            car.Brand = request.Brand;
            car.Model = request.Model;
            car.Year = request.Year;
            car.MaxSpeed = request.MaxSpeed;
            car.TransmissionType = request.TransmissionType;
            car.EnginePower = request.EnginePower;
            car.EngineVolume = request.EngineVolume;
            car.FuelType = request.FuelType;
            car.FuelConsumption = request.FuelConsumption;
            car.Color = request.Color;
            car.Mileage = request.Mileage;
            car.BodyType = request.BodyType;
            car.Seats = request.Seats;
            car.Doors = request.Doors;
        }
        
        throw new ArgumentException("This model does not exist");
    }

    public async Task<CarDto> Handle(UpdateCar request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(request.Id);
        if (vehicle is null) throw new KeyNotFoundException($"No vehicle with id {request.Id} exists.");

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