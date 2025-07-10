using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;

namespace Vehicles.Application.Vehicles.Models.Queries;

public record GetAllYearsByBrandAndModel(string Brand, string Model) : IRequest<List<int>>;

public class GetAllYearsByBrandAndModelHandler : IRequestHandler<GetAllYearsByBrandAndModel, List<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAllYearsByBrandAndModelHandler> _logger;

    public GetAllYearsByBrandAndModelHandler(IUnitOfWork unitOfWork, ILogger<GetAllYearsByBrandAndModelHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<List<int>> Handle(GetAllYearsByBrandAndModel request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetAllModelsByBrandAndModel was called");
        ArgumentNullException.ThrowIfNull(request);
        
        return await _unitOfWork.ModelRepository.GetAllYearsByBrandAndModelAsync(request.Brand, request.Model);
    }
}