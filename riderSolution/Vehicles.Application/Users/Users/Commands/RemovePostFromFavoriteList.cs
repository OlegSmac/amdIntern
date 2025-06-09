using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Users.Relations;

namespace Vehicles.Application.Users.Users.Commands;

public record RemovePostFromFavoriteList(int UserId, int PostId) : IRequest;

public class RemovePostFromFavoriteListHandler : IRequestHandler<RemovePostFromFavoriteList>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RemovePostFromFavoriteListHandler> _logger;

    public RemovePostFromFavoriteListHandler(IUnitOfWork unitOfWork, ILogger<RemovePostFromFavoriteListHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(RemovePostFromFavoriteList request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("RemovePostFromFavoriteList was called");
        ArgumentNullException.ThrowIfNull(request);

        FavoritePost favoritePost = new FavoritePost()
        {
            UserId = request.UserId,
            PostId = request.PostId
        };

        try
        {
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                if (await _unitOfWork.UserRepository.IsPostFavoriteAsync(favoritePost))
                {
                    await _unitOfWork.UserRepository.RemovePostFromFavoriteListAsync(favoritePost);
                    await _unitOfWork.SaveAsync();
                }
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }
}