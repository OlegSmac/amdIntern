using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Users.Companies.Commands;

public record CreateCompany(string Name, string Email, string Password, string Description) : IRequest<Company>;

public class CreateCompanyHandler : IRequestHandler<CreateCompany, Company>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateCompanyHandler> _logger;

    public CreateCompanyHandler(IUnitOfWork unitOfWork, ILogger<CreateCompanyHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    private async Task<Company> CreateCompanyAsync(CreateCompany request)
    {
        return new Company()
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
            Description = request.Description
        };
    } 

    public async Task<Company> Handle(CreateCompany request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CreateCompany was called");
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            Company company = await CreateCompanyAsync(request);
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.CompanyRepository.CreateAsync(company);
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