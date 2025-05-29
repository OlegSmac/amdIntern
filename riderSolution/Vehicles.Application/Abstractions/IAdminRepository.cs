using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Abstractions;

public interface IAdminRepository
{
    Task<Admin> CreateAsync(Admin admin);

    Task<Admin?> GetByIdAsync(int id);
    
    Task<List<Admin>> GetAllAsync();
    
    Task RemoveAsync(int id);
    
    Task<Admin> UpdateAsync(Admin admin);
}