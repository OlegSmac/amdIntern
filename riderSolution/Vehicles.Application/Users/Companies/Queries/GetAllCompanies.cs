using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Users.Companies.Responses;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Users.Companies.Queries;

public record GetAllCompanies() : IRequest<List<CompanyDto>>;

public class GetAllCompaniesHandler : IRequestHandler<GetAllCompanies, List<CompanyDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCompaniesHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<CompanyDto>> Handle(GetAllCompanies request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        List<Company> companies = await _unitOfWork.CompanyRepository.GetAllAsync();
        
        return companies.Select(CompanyDto.FromCompany).ToList();
    }
}