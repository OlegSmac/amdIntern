using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Users.Models;
using Vehicles.Domain.Users.Relations;

namespace Vehicles.Application.Users.Users.Commands;

public record UnsubscribeFromCompany(string UserId, string CompanyId) : IRequest;

public class UnsubscribeFromCompanyHandler : IRequestHandler<UnsubscribeFromCompany>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UnsubscribeFromCompanyHandler> _logger;

    public UnsubscribeFromCompanyHandler(IUnitOfWork unitOfWork, ILogger<UnsubscribeFromCompanyHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(UnsubscribeFromCompany request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("UnsubscribeFromCompany was called");
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            Subscription subscription = await _unitOfWork.UserRepository.FindSubscriptionsAsync(request.UserId, request.CompanyId);
            if (subscription == null) throw new InvalidOperationException("Subscription not found");
            
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.UserRepository.UnsubcribeAsync(subscription);
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