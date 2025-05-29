using Vehicles.Domain.Posts.Models;

namespace Vehicles.Application.Abstractions;

public interface IPostRepository
{
    Task<Post> CreateAsync(Post post);

    Task<Post?> GetByIdAsync(int id);
    
    Task<List<Post>> GetAllAsync();
    
    Task RemoveAsync(int id);
    
    Task<Post> UpdateAsync(Post post);
    
    Task AddCategoryToPostAsync(int postId, int categoryId);
    Task RemoveCategoryFromPostAsync(int postId, int categoryId);
}