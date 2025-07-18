using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Requests.Users.Companies.Queries;

public record GetCompanyById(string Id) : IRequest<Company>;

public class GetCompanyByIdHandler : IRequestHandler<GetCompanyById, Company>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetCompanyByIdHandler> _logger;

    public GetCompanyByIdHandler(IUnitOfWork unitOfWork, ILogger<GetCompanyByIdHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Company> Handle(GetCompanyById request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetCompanyById was called");
        ArgumentNullException.ThrowIfNull(request);
        
        var company = await _unitOfWork.CompanyRepository.GetByIdAsync(request.Id);
        if (company == null) throw new KeyNotFoundException($"Company with id: {request.Id} not found");
        
        return company;
    }
}