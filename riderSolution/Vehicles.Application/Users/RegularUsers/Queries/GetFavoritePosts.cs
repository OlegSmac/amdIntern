using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Posts.Posts.Responses;
using Vehicles.Domain.Posts.Models;

namespace Vehicles.Application.Users.RegularUsers.Queries;

public record GetFavoritePosts(int UserId) : IRequest<List<PostDto>>;

public class GetFavoritePostsHandler : IRequestHandler<GetFavoritePosts, List<PostDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetFavoritePostsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<PostDto>> Handle(GetFavoritePosts request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        List<Post> posts = await _unitOfWork.UserRepository.GetFavoritePostsAsync(request.UserId);
        
        return posts.Select(PostDto.FromPost).ToList();
    }
}