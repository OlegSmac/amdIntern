using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Posts.Models;

namespace Vehicles.Application.Posts.Categories.Queries;

public record GetCategoryById(int Id) : IRequest<Category>;

public class GetCategoryByIdHandler : IRequestHandler<GetCategoryById, Category>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetCategoryByIdHandler> _logger;

    public GetCategoryByIdHandler(IUnitOfWork unitOfWork, ILogger<GetCategoryByIdHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Category> Handle(GetCategoryById request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetCategoryById was called");
        ArgumentNullException.ThrowIfNull(request);
        
        Category? category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id);
        if (category == null) throw new KeyNotFoundException($"Category with id: {request.Id} not found");
        
        return category;
    }
}