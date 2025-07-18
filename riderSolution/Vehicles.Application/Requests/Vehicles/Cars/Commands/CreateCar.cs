using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.VehicleTypes.Models;
using Vehicles.Domain.VehicleTypes.Models.VehicleModels;
using DomainVehicle = Vehicles.Domain.VehicleTypes.Models.Vehicle;

namespace Vehicles.Application.Requests.Vehicles.Cars.Commands;

public record CreateCar(Car Car) : IRequest<Car>;

public class CreateCarHandler : IRequestHandler<CreateCar, DomainVehicle>
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

        return request.Car;
    }

    public async Task<DomainVehicle> Handle(CreateCar request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CreateCar was called");
        ArgumentNullException.ThrowIfNull(request);
        
        Car car = await CreateCarAsync(request);
        await _unitOfWork.ExecuteTransactionAsync(async () =>
        {
            _unitOfWork.VehicleRepository.Add<Car>(car);
            await _unitOfWork.SaveAsync();
        });
        
        return car;
    }
}