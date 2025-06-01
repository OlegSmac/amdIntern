using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.VehicleTypes.Models.VehicleModels;

namespace Vehicles.Application.Vehicles.Vehicles.Commands;

public record RemoveModel(string Brand, string Model, int Year) : IRequest;

public class RemoveModelHandler : IRequestHandler<RemoveModel> 
{
    private readonly IUnitOfWork _unitOfWork;

    public RemoveModelHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RemoveModel request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var brand = new Brand() { Name = request.Brand };
        var model = new Model() { Name = request.Model };
        var year = new Year() { YearNum = request.Year };

        try
        {
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.ModelRepository.CreateAsync(brand, model, year);
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