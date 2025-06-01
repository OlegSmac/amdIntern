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

    private async Task<CompanyNotification> CreateCompanyNotificationAsync(CreateCompanyNotification request)
    {
        return new CompanyNotification()
        {
            Title = request.Title,
            Body = request.Body,
            CompanyId = request.CompanyId,
            PostId = request.PostId,
            Date = DateTime.Now
        };
    }

    public async Task<CompanyNotification> Handle(CreateCompanyNotification request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        CompanyNotification notification = await CreateCompanyNotificationAsync(request);

        try
        {
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.CompanyRepository.CreateCompanyNotification(notification);
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