using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Vehicles.Motorcycles.Responses;
using Vehicles.Domain.VehicleTypes.Models;
using Vehicles.Domain.VehicleTypes.Models.VehicleModels;

namespace Vehicles.Application.Vehicles.Motorcycles.Commands;

public record UpdateMotorcycle(int Id, string Brand, string Model, int Year, int MaxSpeed, string TransmissionType, 
    double EngineVolume, int EnginePower, string FuelType, double FuelConsumption, string Color, int Mileage,
    bool HasSidecar) : IRequest<MotorcycleDto>;

public class UpdateMotorcycleHandler : IRequestHandler<UpdateMotorcycle, MotorcycleDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMotorcycleHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private async Task UpdateMotorcycleAsync(Motorcycle motorcycle, UpdateMotorcycle request)
    {
        Brand brand = new Brand() { Name = request.Brand };
        Model model = new Model() { Name = request.Model };
        Year year = new Year() { YearNum = request.Year };

        if (await _unitOfWork.ModelRepository.ExistsAsync(brand, model, year))
        {
            motorcycle.Brand = request.Brand;
            motorcycle.Model = request.Model;
            motorcycle.Year = request.Year;
            motorcycle.MaxSpeed = request.MaxSpeed;
            motorcycle.TransmissionType = request.TransmissionType;
            motorcycle.EnginePower = request.EnginePower;
            motorcycle.EngineVolume = request.EngineVolume;
            motorcycle.FuelType = request.FuelType;
            motorcycle.FuelConsumption = request.FuelConsumption;
            motorcycle.Color = request.Color;
            motorcycle.Mileage = request.Mileage;
            motorcycle.HasSidecar = request.HasSidecar;
        }
        
        throw new ArgumentException("This model does not exist");
    }

    public async Task<MotorcycleDto> Handle(UpdateMotorcycle request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(request.Id);
        if (vehicle is null) throw new KeyNotFoundException($"No vehicle with id {request.Id} exists.");

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