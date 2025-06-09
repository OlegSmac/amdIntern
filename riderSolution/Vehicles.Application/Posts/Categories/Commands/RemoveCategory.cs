using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;

namespace Vehicles.Application.Posts.Categories.Commands;

public record RemoveCategory(int Id) : IRequest;

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

        try
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id);
            if (category == null) return;
            
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.CategoryRepository.RemoveAsync(request.Id);
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