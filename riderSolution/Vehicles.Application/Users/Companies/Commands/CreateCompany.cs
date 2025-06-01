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

    public async Task<CompanyDto> Handle(CreateCompany request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Company company = await CreateCompanyAsync(request);

        try
        {
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.CompanyRepository.CreateAsync(company);
                await _unitOfWork.SaveAsync();
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return CompanyDto.FromCompany(company);
    }
}