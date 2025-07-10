using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Notifications.Models;

namespace Vehicles.Application.Users.Users.Commands;

public record CreateUserNotification(string Title, string Body, string UserId, int PostId) : IRequest<UserNotification>;

public class CreateUserNotificationHandler : IRequestHandler<CreateUserNotification, UserNotification>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateUserNotificationHandler> _logger;

    public CreateUserNotificationHandler(IUnitOfWork unitOfWork, ILogger<CreateUserNotificationHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    private async Task<UserNotification> CreateUserNotificationAsync(CreateUserNotification request)
    {
        return new UserNotification()
        {
            Title = request.Title,
            Body = request.Body,
            UserId = request.UserId
        };
    }

    public async Task<UserNotification> Handle(CreateUserNotification request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CreateUserNotification was called");
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            UserNotification notification = await CreateUserNotificationAsync(request);
            
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.UserRepository.CreateUserNotification(notification);
                await _unitOfWork.SaveAsync();
            });
            
            return notification;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }
}