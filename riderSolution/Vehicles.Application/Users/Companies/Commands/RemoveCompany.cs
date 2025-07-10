using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;

namespace Vehicles.Application.Users.Companies.Commands;

public record RemoveCompany(string Id) : IRequest;

public class RemoveCompanyHandler : IRequestHandler<RemoveCompany>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RemoveCompanyHandler> _logger;

    public RemoveCompanyHandler(IUnitOfWork unitOfWork, ILogger<RemoveCompanyHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(RemoveCompany request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("RemoveCompany was called");
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(request.Id);
            if (_unitOfWork == null) return;
            
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.CompanyRepository.RemoveAsync(request.Id);
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