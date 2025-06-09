using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.Application.Vehicles.Motorcycles.Queries;

public record GetAllMotorcycles() : IRequest<List<Motorcycle>>;

public class GetAllMotorcyclesHandler : IRequestHandler<GetAllMotorcycles, List<Motorcycle>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAllMotorcyclesHandler> _logger;

    public GetAllMotorcyclesHandler(IUnitOfWork unitOfWork, ILogger<GetAllMotorcyclesHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<List<Motorcycle>> Handle(GetAllMotorcycles request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetAllMotorcycles was called");
        ArgumentNullException.ThrowIfNull(request);
        
        List<Motorcycle> vehicles = await _unitOfWork.VehicleRepository.GetAllMotorcyclesAsync();
        
        return vehicles;
    }
}