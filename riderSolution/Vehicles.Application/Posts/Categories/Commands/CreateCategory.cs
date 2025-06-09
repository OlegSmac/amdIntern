using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Posts.Models;

namespace Vehicles.Application.Posts.Categories.Commands;

public record CreateCategory(string Name) : IRequest<Category>;

public class CreateCategoryHandler : IRequestHandler<CreateCategory, Category>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateCategoryHandler> _logger;

    public CreateCategoryHandler(IUnitOfWork unitOfWork, ILogger<CreateCategoryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Category> CreateCategoryAsync(CreateCategory request)
    {
        return new Category()
        {
            Name = request.Name
        };
    }

    public async Task<Category> Handle(CreateCategory request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CreateCategory was called");
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            Category category = await CreateCategoryAsync(request);
            
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.CategoryRepository.CreateAsync(category);
                await _unitOfWork.SaveAsync();
            });
            
            return category;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }
}