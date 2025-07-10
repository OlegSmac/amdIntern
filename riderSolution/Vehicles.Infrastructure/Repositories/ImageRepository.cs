using Microsoft.EntityFrameworkCore;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Posts.Models;

namespace Vehicles.Infrastructure.Repositories;

public class ImageRepository : IImageRepository
{
    private readonly VehiclesDbContext _context;

    public ImageRepository(VehiclesDbContext context)
    {
        _context = context;
    }

    public async Task<List<PostImage>> GetByPostIdAsync(int postId)
    {
        return await _context.PostImages
            .Where(img => img.PostId == postId)
            .ToListAsync();
    }

    public void RemoveImages(int postId)
    {
        _context.PostImages.RemoveRange(_context.PostImages.Where(img => img.PostId == postId));
    }

}