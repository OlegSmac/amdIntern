using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;

namespace Vehicles.Application.Posts.Posts.Queries;

public record GetPostFavoriteCount(int PostId) : IRequest<int>;

public class GetPostFavoriteCountHandler : IRequestHandler<GetPostFavoriteCount, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetPostFavoriteCountHandler> _logger;

    public GetPostFavoriteCountHandler(IUnitOfWork unitOfWork, ILogger<GetPostFavoriteCountHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<int> Handle(GetPostFavoriteCount request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetPostFavoriteCount was called");
        ArgumentNullException.ThrowIfNull(request);
        
        return await _unitOfWork.PostRepository.GetPostFavoriteCountAsync(request.PostId);
    }
}