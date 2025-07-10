using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.VehicleTypes.Models;
using Vehicles.Domain.VehicleTypes.Models.VehicleModels;
using DomainVehicle = Vehicles.Domain.VehicleTypes.Models.Vehicle;

namespace Vehicles.Application.Vehicles.Motorcycles.Commands;

public record UpdateMotorcycle(Motorcycle Motorcycle) : IRequest<DomainVehicle>;

public class UpdateMotorcycleHandler : IRequestHandler<UpdateMotorcycle, DomainVehicle>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateMotorcycleHandler> _logger;

    public UpdateMotorcycleHandler(IUnitOfWork unitOfWork, ILogger<UpdateMotorcycleHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    private async Task UpdateMotorcycleAsync(Motorcycle motorcycle, UpdateMotorcycle request)
    {
        Brand brand = new Brand() { Name = request.Motorcycle.Brand };
        Model model = new Model() { Name = request.Motorcycle.Model };
        Year year = new Year() { YearNum = request.Motorcycle.Year };

        if (!await _unitOfWork.ModelRepository.ExistsAsync(brand, model, year)) throw new ArgumentException("This model does not exist");
        
        motorcycle.Brand = request.Motorcycle.Brand;
        motorcycle.Model = request.Motorcycle.Model;
        motorcycle.Year = request.Motorcycle.Year;
        motorcycle.MaxSpeed = request.Motorcycle.MaxSpeed;
        motorcycle.TransmissionType = request.Motorcycle.TransmissionType;
        motorcycle.EnginePower = request.Motorcycle.EnginePower;
        motorcycle.EngineVolume = request.Motorcycle.EngineVolume;
        motorcycle.FuelType = request.Motorcycle.FuelType;
        motorcycle.FuelConsumption = request.Motorcycle.FuelConsumption;
        motorcycle.Color = request.Motorcycle.Color;
        motorcycle.Mileage = request.Motorcycle.Mileage;
        motorcycle.HasSidecar = request.Motorcycle.HasSidecar;
    }

    public async Task<DomainVehicle> Handle(UpdateMotorcycle request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("UpdateMotorcycle was called");
        ArgumentNullException.ThrowIfNull(request);

        var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync<Motorcycle>(request.Motorcycle.Id);
        if (vehicle is null) throw new KeyNotFoundException($"No vehicle with id {request.Motorcycle.Id} exists.");

        if (vehicle is Motorcycle motorcycle) await UpdateMotorcycleAsync(motorcycle, request);
        else throw new ArgumentException("This vehicle isn't a motorcycle");
        
        await _unitOfWork.ExecuteTransactionAsync(async () =>
        {
            _unitOfWork.VehicleRepository.Update<Motorcycle>(motorcycle);
            await _unitOfWork.SaveAsync();
        });
        
        return motorcycle;
    }
}