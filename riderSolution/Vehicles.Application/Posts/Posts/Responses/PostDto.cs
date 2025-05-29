using Vehicles.Domain.Posts.Models;

namespace Vehicles.Application.Posts.Posts.Responses;

public class PostDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public DateTime Date { get; set; }
    public bool IsHidden { get; set; }
    public int Views { get; set; }
    public int CompanyId { get; set; }
    public int VehicleId { get; set; }

    public static PostDto FromPost(Post post)
    {
        return new PostDto()
        {
            Id = post.Id,
            Title = post.Title,
            Body = post.Body,
            Date = post.Date,
            IsHidden = post.IsHidden,
            Views = post.Views,
            CompanyId = post.CompanyId,
            VehicleId = post.VehicleId
        };
    }
}