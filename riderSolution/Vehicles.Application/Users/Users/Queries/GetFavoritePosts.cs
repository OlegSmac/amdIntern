using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Posts.Models;

namespace Vehicles.Application.Users.Users.Queries;

public record GetFavoritePosts(int UserId) : IRequest<List<Post>>;

public class GetFavoritePostsHandler : IRequestHandler<GetFavoritePosts, List<Post>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetFavoritePostsHandler> _logger;

    public GetFavoritePostsHandler(IUnitOfWork unitOfWork, ILogger<GetFavoritePostsHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<List<Post>> Handle(GetFavoritePosts request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetFavoritePosts was called");
        ArgumentNullException.ThrowIfNull(request);
        
        List<Post> posts = await _unitOfWork.UserRepository.GetFavoritePostsAsync(request.UserId);
        
        return posts;
    }
}