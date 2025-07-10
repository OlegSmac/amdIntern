using Vehicles.Domain.Posts.Models;

namespace Vehicles.Application.Abstractions;

public interface ICategoryRepository
{
    Task<Category> CreateAsync(Category category);

    Task<Category?> GetByNameAsync(string name);
    
    Task<List<Category>> GetAllAsync();
    
    Task RemoveAsync(string name);
}