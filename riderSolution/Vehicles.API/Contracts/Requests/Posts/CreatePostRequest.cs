using System.ComponentModel.DataAnnotations;
using Vehicles.API.Contracts.Requests.Vehicles;

namespace Vehicles.API.Contracts.Requests.Posts;

public class CreatePostRequest
{
    [Required(ErrorMessage = "Title is required.")]
    public string Title { get; set; }
    
    [Required(ErrorMessage = "Body is required.")]
    public string Body { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
    
    [Range(0, int.MaxValue, ErrorMessage = "Price must be a positive number.")]
    public int Price { get; set; }
    
    public List<string> Images { get; set; }
    
    [Required]
    public string CompanyId { get; set; }
    
    public int VehicleId { get; set; }
    
    [Required]
    public CreateVehicleRequest Vehicle { get; set; }
    
    public List<string> Categories { get; set; }
}