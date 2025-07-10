using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Posts.Models;

namespace Vehicles.Application.Posts.Posts.Commands;

public record HideOrUnhidePost(int Id, bool IsHidden) : IRequest;

public class HideOrUnhidePostHandler : IRequestHandler<HideOrUnhidePost>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<HideOrUnhidePostHandler> _logger;

    public HideOrUnhidePostHandler(IUnitOfWork unitOfWork, ILogger<HideOrUnhidePostHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(HideOrUnhidePost request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("HideOrUnhidePost was called");
        ArgumentNullException.ThrowIfNull(request);
        
        var post = await _unitOfWork.PostRepository.GetByIdAsync<Post>(request.Id);
        post.IsHidden = request.IsHidden;
        
        await _unitOfWork.ExecuteTransactionAsync(async () =>
        {
            _unitOfWork.PostRepository.Update(post);
            await _unitOfWork.SaveAsync();
        });
    }
}