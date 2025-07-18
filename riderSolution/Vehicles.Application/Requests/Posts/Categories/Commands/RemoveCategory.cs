using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;

namespace Vehicles.Application.Requests.Posts.Categories.Commands;

public record RemoveCategory(string Name) : IRequest;

public class RemoveCategoryHandler : IRequestHandler<RemoveCategory>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RemoveCategoryHandler> _logger;

    public RemoveCategoryHandler(IUnitOfWork unitOfWork, ILogger<RemoveCategoryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(RemoveCategory request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("RemoveCategory was called");
        ArgumentNullException.ThrowIfNull(request);
        
        var category = await _unitOfWork.CategoryRepository.GetByNameAsync(request.Name);
        if (category == null) return;
        
        await _unitOfWork.ExecuteTransactionAsync(async () =>
        {
            await _unitOfWork.CategoryRepository.RemoveAsync(request.Name);
            await _unitOfWork.SaveAsync();
        });
    }
}