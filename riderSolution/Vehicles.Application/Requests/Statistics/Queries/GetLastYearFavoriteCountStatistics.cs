using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;

namespace Vehicles.Application.Requests.Statistics.Queries;

public record GetLastYearFavoriteCountStatistics(string CompanyId) : IRequest<Dictionary<int, int>>;

public class GetLastYearFavoriteCountStatisticsHandler : IRequestHandler<GetLastYearFavoriteCountStatistics, Dictionary<int, int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetLastYearFavoriteCountStatisticsHandler> _logger;

    public GetLastYearFavoriteCountStatisticsHandler(IUnitOfWork unitOfWork, ILogger<GetLastYearFavoriteCountStatisticsHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Dictionary<int, int>> Handle(GetLastYearFavoriteCountStatistics request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetLastYearFavoriteCountStatistics was called");
        ArgumentNullException.ThrowIfNull(request);
        
        return await _unitOfWork.StatisticsRepository.GetLastYearFavoriteCountStatistics(request.CompanyId);
    }
}