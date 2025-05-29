using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Posts.Categories.Responses;
using Vehicles.Domain.Posts.Models;

namespace Vehicles.Application.Posts.Categories.Queries;

public record GetAllCategories() : IRequest<List<CategoryDto>>;

public class GetAllCategoriesHandler : IRequestHandler<GetAllCategories, List<CategoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCategoriesHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<CategoryDto>> Handle(GetAllCategories request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        List<Category> categories = await _unitOfWork.CategoryRepository.GetAllAsync();
        
        return categories.Select(CategoryDto.FromCategory).ToList();
    }
}