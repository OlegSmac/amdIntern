using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.Application.Vehicles.Cars.Queries;

public record GetAllCars() : IRequest<List<Car>>;

public class GetAllCarsHandler : IRequestHandler<GetAllCars, List<Car>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAllCarsHandler> _logger;

    public GetAllCarsHandler(IUnitOfWork unitOfWork, ILogger<GetAllCarsHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<List<Car>> Handle(GetAllCars request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetAllCars was called");
        ArgumentNullException.ThrowIfNull(request);
        
        try
        {
            List<Car> vehicles = await _unitOfWork.VehicleRepository.GetAllCarsAsync();

            return vehicles;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }
}