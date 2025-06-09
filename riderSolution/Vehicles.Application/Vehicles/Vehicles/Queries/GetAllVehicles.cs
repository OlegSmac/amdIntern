using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;

using DomainVehicle = Vehicles.Domain.VehicleTypes.Models.Vehicle;

namespace Vehicles.Application.Vehicles.Vehicles.Queries;

public record GetAllVehicles() : IRequest<List<DomainVehicle>>;

public class GetAllVehiclesHandler : IRequestHandler<GetAllVehicles, List<DomainVehicle>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAllVehiclesHandler> _logger;

    public GetAllVehiclesHandler(IUnitOfWork unitOfWork, ILogger<GetAllVehiclesHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<List<DomainVehicle>> Handle(GetAllVehicles request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetAllVehicles was called");
        ArgumentNullException.ThrowIfNull(request);
        
        List<DomainVehicle> vehicles = await _unitOfWork.VehicleRepository.GetAllAsync();

        return vehicles;
    }
}