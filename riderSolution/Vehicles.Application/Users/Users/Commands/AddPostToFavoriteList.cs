using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Posts.Models;
using Vehicles.Domain.Users.Models;
using Vehicles.Domain.Users.Relations;

namespace Vehicles.Application.Users.Users.Commands;

public record AddPostToFavoriteList(int UserId, int PostId) : IRequest<bool>;

public class AddPostToFavoriteListHandler : IRequestHandler<AddPostToFavoriteList, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddPostToFavoriteListHandler> _logger;

    public AddPostToFavoriteListHandler(IUnitOfWork unitOfWork, ILogger<AddPostToFavoriteListHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    private async Task<FavoritePost> CreateFavoritePost(AddPostToFavoriteList request)
    {
        return new FavoritePost()
        {
            UserId = request.UserId,
            PostId = request.PostId,
            Date = DateTime.Now
        };
    }

    public async Task<bool> Handle(AddPostToFavoriteList request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("AddPostToFavoriteList was called");
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            User? user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
            if (user == null) throw new KeyNotFoundException($"User with ID {request.UserId} not found");
        
            Post? post = await _unitOfWork.PostRepository.GetByIdAsync(request.PostId);
            if (post == null) throw new KeyNotFoundException($"Post with ID {request.PostId} not found");
        
            FavoritePost favoritePost = await CreateFavoritePost(request);
            
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.UserRepository.AddPostToFavoriteListAsync(favoritePost);
                await _unitOfWork.SaveAsync();
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }

        return true;
    }
}