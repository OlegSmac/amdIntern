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

    private async Task<UserNotification> CreateUserNotificationAsync(CreateUserNotification request)
    {
        return new UserNotification()
        {
            Title = request.Title,
            Body = request.Body,
            UserId = request.UserId,
            PostId = request.PostId,
            Date = DateTime.Now
        };
    }

    public async Task<UserNotification> Handle(CreateUserNotification request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        UserNotification notification = await CreateUserNotificationAsync(request);

        try
        {
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.UserRepository.CreateUserNotification(notification);
                await _unitOfWork.SaveAsync();
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return notification;
    }
}