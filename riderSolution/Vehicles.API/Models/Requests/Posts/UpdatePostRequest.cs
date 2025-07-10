using System.ComponentModel.DataAnnotations;
using Vehicles.API.Models.DTOs;
using Vehicles.API.Models.Requests.Vehicles;

namespace Vehicles.API.Models.Requests.Posts;

public class UpdatePostRequest
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Title is required.")]
    public string Title { get; set; }
    
    [Required(ErrorMessage = "Body is required.")]
    public string Body { get; set;}
    
    [Required]
    public DateTime Date { get; set; }
    
    [Range(0, int.MaxValue, ErrorMessage = "Price must be a positive number.")]
    public int Price { get; set; }
    public List<string> Images { get; set; }
    public int Views { get; set; }
    public bool IsHidden { get; set; }
    
    [Required]
    public string CompanyId { get; set; }
    public int VehicleId { get; set; }
    
    [Required]
    public UpdateVehicleRequest Vehicle { get; set; }
    
    public List<string> Categories { get; set; }
}