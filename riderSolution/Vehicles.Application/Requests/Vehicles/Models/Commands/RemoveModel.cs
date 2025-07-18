using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.VehicleTypes.Models.VehicleModels;

namespace Vehicles.Application.Requests.Vehicles.Models.Commands;

public record RemoveModel(string Brand, string Model, int Year) : IRequest;

public class RemoveModelHandler : IRequestHandler<RemoveModel> 
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RemoveModelHandler> _logger;

    public RemoveModelHandler(IUnitOfWork unitOfWork, ILogger<RemoveModelHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(RemoveModel request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("RemoveModel was called");
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
    }
}