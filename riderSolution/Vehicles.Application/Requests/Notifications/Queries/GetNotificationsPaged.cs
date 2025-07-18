using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Application.PaginationModels;
using Vehicles.Domain.Notifications.Models;

namespace Vehicles.Application.Requests.Notifications.Queries;

public record GetNotificationsPaged<TNotification> : IRequest<PaginatedResult<TNotification>> where TNotification : Notification
{
    public PagedRequest PagedRequest { get; set; }
}

public class GetNotificationsPagedHandler<TNotification> : IRequestHandler<GetNotificationsPaged<TNotification>, PaginatedResult<TNotification>> where TNotification : Notification
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetNotificationsPagedHandler<TNotification>> _logger;

    public GetNotificationsPagedHandler(IUnitOfWork unitOfWork, ILogger<GetNotificationsPagedHandler<TNotification>> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<PaginatedResult<TNotification>> Handle(GetNotificationsPaged<TNotification> request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetNotificationsPaged was called");
        ArgumentNullException.ThrowIfNull(request);

        return await _unitOfWork.NotificationRepository.GetPagedDataAsync<TNotification>(request.PagedRequest);
    }
}
