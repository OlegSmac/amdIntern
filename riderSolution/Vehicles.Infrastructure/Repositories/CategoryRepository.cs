using Microsoft.EntityFrameworkCore;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Posts.Models;

namespace Vehicles.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly VehiclesDbContext _context;

    public CategoryRepository(VehiclesDbContext context)
    {
        _context = context;
    }

    public async Task<Category> CreateAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
        return category;
    }

    public async Task<Category?> GetByNameAsync(string name)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);
    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();        
    }

    public async Task RemoveAsync(string name)
    {
        var toDelete = await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);
        if (toDelete != null)
        {
            _context.Categories.Remove(toDelete);
            await _context.SaveChangesAsync();
        }
    }
}