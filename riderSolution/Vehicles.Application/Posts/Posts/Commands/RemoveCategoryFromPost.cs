using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;

namespace Vehicles.Application.Posts.Posts.Commands;

public record RemoveCategoryFromPost(int PostId, int CategoryId) : IRequest;

public class RemoveCategoryFromPostHandler : IRequestHandler<RemoveCategoryFromPost>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RemoveCategoryFromPostHandler> _logger;

    public RemoveCategoryFromPostHandler(IUnitOfWork unitOfWork, ILogger<RemoveCategoryFromPostHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(RemoveCategoryFromPost request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("RemoveCategoryFromPost was called");
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.PostRepository.RemoveCategoryFromPostAsync(request.PostId, request.CategoryId);
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