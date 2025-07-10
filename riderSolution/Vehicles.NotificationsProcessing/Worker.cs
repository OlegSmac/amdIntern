using Vehicles.Application.Abstractions;
using Vehicles.NotificationsProcessing.Services;

namespace Vehicles.NotificationsProcessing;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;

    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider, IUnitOfWork unitOfWork, IEmailService emailService)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _unitOfWork = unitOfWork;
        _emailService = emailService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Notification worker started.");
        
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();

                var unsentNotifications = await _unitOfWork.NotificationRepository.GetUnsentNotificationsAsync();

                foreach (var notification in unsentNotifications)
                {
                    try
                    {
                        await _emailService.SendAsync(notification);
                        notification.IsSent = true;
                        _logger.LogInformation($"Notification {notification.Id} sent.");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Failed to send notification {notification.Id}.");
                    }
                }
                
                await _unitOfWork.SaveAsync();
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in notification worker.");
            }
        }
    }
}