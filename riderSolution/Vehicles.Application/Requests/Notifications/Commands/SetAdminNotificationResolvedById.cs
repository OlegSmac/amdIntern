using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;

namespace Vehicles.Application.Requests.Notifications.Commands;

public record SetAdminNotificationResolvedById(int NotificationId) : IRequest;

public class SetAdminNotificationResolvedByIdHandler : IRequestHandler<SetAdminNotificationResolvedById>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SetAdminNotificationResolvedByIdHandler> _logger;

    public SetAdminNotificationResolvedByIdHandler(IUnitOfWork unitOfWork, ILogger<SetAdminNotificationResolvedByIdHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(SetAdminNotificationResolvedById request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("SetAdminNotificationResolvedById was called");
        ArgumentNullException.ThrowIfNull(request);
        
        await _unitOfWork.ExecuteTransactionAsync(async () =>
        {
            await _unitOfWork.NotificationRepository.SetAdminNotificationResolvedAsync(request.NotificationId);
            await _unitOfWork.SaveAsync();
        });
    }
}