using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Vehicles.Cars.Queries;
using Vehicles.Application.Vehicles.Cars.Responses;
using Vehicles.Application.Vehicles.Trucks.Responses;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.Application.Vehicles.Trucks.Queries;

public record GetAllTrucks() : IRequest<List<TruckDto>>;

public class GetAllTrucksHandler : IRequestHandler<GetAllTrucks, List<TruckDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllTrucksHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<TruckDto>> Handle(GetAllTrucks request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        List<Truck> vehicles = await _unitOfWork.VehicleRepository.GetAllTrucksAsync();
        
        return vehicles.Select(TruckDto.FromTruck).ToList();
    }
}