using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Vehicles.Cars.Responses;
using Vehicles.Application.Vehicles.Motorcycles.Responses;
using Vehicles.Application.Vehicles.Trucks.Responses;
using Vehicles.Application.Vehicles.Vehicles.Responses;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.Application.Vehicles.Vehicles.Queries;

public record GetAllVehicles() : IRequest<List<VehicleDto>>;

public class GetAllVehiclesHandler : IRequestHandler<GetAllVehicles, List<VehicleDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllVehiclesHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<VehicleDto>> Handle(GetAllVehicles request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        List<Domain.VehicleTypes.Models.Vehicle> vehicles = await _unitOfWork.VehicleRepository.GetAllAsync();
        
        return vehicles.Select(VehicleDto.FromVehicle).ToList();
    }
}