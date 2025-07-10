using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Application.PaginationModels;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Users.Companies.Queries;

public record GetPagedCompanies(int PageIndex, int PageSize) : IRequest<PaginatedResult<Company>>;

public class GetPagedCompaniesHandler : IRequestHandler<GetPagedCompanies, PaginatedResult<Company>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetPagedCompaniesHandler> _logger;

    public GetPagedCompaniesHandler(IUnitOfWork unitOfWork, ILogger<GetPagedCompaniesHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<PaginatedResult<Company>> Handle(GetPagedCompanies request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetPagedCompanies was called");
        ArgumentNullException.ThrowIfNull(request);

        var company = await _unitOfWork.CompanyRepository.GetPagedCompanies(request.PageIndex, request.PageSize);

        return company;
    }
}