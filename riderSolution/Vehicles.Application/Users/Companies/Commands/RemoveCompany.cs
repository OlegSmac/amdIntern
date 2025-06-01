using MediatR;
using Vehicles.Application.Abstractions;

namespace Vehicles.Application.Users.Companies.Commands;

public record RemoveCompany(int Id) : IRequest;

public class RemoveCompanyHandler : IRequestHandler<RemoveCompany>
{
    private readonly IUnitOfWork _unitOfWork;

    public RemoveCompanyHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RemoveCompany request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var company = await _unitOfWork.CompanyRepository.GetByIdAsync(request.Id);
        if (_unitOfWork == null) return;

        try
        {
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.CompanyRepository.RemoveAsync(request.Id);
                await _unitOfWork.SaveAsync();
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}