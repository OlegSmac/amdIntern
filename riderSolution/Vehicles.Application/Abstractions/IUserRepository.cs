using Vehicles.Domain.Notifications.Models;
using Vehicles.Domain.Posts.Models;
using Vehicles.Domain.Users.Models;
using Vehicles.Domain.Users.Relations;

namespace Vehicles.Application.Abstractions;

public interface IUserRepository
{
    Task<User> CreateAsync(User user);
    
    Task<User?> GetByIdAsync(string id);
    
    Task RemoveAsync(string id);
    
    Task<User> UpdateAsync(User user);
    
    Task SubcribeAsync(Subscription subscription);
    
    Task<Subscription> FindSubscriptionsAsync(string userId, string companyId);
    
    Task UnsubcribeAsync(Subscription subscription);
    
    Task<UserNotification> CreateUserNotification(UserNotification notification);
    
    Task AddPostToFavoriteListAsync(FavoritePost favoritePost);
    
    Task RemovePostFromFavoriteListAsync(FavoritePost favoritePost);
    
    Task<bool> IsPostFavoriteAsync(string userId, int postId);
    
    Task<List<string>> GetUserSubscriptions(string userId);
    
}