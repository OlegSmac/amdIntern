using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Vehicles.Vehicles.Responses;
using Vehicles.Domain.VehicleTypes.Models.VehicleModels;

namespace Vehicles.Application.Vehicles.Vehicles.Commands;

public record CreateModel(string Brand, string Model, int Year) : IRequest<ModelDto>;

public class CreateModelHandler : IRequestHandler<CreateModel, ModelDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateModelHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ModelDto> Handle(CreateModel request, CancellationToken cancellationToken)
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

        return new ModelDto()
        {
            Brand = brand.Name,
            Model = model.Name,
            Year = year.YearNum
        };
    }
}
