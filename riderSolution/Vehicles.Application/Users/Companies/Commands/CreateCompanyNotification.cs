using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Notifications.Models;

namespace Vehicles.Application.Users.Companies.Commands;

public record CreateCompanyNotification(string Title, string Body, string CompanyId, int PostId)
    : IRequest<CompanyNotification>;

public class CreateCompanyNotificationHandler : IRequestHandler<CreateCompanyNotification, CompanyNotification>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateCompanyNotificationHandler> _logger;

    public CreateCompanyNotificationHandler(IUnitOfWork unitOfWork, ILogger<CreateCompanyNotificationHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    private async Task<CompanyNotification> CreateCompanyNotificationAsync(CreateCompanyNotification request)
    {
        return new CompanyNotification()
        {
            Title = request.Title,
            Body = request.Body,
            CompanyId = request.CompanyId
        };
    }

    public async Task<CompanyNotification> Handle(CreateCompanyNotification request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CreateCompanyNotification was called");
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            CompanyNotification notification = await CreateCompanyNotificationAsync(request);
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.CompanyRepository.CreateCompanyNotification(notification);
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