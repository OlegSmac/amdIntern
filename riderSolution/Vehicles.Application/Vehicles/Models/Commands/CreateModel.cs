using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Vehicles.Models.Responses;
using Vehicles.Domain.VehicleTypes.Models.VehicleModels;

namespace Vehicles.Application.Vehicles.Vehicles.Commands;

public record CreateModel(string Brand, string Model, int Year) : IRequest<VehicleModel>;

public class CreateModelHandler : IRequestHandler<CreateModel, VehicleModel>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateModelHandler> _logger;

    public CreateModelHandler(IUnitOfWork unitOfWork, ILogger<CreateModelHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<VehicleModel> Handle(CreateModel request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CreateModel was called");
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
            _logger.LogError(e.Message);
            throw;
        }

        return new VehicleModel()
        {
            Brand = brand.Name,
            Model = model.Name,
            Year = year.YearNum
        };
    }
}
