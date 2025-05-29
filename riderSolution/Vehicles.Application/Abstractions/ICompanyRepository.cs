using Vehicles.Domain.Notifications.Models;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Abstractions;

public interface ICompanyRepository
{
    Task<Company> CreateAsync(Company company);

    Task<Company?> GetByIdAsync(int id);
    
    Task<List<Company>> GetAllAsync();
    
    Task RemoveAsync(int id);
    
    Task<Company> UpdateAsync(Company company);
    
    Task<CompanyNotification> CreateCompanyNotification(CompanyNotification notification);
}