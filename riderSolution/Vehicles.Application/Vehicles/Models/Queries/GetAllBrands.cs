using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;

namespace Vehicles.Application.Vehicles.Models.Queries;

public record GetAllBrands() : IRequest<List<string>>;

public class GetAllBrandsHandler : IRequestHandler<GetAllBrands, List<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAllBrandsHandler> _logger;

    public GetAllBrandsHandler(IUnitOfWork unitOfWork, ILogger<GetAllBrandsHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<List<string>> Handle(GetAllBrands request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetAllBrands was called");
        ArgumentNullException.ThrowIfNull(request);

        return await _unitOfWork.ModelRepository.GetAllBrandsAsync();
    }
}