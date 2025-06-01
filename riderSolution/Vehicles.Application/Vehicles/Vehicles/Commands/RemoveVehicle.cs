using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Vehicles.Vehicles.Responses;

namespace Vehicles.Application.Vehicles.Vehicles.Commands;

public record RemoveVehicle(int Id) : IRequest;

public class RemoveVehicleHandler : IRequestHandler<RemoveVehicle>
{
    private readonly IUnitOfWork _unitOfWork;

    public RemoveVehicleHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RemoveVehicle request, CancellationToken cancellationToken)
    {
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