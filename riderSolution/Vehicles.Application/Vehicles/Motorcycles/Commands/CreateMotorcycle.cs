using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Vehicles.Motorcycles.Responses;
using Vehicles.Domain.VehicleTypes.Models;
using Vehicles.Domain.VehicleTypes.Models.VehicleModels;

namespace Vehicles.Application.Vehicles.Motorcycles.Commands;

public record CreateMotorcycle(string Brand, string Model, int Year, int MaxSpeed, string TransmissionType, 
    double EngineVolume, int EnginePower, string FuelType, double FuelConsumption, string Color, int Mileage, 
    bool HasSidecar) : IRequest<MotorcycleDto>;

public class CreateMotorcycleHandler : IRequestHandler<CreateMotorcycle, MotorcycleDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateMotorcycleHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private async Task<Motorcycle> CreateMotorcycleAsync(CreateMotorcycle request)
    {
        Brand brand = new Brand() { Name = request.Brand };
        Model model = new Model() { Name = request.Model };
        Year year = new Year() { YearNum = request.Year };

        if (await _unitOfWork.ModelRepository.ExistsAsync(brand, model, year))
        {
            return new Motorcycle()
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
                HasSidecar = request.HasSidecar
            };
        }
        
        throw new ArgumentException("This model does not exist");
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