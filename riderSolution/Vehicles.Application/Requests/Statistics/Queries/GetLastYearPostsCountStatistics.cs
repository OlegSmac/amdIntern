using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;

namespace Vehicles.Application.Requests.Statistics.Queries;

public record GetLastYearPostsCountStatistics(string CompanyId) : IRequest<Dictionary<int, int>>;

public class GetLastYearPostsCountStatisticsHandler : IRequestHandler<GetLastYearPostsCountStatistics, Dictionary<int, int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetLastYearPostsCountStatisticsHandler> _logger;

    public GetLastYearPostsCountStatisticsHandler(IUnitOfWork unitOfWork, ILogger<GetLastYearPostsCountStatisticsHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Dictionary<int, int>> Handle(GetLastYearPostsCountStatistics request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetLastYearPostsCountStatistics was called");
        ArgumentNullException.ThrowIfNull(request);
        
        return await _unitOfWork.StatisticsRepository.GetLastYearPostsCountStatistics(request.CompanyId);
    }
}