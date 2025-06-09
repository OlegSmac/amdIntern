using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;

namespace Vehicles.Application.Vehicles.Vehicles.Commands;

public record RemoveVehicle(int Id) : IRequest;

public class RemoveVehicleHandler : IRequestHandler<RemoveVehicle>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RemoveVehicleHandler> _logger;

    public RemoveVehicleHandler(IUnitOfWork unitOfWork, ILogger<RemoveVehicleHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(RemoveVehicle request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"RemoveVehicle was called");
        ArgumentNullException.ThrowIfNull(request);
        
        var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(request.Id);
        if (vehicle == null) return;

        try
        {
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.VehicleRepository.RemoveAsync(vehicle);
                await _unitOfWork.SaveAsync();
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}