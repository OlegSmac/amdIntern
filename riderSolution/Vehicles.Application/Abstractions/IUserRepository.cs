using Vehicles.Domain.Notifications.Models;
using Vehicles.Domain.Posts.Models;
using Vehicles.Domain.Users.Models;
using Vehicles.Domain.Users.Relations;

namespace Vehicles.Application.Abstractions;

public interface IUserRepository
{
    Task<RegularUser> CreateAsync(RegularUser user);

    Task<RegularUser?> GetByIdAsync(int id);
    
    Task<List<RegularUser>> GetAllAsync();
    
    Task RemoveAsync(int id);
    
    Task<RegularUser> UpdateAsync(RegularUser user);

    Task SubcribeAsync(Subscription subscription);
    
    Task UnsubcribeAsync(Subscription subscription);
    
    Task<UserNotification> CreateUserNotification(UserNotification notification);
    
    Task AddPostToFavoriteListAsync(FavoritePost favoritePost);
    
    Task RemovePostFromFavoriteListAsync(FavoritePost favoritePost);
    
    Task<List<Post>> GetFavoritePostsAsync(int userId);
    
    Task<bool> IsPostFavoriteAsync(FavoritePost favoritePost);
}