using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Vehicles.Motorcycles.Responses;
using Vehicles.Domain.VehicleTypes.Models;
using Vehicles.Domain.VehicleTypes.Models.VehicleModels;

namespace Vehicles.Application.Vehicles.Motorcycles.Commands;

public record CreateMotorcycle(Motorcycle Motorcycle) : IRequest<MotorcycleDto>;

public class CreateMotorcycleHandler : IRequestHandler<CreateMotorcycle, MotorcycleDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateMotorcycleHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private async Task<Motorcycle> CreateMotorcycleAsync(CreateMotorcycle request)
    {
        Brand brand = new Brand() { Name = request.Motorcycle.Brand };
        Model model = new Model() { Name = request.Motorcycle.Model };
        Year year = new Year() { YearNum = request.Motorcycle.Year };

        if (!await _unitOfWork.ModelRepository.ExistsAsync(brand, model, year)) throw new ArgumentException("This model does not exist");
        
        return new Motorcycle()
        {
            Brand = request.Motorcycle.Brand,
            Model = request.Motorcycle.Model,
            Year = request.Motorcycle.Year,
            TransmissionType = request.Motorcycle.TransmissionType,
            EngineVolume = request.Motorcycle.EngineVolume,
            EnginePower = request.Motorcycle.EnginePower,
            FuelType = request.Motorcycle.FuelType,
            FuelConsumption = request.Motorcycle.FuelConsumption,
            Color = request.Motorcycle.Color,
            Mileage = request.Motorcycle.Mileage,
            MaxSpeed = request.Motorcycle.MaxSpeed,
            HasSidecar = request.Motorcycle.HasSidecar
        };
    }
    
    public async Task<MotorcycleDto> Handle(CreateMotorcycle request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        Motorcycle motorcycle = await CreateMotorcycleAsync(request);

        try
        {
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.VehicleRepository.CreateAsync(motorcycle);
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