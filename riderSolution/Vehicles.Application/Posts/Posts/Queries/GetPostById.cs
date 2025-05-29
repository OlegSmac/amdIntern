using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Posts.Posts.Responses;
using Vehicles.Domain.Posts.Models;

namespace Vehicles.Application.Posts.Posts.Queries;

public record GetPostById(int Id) : IRequest<PostDto>;

public class GetPostByIdHandler : IRequestHandler<GetPostById, PostDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPostByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PostDto> Handle(GetPostById request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        Post? post = await _unitOfWork.PostRepository.GetByIdAsync(request.Id);

        return PostDto.FromPost(post);
    }
}