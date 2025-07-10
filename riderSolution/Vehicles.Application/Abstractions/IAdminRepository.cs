using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Abstractions;

public interface IAdminRepository
{
    Task<Admin> CreateAsync(Admin admin);

    Task<Admin?> GetByIdAsync(string id);
    
    Task RemoveAsync(string id);
    
    Task<Admin> UpdateAsync(Admin admin);
}