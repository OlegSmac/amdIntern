using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;

namespace Vehicles.Application.Vehicles.Models.Queries;

public record GetAllModelsByBrand(string Brand) : IRequest<List<string>>;

public class GetAllModelsByBrandHandler : IRequestHandler<GetAllModelsByBrand, List<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAllModelsByBrandHandler> _logger;

    public GetAllModelsByBrandHandler(IUnitOfWork unitOfWork, ILogger<GetAllModelsByBrandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<List<string>> Handle(GetAllModelsByBrand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetAllModelsByBrand was called");
        ArgumentNullException.ThrowIfNull(request);
        
        return await _unitOfWork.ModelRepository.GetAllModelsByBrandAsync(request.Brand);
    }
}