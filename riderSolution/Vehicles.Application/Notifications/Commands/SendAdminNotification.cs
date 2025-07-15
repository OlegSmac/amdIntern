using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Application.PaginationModels;
using Vehicles.Application.Posts.Posts.Commands;
using Vehicles.Domain.Notifications.Models;
using Vehicles.Domain.Posts.Models;

namespace Vehicles.Application.Notifications.Commands;

public record SendAdminNotification(string Company, string Brand, string Model, int Year): IRequest;

public class SendAdminNotificationHandler : IRequestHandler<SendAdminNotification>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SendAdminNotificationHandler> _logger;
    private readonly INotificationSender _notificationSender;

    public SendAdminNotificationHandler(IUnitOfWork unitOfWork, ILogger<SendAdminNotificationHandler> logger, INotificationSender notificationSender)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _notificationSender = notificationSender;
    }

    public async Task Handle(SendAdminNotification request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("SendAdminNotificaiton was called");
        ArgumentNullException.ThrowIfNull(request);

        var adminNotification = new AdminNotification
        {
            Title = "Request to add new Model.",
            Body = $"\"{request.Company}\" sent request to add this model: {request.Brand} {request.Model} {request.Year}.",
            AdminId = "6740eff2-9536-4074-919d-185fd61506d9",
            Brand = request.Brand,
            Model = request.Model,
            Year = request.Year
        };
        
        //sending SignalR notification
        var unreadCountRequest = new PagedRequest
        {
            PageIndex = 0,
            PageSize = 99,
            ColumnNameForSorting = "",
            SortDirection = "asc",
            RequestFilters = new RequestFilters
            {
                Filters = new List<Filter>
                {
                    new Filter { Path = "adminId", Value = "6740eff2-9536-4074-919d-185fd61506d9" },
                    new Filter { Path = "isRead", Value = "false" }
                }
            }
        };

        var resultUnreadCountRequest = await _unitOfWork.NotificationRepository.GetPagedDataAsync<AdminNotification>(unreadCountRequest);
        int unreadCount = resultUnreadCountRequest.Items.Count;
        await _notificationSender.SendUnreadCountAsync("6740eff2-9536-4074-919d-185fd61506d9", unreadCount + 1);

        await _unitOfWork.ExecuteTransactionAsync(async () =>
        {
            _unitOfWork.PostRepository.Add<AdminNotification>(adminNotification);
            await _unitOfWork.SaveAsync();
        });
    }
}