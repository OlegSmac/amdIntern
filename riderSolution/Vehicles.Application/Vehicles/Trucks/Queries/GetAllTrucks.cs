using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.Application.Vehicles.Trucks.Queries;

public record GetAllTrucks() : IRequest<List<Truck>>;

public class GetAllTrucksHandler : IRequestHandler<GetAllTrucks, List<Truck>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAllTrucksHandler> _logger;

    public GetAllTrucksHandler(IUnitOfWork unitOfWork, ILogger<GetAllTrucksHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<List<Truck>> Handle(GetAllTrucks request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GettingAllTrucks was called");
        ArgumentNullException.ThrowIfNull(request);
        
        List<Truck> vehicles = await _unitOfWork.VehicleRepository.GetAllTrucksAsync();

        return vehicles;
    }
}