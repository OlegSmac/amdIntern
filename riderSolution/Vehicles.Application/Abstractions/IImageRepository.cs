using Vehicles.Domain.Posts.Models;

namespace Vehicles.Application.Abstractions;

public interface IImageRepository
{
    Task<List<PostImage>> GetByPostIdAsync(int postId);

    void RemoveImages(int postId);
}