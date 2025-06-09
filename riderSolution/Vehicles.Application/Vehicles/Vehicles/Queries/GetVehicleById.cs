using MediatR;
using Vehicles.Application.Abstractions;
using DomainVehicle = Vehicles.Domain.VehicleTypes.Models.Vehicle;

namespace Vehicles.Application.Vehicles.Vehicle.Queries;

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
        
        Domain.VehicleTypes.Models.Vehicle? vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(request.Id);
        if (vehicle == null) throw new KeyNotFoundException($"Vehicle with id: {request.Id} not found");
        
        return vehicle;
    }
}