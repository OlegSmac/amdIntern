using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Vehicles.Cars.Responses;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.Application.Vehicles.Cars.Queries;

public record GetAllCars() : IRequest<List<CarDto>>;

public class GetAllCarsHandler : IRequestHandler<GetAllCars, List<CarDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCarsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<CarDto>> Handle(GetAllCars request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            List<Car> vehicles = await _unitOfWork.VehicleRepository.GetAllCarsAsync();

            return vehicles.Select(CarDto.FromCar).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}