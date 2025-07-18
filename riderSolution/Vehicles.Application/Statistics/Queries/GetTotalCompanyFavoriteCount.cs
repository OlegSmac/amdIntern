using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;

namespace Vehicles.Application.Statistics.Queries;

public record GetTotalCompanyFavoriteCount(string CompanyId) : IRequest<int>;

public class GetTotalCompanyFavoriteCountHandler : IRequestHandler<GetTotalCompanyFavoriteCount, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetTotalCompanyFavoriteCountHandler> _logger;

    public GetTotalCompanyFavoriteCountHandler(IUnitOfWork unitOfWork, ILogger<GetTotalCompanyFavoriteCountHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<int> Handle(GetTotalCompanyFavoriteCount request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetTotalCompanyFavoriteCount was called");
        ArgumentNullException.ThrowIfNull(request);
        
        return await _unitOfWork.StatisticsRepository.GetTotalCompanyFavoriteCount(request.CompanyId);
    }
}