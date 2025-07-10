using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Notifications.Models;

namespace Vehicles.Infrastructure.Repositories;

public class NotificationRepository : EFCoreRepository, INotificationRepository
{
    public NotificationRepository(VehiclesDbContext dbContext, IMapper mapper) : base(dbContext, mapper) 
    { }

    public async Task<List<Notification>> GetUnsentNotificationsAsync()
    {
        var userNotifications = await _dbContext.UserNotifications
            .Where(n => !n.IsSent)
            .Include(n => n.User)
            .ThenInclude(u => u.ApplicationUser)
            .ToListAsync();

        var companyNotifications = await _dbContext.CompanyNotifications
            .Where(n => !n.IsSent)
            .Include(n => n.Company)
            .ThenInclude(u => u.ApplicationUser)
            .ToListAsync();

        return userNotifications.Cast<Notification>()
            .Concat(companyNotifications)
            .ToList();
    }

    public async Task SetNotificationReadAsync(int id)
    {
        var notification = await _dbContext.Notifications.FirstOrDefaultAsync(n => n.Id == id);
        if (notification == null) return;
        
        notification.IsRead = true;
    }

    public async Task SetAdminNotificationResolvedAsync(int id)
    {
        var adminNotification = await _dbContext.AdminNotifications.FirstOrDefaultAsync(n => n.Id == id);
        if (adminNotification == null) return;

        adminNotification.IsResolved = true;
    }

}