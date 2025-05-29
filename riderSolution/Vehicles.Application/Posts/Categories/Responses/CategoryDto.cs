using Vehicles.Domain.Posts.Models;

namespace Vehicles.Application.Posts.Categories.Responses;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }

    public static CategoryDto FromCategory(Category category)
    {
        return new CategoryDto()
        {
            Id = category.Id,
            Name = category.Name
        };
    }
}