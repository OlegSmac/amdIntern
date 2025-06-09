using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Posts.Models;

namespace Vehicles.Application.Posts.Posts.Queries;

public record GetAllPosts() : IRequest<List<Post>>;

public class GetAllPostsHandler : IRequestHandler<GetAllPosts, List<Post>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAllPostsHandler> _logger;

    public GetAllPostsHandler(IUnitOfWork unitOfWork, ILogger<GetAllPostsHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<List<Post>> Handle(GetAllPosts request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetAllPosts was called");
        ArgumentNullException.ThrowIfNull(request);

        List<Post> posts = await _unitOfWork.PostRepository.GetAllAsync();

        return posts;
    }
}