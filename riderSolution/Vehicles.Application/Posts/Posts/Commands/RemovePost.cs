using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Posts.Models;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.Application.Posts.Posts.Commands;

public record RemovePost(int Id) : IRequest;

public class RemovePostHandler : IRequestHandler<RemovePost>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RemovePostHandler> _logger;

    public RemovePostHandler(IUnitOfWork unitOfWork, ILogger<RemovePostHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task Handle(RemovePost request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("RemovePost was called");
        ArgumentNullException.ThrowIfNull(request);

        var post = await _unitOfWork.PostRepository.GetByIdAsync<Post>(request.Id);
        if (post == null) return;

        var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync<Vehicle>(post.VehicleId);
        if (vehicle == null) return;
        
        await _unitOfWork.ExecuteTransactionAsync(async () =>
        {
            await _unitOfWork.VehicleRepository.DeleteAsync<Vehicle>(vehicle.Id);
            await _unitOfWork.PostRepository.DeleteAsync<Post>(request.Id);
            await _unitOfWork.SaveAsync();
        });
    }
}