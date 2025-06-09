using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Posts.Models;

namespace Vehicles.Application.Posts.Posts.Commands;

public record RemovePost(int Id) : IRequest;

public class RemovePostHandler : IRequestHandler<RemovePost>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RemovePostHandler> _logger;

    public RemovePostHandler(IUnitOfWork unitOfWork, ILogger<RemovePostHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task Handle(RemovePost request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("RemovePost was called");
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            Post? post = await _unitOfWork.PostRepository.GetByIdAsync(request.Id);
            if (post == null) return;
            
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.PostRepository.RemoveAsync(request.Id);
                await _unitOfWork.SaveAsync();
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }
}