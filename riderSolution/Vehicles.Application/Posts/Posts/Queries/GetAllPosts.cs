using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Posts.Posts.Responses;
using Vehicles.Domain.Posts.Models;

namespace Vehicles.Application.Posts.Posts.Queries;

public record GetAllPosts() : IRequest<List<PostDto>>;

public class GetAllPostsHandler : IRequestHandler<GetAllPosts, List<PostDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllPostsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<PostDto>> Handle(GetAllPosts request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        List<Post> posts = await _unitOfWork.PostRepository.GetAllAsync();
        
        return posts.Select(PostDto.FromPost).ToList();
    }
}