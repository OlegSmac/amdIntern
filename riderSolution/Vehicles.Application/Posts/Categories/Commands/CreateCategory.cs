using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Posts.Categories.Responses;
using Vehicles.Domain.Posts.Models;

namespace Vehicles.Application.Posts.Categories.Commands;

public record CreateCategory(string Name) : IRequest<CategoryDto>;

public class CreateCategoryHandler : IRequestHandler<CreateCategory, CategoryDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Category> CreateCategoryAsync(CreateCategory request)
    {
        return new Category()
        {
            Name = request.Name
        };
    }

    public async Task<CategoryDto> Handle(CreateCategory request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Category category = await CreateCategoryAsync(request);

        try
        {
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.CategoryRepository.CreateAsync(category);
                await _unitOfWork.SaveAsync();
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return CategoryDto.FromCategory(category);
    }
}