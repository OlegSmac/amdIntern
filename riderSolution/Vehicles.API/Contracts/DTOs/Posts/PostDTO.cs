using Vehicles.API.Contracts.DTOs.Users;
using Vehicles.API.Contracts.DTOs.Vehicles;

namespace Vehicles.API.Contracts.DTOs.Posts;

public class PostDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set;}
    public DateTime Date { get; set; }
    public int Views { get; set; }
    public bool IsHidden { get; set; }
    public int Price { get; set; }
    
    public List<PostImageDTO> Images { get; set; }
    
    public CompanyDTO Company { get; set; }
    public VehicleDTO Vehicle { get; set; }
    
    public List<string> Categories { get; set; }
}