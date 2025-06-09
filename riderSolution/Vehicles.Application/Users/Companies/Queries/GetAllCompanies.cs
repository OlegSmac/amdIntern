using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Users.Companies.Queries;

public record GetAllCompanies() : IRequest<List<Company>>;

public class GetAllCompaniesHandler : IRequestHandler<GetAllCompanies, List<Company>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAllCompaniesHandler> _logger;

    public GetAllCompaniesHandler(IUnitOfWork unitOfWork, ILogger<GetAllCompaniesHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<List<Company>> Handle(GetAllCompanies request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetAllCompanies was called");
        ArgumentNullException.ThrowIfNull(request);

        List<Company> companies = await _unitOfWork.CompanyRepository.GetAllAsync();
        
        return companies;
    }
}