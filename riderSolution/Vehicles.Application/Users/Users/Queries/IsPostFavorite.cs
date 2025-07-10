using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;

namespace Vehicles.Application.Users.Users.Queries;

public record IsPostFavorite(string UserId, int PostId) : IRequest<bool>;

public class IsPostFavoriteHandler : IRequestHandler<IsPostFavorite, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<IsPostFavoriteHandler> _logger;

    public IsPostFavoriteHandler(IUnitOfWork unitOfWork, ILogger<IsPostFavoriteHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(IsPostFavorite request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("IsPostFavorite was called");
        ArgumentNullException.ThrowIfNull(request);

        return await _unitOfWork.UserRepository.IsPostFavoriteAsync(request.UserId, request.PostId);
    }
}
