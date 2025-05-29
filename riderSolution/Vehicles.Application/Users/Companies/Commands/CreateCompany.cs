using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Users.Companies.Responses;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Users.Companies.Commands;

public record CreateCompany(string Name, string Email, string Password, string Description) : IRequest<CompanyDto>;

public class CreateCompanyHandler : IRequestHandler<CreateCompany, CompanyDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCompanyHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CompanyDto> Handle(CreateCompany request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var company = new Company()
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
            Description = request.Description
        };

        await _unitOfWork.CompanyRepository.CreateAsync(company);
        await _unitOfWork.SaveAsync();

        return CompanyDto.FromCompany(company);
    }
}