namespace Vehicles.Domain.Posts.Models;

public class PostImage
{
    public int Id { get; set; }
    public string Url { get; set; }

    public int PostId { get; set; }
    public Post Post { get; set; }
}
