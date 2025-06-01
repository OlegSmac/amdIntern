using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Users.Companies.Responses;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Users.Companies.Commands;

public record UpdateCompany(int Id, string Name, string Email, string Password, string Description)
    : IRequest<CompanyDto>;

public class UpdateCompanyHandler : IRequestHandler<UpdateCompany, CompanyDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCompanyHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private async Task UpdateCompanyAsync(Company company, UpdateCompany request)
    {
        company.Name = request.Name;
        company.Email = request.Email;
        company.Password = request.Password;
        company.Description = request.Description;
    }

    public async Task<CompanyDto> Handle(UpdateCompany request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var company = await _unitOfWork.CompanyRepository.GetByIdAsync(request.Id);
        if (company is null) throw new KeyNotFoundException($"Company with id {request.Id} does not exist.");
        
        await UpdateCompanyAsync(company, request);

        try
        {
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.CompanyRepository.UpdateAsync(company);
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