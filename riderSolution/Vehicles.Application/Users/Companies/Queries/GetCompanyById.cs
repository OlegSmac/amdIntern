using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Users.Companies.Responses;

namespace Vehicles.Application.Users.Companies.Queries;

public record GetCompanyById(int Id) : IRequest<CompanyDto>;

public class GetCompanyByIdHandler : IRequestHandler<GetCompanyById, CompanyDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCompanyByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CompanyDto> Handle(GetCompanyById request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var company = await _unitOfWork.CompanyRepository.GetByIdAsync(request.Id);
        if (company == null) throw new KeyNotFoundException($"Company with id: {request.Id} not found");
        
        return CompanyDto.FromCompany(company);
    }
}