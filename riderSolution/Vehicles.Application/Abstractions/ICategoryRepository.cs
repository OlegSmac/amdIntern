using Vehicles.Domain.Posts.Models;

namespace Vehicles.Application.Abstractions;

public interface ICategoryRepository
{
    Task<Category> CreateAsync(Category category);

    Task<Category?> GetByIdAsync(int id);
    
    Task<List<Category>> GetAllAsync();
    
    Task RemoveAsync(int id);
}