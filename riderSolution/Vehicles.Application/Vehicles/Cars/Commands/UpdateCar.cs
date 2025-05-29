using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Vehicles.Cars.Responses;
using Vehicles.Domain.VehicleTypes.Models;

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

    public async Task<CarDto> Handle(UpdateCar request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(request.Id);
        if (vehicle is null) throw new KeyNotFoundException($"No vehicle with id {request.Id} exists.");
        
        //Is it better?
        /*car.UpdateFrom(
            request.Brand, request.Model, request.Year, request.MaxSpeed, request.TransmissionType,
            request.EngineVolume, request.EnginePower, request.FuelType, request.FuelConsumption,
            request.Color, request.Mileage, request.BodyType, request.Seats, request.Doors
        );*/
        vehicle.Brand = request.Brand;
        vehicle.Model = request.Model;
        vehicle.Year = request.Year;
        vehicle.MaxSpeed = request.MaxSpeed;
        vehicle.TransmissionType = request.TransmissionType;
        vehicle.EnginePower = request.EnginePower;
        vehicle.EngineVolume = request.EngineVolume;
        vehicle.FuelType = request.FuelType;
        vehicle.FuelConsumption = request.FuelConsumption;
        vehicle.Color = request.Color;
        vehicle.Mileage = request.Mileage;
        ((Car)vehicle).BodyType = request.BodyType;
        ((Car)vehicle).Seats = request.Seats;
        ((Car)vehicle).Doors = request.Doors;

        await _unitOfWork.VehicleRepository.UpdateAsync(vehicle);
        await _unitOfWork.SaveAsync();

        return CarDto.FromCar(((Car)vehicle));
    }
}