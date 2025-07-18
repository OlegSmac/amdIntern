using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Requests.Users.Companies.Commands;

public record UpdateCompany(string Id, string Name, string Description)
    : IRequest<Company>;

public class UpdateCompanyHandler : IRequestHandler<UpdateCompany, Company>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateCompanyHandler> _logger;

    public UpdateCompanyHandler(IUnitOfWork unitOfWork, ILogger<UpdateCompanyHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    private async Task UpdateCompanyAsync(Company company, UpdateCompany request)
    {
        company.Name = request.Name;
        company.Description = request.Description;
    }

    public async Task<Company> Handle(UpdateCompany request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("UpdateCompany was called");
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(request.Id);
            if (company is null) throw new KeyNotFoundException($"Company with id {request.Id} does not exist.");
        
            await UpdateCompanyAsync(company, request);
            
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.CompanyRepository.UpdateAsync(company);
                await _unitOfWork.SaveAsync();
            });
            
            return company;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }
}