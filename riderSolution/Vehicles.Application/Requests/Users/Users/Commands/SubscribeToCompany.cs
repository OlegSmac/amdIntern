using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Users.Models;
using Vehicles.Domain.Users.Relations;

namespace Vehicles.Application.Requests.Users.Users.Commands;

public record SubscribeToCompany(string UserId, string CompanyId) : IRequest;

public class SubscribeToCompanyHandler : IRequestHandler<SubscribeToCompany>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SubscribeToCompanyHandler> _logger;

    public SubscribeToCompanyHandler(IUnitOfWork unitOfWork, ILogger<SubscribeToCompanyHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    private Subscription CreateSubscription(User user, Company company)
    {
        return new Subscription()
        {
            User = user,
            Company = company
        };
    }

    public async Task Handle(SubscribeToCompany request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("SubscribeToCompany was called");
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            User? user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
            if (user == null) throw new KeyNotFoundException($"User with ID {request.UserId} not found");
        
            Company? company = await _unitOfWork.CompanyRepository.GetByIdAsync(request.CompanyId);
            if (company == null) throw new KeyNotFoundException($"Company with ID {request.CompanyId} not found");

            Subscription subscription = CreateSubscription(user, company);
            
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.UserRepository.SubcribeAsync(subscription);
                await _unitOfWork.SaveAsync();
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }
}