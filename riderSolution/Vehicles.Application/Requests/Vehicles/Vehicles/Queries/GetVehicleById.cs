using MediatR;
using Vehicles.Application.Abstractions;
using DomainVehicle = Vehicles.Domain.VehicleTypes.Models.Vehicle;

namespace Vehicles.Application.Requests.Vehicles.Vehicle.Queries;

public record GetVehicleById(int Id) : IRequest<DomainVehicle>;

public class GetVehicleByIdHandler : IRequestHandler<GetVehicleById, DomainVehicle>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetVehicleByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<DomainVehicle> Handle(GetVehicleById request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        DomainVehicle? vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync<DomainVehicle>(request.Id);
        if (vehicle == null) throw new KeyNotFoundException($"Vehicle with id: {request.Id} not found");
        
        return vehicle;
    }
}