using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Vehicles.Cars.Queries;
using Vehicles.Application.Vehicles.Cars.Responses;
using Vehicles.Application.Vehicles.Motorcycles.Responses;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.Application.Vehicles.Motorcycles.Queries;

public record GetAllMotorcycles() : IRequest<List<MotorcycleDto>>;

public class GetAllMotorcyclesHandler : IRequestHandler<GetAllMotorcycles, List<MotorcycleDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllMotorcyclesHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<MotorcycleDto>> Handle(GetAllMotorcycles request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        List<Motorcycle> vehicles = await _unitOfWork.VehicleRepository.GetAllMotorcyclesAsync();
        
        return vehicles.Select(MotorcycleDto.FromMotorcycle).ToList();
    }
}