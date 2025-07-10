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

    public async Task<User?> GetByIdAsync(string id)
    {
        return await _context.Users
            .Include(u => u.ApplicationUser)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task RemoveAsync(string id)
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

    public async Task<Subscription?> FindSubscriptionsAsync(string userId, string companyId)
    {
        return await _context.Subscriptions
            .FirstOrDefaultAsync(s => s.UserId == userId && s.CompanyId == companyId);
    }

    public async Task UnsubcribeAsync(Subscription subscription)
    {
        _context.Subscriptions.Remove(subscription);
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

    public async Task<bool> IsPostFavoriteAsync(string userId, int postId)
    {
        return await _context.FavoritePosts.CountAsync(fp => fp.PostId == postId && fp.UserId == userId) != 0;
    }

    public async Task<List<string>> GetUserSubscriptions(string userId)
    {
        return await _context.Subscriptions
            .Where(s => s.UserId == userId)
            .Select(s => s.CompanyId)
            .ToListAsync();
    }

}