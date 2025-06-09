using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Posts.Models;

namespace Vehicles.Application.Posts.Posts.Queries;

public record GetPostById(int Id) : IRequest<Post>;

public class GetPostByIdHandler : IRequestHandler<GetPostById, Post>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetPostByIdHandler> _logger;

    public GetPostByIdHandler(IUnitOfWork unitOfWork, ILogger<GetPostByIdHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Post> Handle(GetPostById request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetPostById was called");
        ArgumentNullException.ThrowIfNull(request);
        
        Post? post = await _unitOfWork.PostRepository.GetByIdAsync(request.Id);

        return post;
    }
}