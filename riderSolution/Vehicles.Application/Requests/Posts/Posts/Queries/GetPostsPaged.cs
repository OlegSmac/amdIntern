using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Application.PaginationModels;
using Vehicles.Domain.Posts.Models;
using Vehicles.Domain.Users.Models;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.Application.Requests.Posts.Posts.Queries;

public record GetPostsPaged() : IRequest<PaginatedResult<Post>>
{
    public PagedRequest PagedRequest { get; set; }
}

public class GetPostsPagedHandler : IRequestHandler<GetPostsPaged, PaginatedResult<Post>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetPostsPagedHandler> _logger;

    public GetPostsPagedHandler(IUnitOfWork unitOfWork, ILogger<GetPostsPagedHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<PaginatedResult<Post>> Handle(GetPostsPaged request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetPostsPaged was called");
        ArgumentNullException.ThrowIfNull(request);
        
        var posts = await _unitOfWork.PostRepository.GetPagedDataAsync<Post>(request.PagedRequest);
        foreach (var post in posts.Items)
        {
            Company? company = await _unitOfWork.CompanyRepository.GetByIdAsync(post.CompanyId);
            Vehicle? vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync<Vehicle>(post.VehicleId);
            List<PostImage> images = await _unitOfWork.ImageRepository.GetByPostIdAsync(post.Id);
            
            post.Company = company;
            post.Vehicle = vehicle;
            post.Images = images;
        }

        return posts;
    }
}