using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Requests.Auth.Requests;
using Vehicles.Application.Requests.Auth.Responses;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Requests.Auth.Commands;

public class CompanyRegistrationHandler : IRegistrationHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CompanyRegistrationHandler> _logger;

    public string Type => "company";

    public CompanyRegistrationHandler(IUnitOfWork unitOfWork, ILogger<CompanyRegistrationHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    private async Task<Company> CreateCompanyAsync(RegisterRequest request, ApplicationUser identity)
    {
        return new Company()
        {
            Id = identity.Id,
            Name = request.Name,
            Description = request.Description
        };
    }

    public async Task<RegistrationResult> RegisterAsync(RegisterRequest request, ApplicationUser identity)
    {
        _logger.LogInformation("CreateCompany was called");
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            Company company = await CreateCompanyAsync(request, identity);
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.CompanyRepository.CreateAsync(company);
                await _unitOfWork.SaveAsync();
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }

        return new RegistrationResult
        {
            Claims = new List<Claim>
            {
                new("Name", request.Name!),
                new("Description", request.Description!)
            },
            Role = "Company"
        };
    }
}
