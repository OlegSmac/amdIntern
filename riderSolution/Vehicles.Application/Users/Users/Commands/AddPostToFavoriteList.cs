using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Notifications.Models;
using Vehicles.Domain.Posts.Models;
using Vehicles.Domain.Users.Models;
using Vehicles.Domain.Users.Relations;

namespace Vehicles.Application.Users.Users.Commands;

public record AddPostToFavoriteList(string UserId, int PostId) : IRequest<bool>;

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
        
            Post? post = await _unitOfWork.PostRepository.GetByIdAsync<Post>(request.PostId);
            if (post == null) throw new KeyNotFoundException($"Post with ID {request.PostId} not found");
        
            FavoritePost favoritePost = await CreateFavoritePost(request);
            
            Console.WriteLine($"username: {user.ApplicationUser.UserName}");
            
            //Notification for company
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(post.CompanyId);
            var companyNotification = new CompanyNotification
            {
                Title = $"{user.ApplicationUser.UserName} added this post to favorite list!",
                Body = $"<p>User <strong>{user.FirstName} {user.LastName}</strong> added post \"{post.Title}\" to favorite list.</p> <p><a href='http://localhost:5173/post/{post.Id}'>Click here to view the post</a></p>",
                CompanyId = post.CompanyId
            };
            
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.UserRepository.AddPostToFavoriteListAsync(favoritePost);
                _unitOfWork.NotificationRepository.Add<CompanyNotification>(companyNotification);
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