using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Posts.Models;

namespace Vehicles.Application.Requests.Posts.Categories.Queries;

public record GetCategoryByName(string Name) : IRequest<Category>;

public class GetCategoryByNameHandler : IRequestHandler<GetCategoryByName, Category>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetCategoryByNameHandler> _logger;

    public GetCategoryByNameHandler(IUnitOfWork unitOfWork, ILogger<GetCategoryByNameHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Category> Handle(GetCategoryByName request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetCategoryByName was called");
        ArgumentNullException.ThrowIfNull(request);
        
        Category? category = await _unitOfWork.CategoryRepository.GetByNameAsync(request.Name);
        if (category == null) throw new KeyNotFoundException($"Category with name: {request.Name} not found");
        
        return category;
    }
}