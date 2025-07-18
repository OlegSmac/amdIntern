using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;

namespace Vehicles.Application.Requests.Statistics.Queries;

public record GetTotalCompanyPostsCount(string CompanyId) : IRequest<int>;

public class GetTotalCompanyPostsCountHandler : IRequestHandler<GetTotalCompanyPostsCount, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetTotalCompanyPostsCountHandler> _logger;

    public GetTotalCompanyPostsCountHandler(IUnitOfWork unitOfWork, ILogger<GetTotalCompanyPostsCountHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<int> Handle(GetTotalCompanyPostsCount request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetTotalCompanyPostsCount was called");
        ArgumentNullException.ThrowIfNull(request);
        
        return await _unitOfWork.StatisticsRepository.GetTotalCompanyPostsCount(request.CompanyId);
    }
}