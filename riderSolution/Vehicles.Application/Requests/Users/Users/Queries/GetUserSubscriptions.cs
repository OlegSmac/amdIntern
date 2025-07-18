using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;

namespace Vehicles.Application.Requests.Users.Users.Queries;

public record GetUserSubscriptions(string UserId) : IRequest<List<string>>;

public class GetUserSubscriptionsHandler : IRequestHandler<GetUserSubscriptions, List<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetUserSubscriptionsHandler> _logger;

    public GetUserSubscriptionsHandler(IUnitOfWork unitOfWork, ILogger<GetUserSubscriptionsHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<List<string>> Handle(GetUserSubscriptions request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetUserSubscription was called");
        ArgumentNullException.ThrowIfNull(request);
        
        return await _unitOfWork.UserRepository.GetUserSubscriptions(request.UserId);
    }
}
