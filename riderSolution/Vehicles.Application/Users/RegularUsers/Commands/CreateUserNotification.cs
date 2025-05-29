using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Notifications.Models;

namespace Vehicles.Application.Users.RegularUsers.Commands;

public record CreateUserNotification(string Title, string Body, int UserId, int PostId) : IRequest<UserNotification>;

public class CreateUserNotificationHandler : IRequestHandler<CreateUserNotification, UserNotification>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserNotificationHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UserNotification> Handle(CreateUserNotification request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        UserNotification notification = new UserNotification()
        {
            Title = request.Title,
            Body = request.Body,
            UserId = request.UserId,
            PostId = request.PostId,
            Date = DateTime.Now
        };
        
        await _unitOfWork.UserRepository.CreateUserNotification(notification);
        await _unitOfWork.SaveAsync();
        
        return notification;
    }
}