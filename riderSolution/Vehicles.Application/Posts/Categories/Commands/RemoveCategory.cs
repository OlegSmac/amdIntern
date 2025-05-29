using MediatR;
using Vehicles.Application.Abstractions;

namespace Vehicles.Application.Posts.Categories.Commands;

public record RemoveCategory(int Id) : IRequest;

public class RemoveCategoryHandler : IRequestHandler<RemoveCategory>
{
    private readonly IUnitOfWork _unitOfWork;

    public RemoveCategoryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RemoveCategory request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id);
        if (category != null)
        {
            await _unitOfWork.CategoryRepository.RemoveAsync(request.Id);
            await _unitOfWork.SaveAsync();
        }
    }
}