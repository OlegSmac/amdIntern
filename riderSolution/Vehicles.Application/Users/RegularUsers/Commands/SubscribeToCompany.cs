using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Users.Models;
using Vehicles.Domain.Users.Relations;

namespace Vehicles.Application.Users.RegularUsers.Commands;

public record SubscribeToCompany(int UserId, int CompanyId) : IRequest;

public class SubscribeToCompanyHandler : IRequestHandler<SubscribeToCompany>
{
    private readonly IUnitOfWork _unitOfWork;

    public SubscribeToCompanyHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private Subscription CreateSubscription(RegularUser user, Company company)
    {
        return new Subscription()
        {
            User = user,
            Company = company
        };
    }

    public async Task Handle(SubscribeToCompany request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        RegularUser? user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
        if (user == null) throw new KeyNotFoundException($"User with ID {request.UserId} not found");
        
        Company? company = await _unitOfWork.CompanyRepository.GetByIdAsync(request.CompanyId);
        if (company == null) throw new KeyNotFoundException($"Company with ID {request.CompanyId} not found");

        Subscription subscription = CreateSubscription(user, company);

        try
        {
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.UserRepository.SubcribeAsync(subscription);
                await _unitOfWork.SaveAsync();
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}