using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Posts.Models;

namespace Vehicles.Application.Requests.Posts.Categories.Queries;

public record GetAllCategories() : IRequest<List<Category>>;

public class GetAllCategoriesHandler : IRequestHandler<GetAllCategories, List<Category>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAllCategoriesHandler> _logger;

    public GetAllCategoriesHandler(IUnitOfWork unitOfWork, ILogger<GetAllCategoriesHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<List<Category>> Handle(GetAllCategories request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetAllCategories was called");
        ArgumentNullException.ThrowIfNull(request);

        List<Category> categories = await _unitOfWork.CategoryRepository.GetAllAsync();

        return categories;
    }
}