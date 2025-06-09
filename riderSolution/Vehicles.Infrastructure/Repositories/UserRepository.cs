using Microsoft.EntityFrameworkCore;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Notifications.Models;
using Vehicles.Domain.Posts.Models;
using Vehicles.Domain.Users.Models;
using Vehicles.Domain.Users.Relations;

namespace Vehicles.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly VehiclesDbContext _context;

    public UserRepository(VehiclesDbContext context)
    {
        _context = context;
    }

    public async Task<User> CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);
        return user;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task RemoveAsync(int id)
    {
        var toDelete = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (toDelete != null)
        {
            _context.Users.Remove(toDelete);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<User> UpdateAsync(User user)
    {
        _context.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task SubcribeAsync(Subscription subscription)
    {
        await _context.Subscriptions.AddAsync(subscription);
    }

    public async Task UnsubcribeAsync(Subscription subscription)
    {
        _context.Subscriptions.Remove(subscription);
        await _context.SaveChangesAsync();
    }

    public async Task<UserNotification> CreateUserNotification(UserNotification notification)
    {
        await _context.UserNotifications.AddAsync(notification);
        return notification;
    }

    public async Task AddPostToFavoriteListAsync(FavoritePost favoritePost)
    {
        await _context.FavoritePosts.AddAsync(favoritePost);
    }

    public async Task RemovePostFromFavoriteListAsync(FavoritePost favoritePost)
    {
        _context.FavoritePosts.Remove(favoritePost);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Post>> GetFavoritePostsAsync(int userId)
    {
        List<int> favoritePostIds = await _context.FavoritePosts.Where(fp => fp.UserId == userId)
            .Select(fp => fp.PostId)
            .ToListAsync();

        List<Post> posts = await _context.Posts.Where(p => favoritePostIds.Contains(p.Id)).
            ToListAsync();
        
        return posts;
    }

    public async Task<bool> IsPostFavoriteAsync(FavoritePost favoritePost)
    {
        return _context.FavoritePosts.Any(fp => fp.PostId == favoritePost.PostId && fp.UserId == favoritePost.UserId);
    }
}