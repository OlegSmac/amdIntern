using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Vehicles.Motorcycles.Responses;
using Vehicles.Domain.VehicleTypes.Models;
using Vehicles.Domain.VehicleTypes.Models.VehicleModels;

namespace Vehicles.Application.Vehicles.Motorcycles.Commands;

public record UpdateMotorcycle(Motorcycle Motorcycle) : IRequest<MotorcycleDto>;

public class UpdateMotorcycleHandler : IRequestHandler<UpdateMotorcycle, MotorcycleDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMotorcycleHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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

    public async Task<MotorcycleDto> Handle(UpdateMotorcycle request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(request.Motorcycle.Id);
        if (vehicle is null) throw new KeyNotFoundException($"No vehicle with id {request.Motorcycle.Id} exists.");

        if (vehicle is Motorcycle motorcycle) await UpdateMotorcycleAsync(motorcycle, request);
        else throw new ArgumentException("This vehicle isn't a motorcycle");

        try
        {
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.VehicleRepository.UpdateAsync(motorcycle);
                await _unitOfWork.SaveAsync();
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        return MotorcycleDto.FromMotorcycle(motorcycle);
    }
}