using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;

namespace Vehicles.Application.Requests.Notifications.Commands;

public record SetNotificationReadById(int NotificationId) : IRequest;

public class SetNotificationReadByIdHandler : IRequestHandler<SetNotificationReadById>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SetNotificationReadByIdHandler> _logger;

    public SetNotificationReadByIdHandler(IUnitOfWork unitOfWork, ILogger<SetNotificationReadByIdHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(SetNotificationReadById request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("SetNotificationReadById was called");
        ArgumentNullException.ThrowIfNull(request);
        
        await _unitOfWork.ExecuteTransactionAsync(async () =>
        {
            await _unitOfWork.NotificationRepository.SetNotificationReadAsync(request.NotificationId);
            await _unitOfWork.SaveAsync();
        });
    }
}