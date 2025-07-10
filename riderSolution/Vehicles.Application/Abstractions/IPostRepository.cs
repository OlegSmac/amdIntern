using Vehicles.Domain.Posts.Models;

namespace Vehicles.Application.Abstractions;

public interface IPostRepository : IRepository
{
    Task<int> GetPostFavoriteCountAsync(int postId);
}