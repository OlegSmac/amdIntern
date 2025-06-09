using Vehicles.Domain.Notifications.Models;
using Vehicles.Domain.Posts.Models;
using Vehicles.Domain.Users.Models;
using Vehicles.Domain.Users.Relations;

namespace Vehicles.Application.Abstractions;

public interface IUserRepository
{
    Task<User> CreateAsync(User user);

    Task<User?> GetByIdAsync(int id);
    
    Task<List<User>> GetAllAsync();
    
    Task RemoveAsync(int id);
    
    Task<User> UpdateAsync(User user);

    Task SubcribeAsync(Subscription subscription);
    
    Task UnsubcribeAsync(Subscription subscription);
    
    Task<UserNotification> CreateUserNotification(UserNotification notification);
    
    Task AddPostToFavoriteListAsync(FavoritePost favoritePost);
    
    Task RemovePostFromFavoriteListAsync(FavoritePost favoritePost);
    
    Task<List<Post>> GetFavoritePostsAsync(int userId);
    
    Task<bool> IsPostFavoriteAsync(FavoritePost favoritePost);
}