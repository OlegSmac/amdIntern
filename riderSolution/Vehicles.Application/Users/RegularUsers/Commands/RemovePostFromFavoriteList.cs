using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Users.Relations;

namespace Vehicles.Application.Users.RegularUsers.Commands;

public record RemovePostFromFavoriteList(int UserId, int PostId) : IRequest;

public class RemovePostFromFavoriteListHandler : IRequestHandler<RemovePostFromFavoriteList>
{
    private readonly IUnitOfWork _unitOfWork;

    public RemovePostFromFavoriteListHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RemovePostFromFavoriteList request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        FavoritePost favoritePost = new FavoritePost()
        {
            UserId = request.UserId,
            PostId = request.PostId
        };

        if (await _unitOfWork.UserRepository.IsPostFavoriteAsync(favoritePost))
        {
            await _unitOfWork.UserRepository.RemovePostFromFavoriteListAsync(favoritePost);
            await _unitOfWork.SaveAsync();
        }
    }
}