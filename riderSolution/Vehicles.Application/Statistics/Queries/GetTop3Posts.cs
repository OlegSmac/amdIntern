using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Posts.Models;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.Application.Statistics.Queries;

public record GetTop3Posts(string CompanyId) : IRequest<Dictionary<Post, int>>;

public class GetTop3PostsHandler : IRequestHandler<GetTop3Posts, Dictionary<Post, int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetTop3PostsHandler> _logger;

    public GetTop3PostsHandler(IUnitOfWork unitOfWork, ILogger<GetTop3PostsHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Dictionary<Post, int>> Handle(GetTop3Posts request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetTop3Posts was called");
        ArgumentNullException.ThrowIfNull(request);
        
        var top3Posts = await _unitOfWork.StatisticsRepository.GetTop3Posts(request.CompanyId);
        foreach (var post in top3Posts)
        {
            var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync<Vehicle>(post.Key.VehicleId);
            post.Key.Vehicle = vehicle;

            var images = await _unitOfWork.ImageRepository.GetByPostIdAsync(post.Key.Id);
            post.Key.Images = images;
        }

        return top3Posts;
    }
}