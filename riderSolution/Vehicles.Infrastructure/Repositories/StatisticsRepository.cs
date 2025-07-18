using Microsoft.EntityFrameworkCore;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Posts.Models;

namespace Vehicles.Infrastructure.Repositories;

public class StatisticsRepository : IStatisticsRepository
{
    private readonly VehiclesDbContext _dbContext;
    
    public StatisticsRepository(VehiclesDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Dictionary<int, int>> GetLastYearFavoriteCountStatistics(string companyId)
    {
        var now = DateTime.UtcNow;
        var oneYearAgo = now.AddMonths(-11);
        
        var counts = await _dbContext.FavoritePosts
            .Where(fp => fp.Date >= oneYearAgo && fp.Post.CompanyId == companyId)
            .GroupBy(fp => fp.Date.Month)
            .Select(mc => new
            {
                Month = mc.Key,
                Count = mc.Count()
            })
            .ToDictionaryAsync(mc => mc.Month, mc => mc.Count);

        var currentMonth = now.Month;
        var nextMonth = currentMonth % 12 + 1;
        
        var orderedResult = new Dictionary<int, int>();
        for (int i = 0; i < 12; i++)
        {
            int month = (nextMonth + i - 1) % 12 + 1;
            orderedResult[month] = counts.TryGetValue(month, out var count) ? count : 0;
        }
        
        return orderedResult;
    }

    public async Task<Dictionary<int, int>> GetLastYearPostsCountStatistics(string companyId)
    {
        var now = DateTime.UtcNow;
        var oneYearAgo = now.AddMonths(-11);

        var counts = await _dbContext.Posts
            .Where(p => p.Date >= oneYearAgo && p.CompanyId == companyId)
            .GroupBy(p => p.Date.Month)
            .Select(mc => new
            {
                Month = mc.Key,
                Count = mc.Count()
            })
            .ToDictionaryAsync(mc => mc.Month, mc => mc.Count);
        
        var currentMonth = now.Month;
        var nextMonth = currentMonth % 12 + 1;
        
        var orderedResult = new Dictionary<int, int>();
        for (int i = 0; i < 12; i++)
        {
            int month = (nextMonth + i - 1) % 12 + 1;
            orderedResult[month] = counts.TryGetValue(month, out var count) ? count : 0;
        }
        
        return orderedResult;
    }

    public async Task<Dictionary<Post, int>> GetTop3Posts(string companyId)
    {
        return await _dbContext.FavoritePosts
            .Where(fp => fp.Post.CompanyId == companyId)
            .GroupBy(fp => fp.Post)
            .OrderByDescending(group => group.Count())
            .Take(3)
            .Select(group => new
            {
                Post = group.Key,
                Count = group.Count()
            })
            .ToDictionaryAsync(p => p.Post, p => p.Count);
    }

    public async Task<int> GetTotalCompanyPostsCount(string companyId)
    {
        return await _dbContext.Posts
            .Where(p => p.CompanyId == companyId)
            .CountAsync();
    }

    public async Task<int> GetTotalCompanyFavoriteCount(string companyId)
    {
        return await _dbContext.FavoritePosts
            .Where(fp => fp.Post.CompanyId == companyId)
            .CountAsync();
    }
}