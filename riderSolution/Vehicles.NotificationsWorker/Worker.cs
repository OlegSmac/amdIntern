using Vehicles.Application.Abstractions;
using Vehicles.NotificationsProcessing.Services;

namespace Vehicles.NotificationsProcessing;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _serviceProvider;

    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Notification worker started.");
        
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                var unsentNotifications = await unitOfWork.NotificationRepository.GetUnsentNotificationsAsync();

                foreach (var notification in unsentNotifications)
                {
                    try
                    {
                        await emailService.SendAsync(notification);
                        notification.IsSent = true;
                        _logger.LogInformation($"Notification {notification.Id} sent.");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Failed to send notification {notification.Id}.");
                    }
                }
                
                await unitOfWork.SaveAsync();
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in notification worker.");
            }
        }
    }
}