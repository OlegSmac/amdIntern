using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Vehicles.Cars.Responses;
using Vehicles.Domain.VehicleTypes.Models;
using Vehicles.Domain.VehicleTypes.Models.VehicleModels;

namespace Vehicles.Application.Vehicles.Cars.Commands;

public record CreateCar(string Brand, string Model, int Year, int MaxSpeed, string TransmissionType, 
    double EngineVolume, int EnginePower, string FuelType, double FuelConsumption, string Color, int Mileage,
    string BodyType, int Seats, int Doors) : IRequest<CarDto>;

public class CreateCarHandler : IRequestHandler<CreateCar, CarDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCarHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private async Task<Car> CreateCarAsync(CreateCar request)
    {
        Brand brand = new Brand() { Name = request.Brand };
        Model model = new Model() { Name = request.Model };
        Year year = new Year() { YearNum = request.Year };

        if (await _unitOfWork.ModelRepository.ExistsAsync(brand, model, year))
        {
            return new Car()
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
                BodyType = request.BodyType,
                Seats = request.Seats,
                Doors = request.Doors
            };
        }
        
        throw new ArgumentException("This model does not exist");
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