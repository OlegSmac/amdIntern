using Vehicles.Domain.Posts.Models;

namespace Vehicles.Application.Abstractions;

public interface IStatisticsRepository
{
    Task<Dictionary<int, int>> GetLastYearFavoriteCountStatistics(string companyId);
    Task<Dictionary<int, int>> GetLastYearPostsCountStatistics(string companyId);
    Task<Dictionary<Post, int>> GetTop3Posts(string companyId);
    Task<int> GetTotalCompanyPostsCount(string companyId);
    Task<int> GetTotalCompanyFavoriteCount(string companyId);
}