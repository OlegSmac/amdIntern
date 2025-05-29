using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Posts.Categories.Responses;
using Vehicles.Domain.Posts.Models;

namespace Vehicles.Application.Posts.Categories.Queries;

public record GetCategoryById(int Id) : IRequest<CategoryDto>;

public class GetCategoryByIdHandler : IRequestHandler<GetCategoryById, CategoryDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCategoryByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CategoryDto> Handle(GetCategoryById request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        Category? category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id);
        if (category == null) throw new KeyNotFoundException($"Category with id: {request.Id} not found");
        
        return CategoryDto.FromCategory(category);
    }
}