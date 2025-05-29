using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Notifications.Models;

namespace Vehicles.Application.Users.Companies.Commands;

public record CreateCompanyNotification(string Title, string Body, int CompanyId, int PostId)
    : IRequest<CompanyNotification>;

public class CreateCompanyNotificationHandler : IRequestHandler<CreateCompanyNotification, CompanyNotification>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCompanyNotificationHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CompanyNotification> Handle(CreateCompanyNotification request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        CompanyNotification notification = new CompanyNotification()
        {
            Title = request.Title,
            Body = request.Body,
            CompanyId = request.CompanyId,
            PostId = request.PostId,
            Date = DateTime.Now
        };
        
        await _unitOfWork.CompanyRepository.CreateCompanyNotification(notification);
        await _unitOfWork.SaveAsync();
        
        return notification;
    }
}