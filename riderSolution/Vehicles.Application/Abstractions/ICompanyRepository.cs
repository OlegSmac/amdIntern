using Vehicles.Application.PaginationModels;
using Vehicles.Domain.Notifications.Models;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Abstractions;

public interface ICompanyRepository
{
    Task<Company> CreateAsync(Company company);
    Task<Company?> GetByIdAsync(string id);
    Task RemoveAsync(string id);
    Task<Company> UpdateAsync(Company company);
    Task<CompanyNotification> CreateCompanyNotification(CompanyNotification notification);
    Task<PaginatedResult<Company>> GetPagedCompanies(int pageIndex, int pageSize);
    Task<List<string>> GetCompanySubscribers(string companyId);
}