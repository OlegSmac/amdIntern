using Microsoft.EntityFrameworkCore;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Posts.Models;

namespace Vehicles.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly VehiclesDbContext _context;

    public PostRepository(VehiclesDbContext context)
    {
        _context = context;
    }

    public async Task<Post> CreateAsync(Post post)
    {
        await _context.Posts.AddAsync(post);
        return post;
    }

    public async Task<Post?> GetByIdAsync(int id)
    {
        return await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Post>> GetAllAsync()
    {
        return await _context.Posts.ToListAsync();
    }

    public async Task RemoveAsync(int id)
    {
        var toDelete = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
        if (toDelete != null)
        {
            _context.Posts.Remove(toDelete);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Post> UpdateAsync(Post post)
    {
        _context.Posts.Update(post);
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task AddCategoryToPostAsync(int postId, int categoryId)
    {
        Post? post = await _context.Posts.Include(p => p.Categories).FirstOrDefaultAsync(p => p.Id == postId);
        
        if (post == null)
            throw new KeyNotFoundException($"Post with ID {postId} not found.");

        Category? category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == categoryId);

        if (category == null)
            throw new KeyNotFoundException($"Category with ID {categoryId} not found.");
        
        if (!post.Categories.Any(c => c.Id == categoryId))
        {
            post.Categories.Add(category);
            await _context.SaveChangesAsync();
        }
    }

    public async Task RemoveCategoryFromPostAsync(int postId, int categoryId)
    {
        Post? post = await _context.Posts.Include(p => p.Categories)
            .FirstOrDefaultAsync(p => p.Id == postId);
        
        if (post == null)
            throw new KeyNotFoundException($"Post with ID {postId} not found.");

        Category? category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);

        if (category == null)
            throw new KeyNotFoundException($"Category with ID {categoryId} not found.");
        
        if (post.Categories.Any(c => c.Id == categoryId))
        {
            post.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
    
}