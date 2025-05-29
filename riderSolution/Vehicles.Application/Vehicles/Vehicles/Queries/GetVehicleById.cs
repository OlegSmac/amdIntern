using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Vehicles.Vehicles.Responses;

namespace Vehicles.Application.Vehicles.Vehicle.Queries;

public record GetVehicleById(int Id) : IRequest<VehicleDto>;

public class GetVehicleByIdHandler : IRequestHandler<GetVehicleById, VehicleDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetVehicleByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<VehicleDto> Handle(GetVehicleById request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        Domain.VehicleTypes.Models.Vehicle? vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(request.Id);
        if (vehicle == null) throw new KeyNotFoundException($"Vehicle with id: {request.Id} not found");
        
        return VehicleDto.FromVehicle(vehicle);
    }
}