using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Posts.Models;

namespace Vehicles.Infrastructure.Repositories;

public class PostRepository : EFCoreRepository, IPostRepository
{
    public PostRepository(VehiclesDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    { }

    public async Task<int> GetPostFavoriteCountAsync(int postId)
    {
        return await _dbContext.FavoritePosts.CountAsync(p => p.PostId == postId);
    }
    
}