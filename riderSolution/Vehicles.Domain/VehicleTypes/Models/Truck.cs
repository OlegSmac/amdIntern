using System.ComponentModel.DataAnnotations;

namespace Vehicles.Domain.VehicleTypes.Models;

public class Truck : Vehicle
{
    [MaxLength(50)]
    [Required]
    public required string CabinType { get; set; }
    
    [Required]
    public required int LoadCapacity { get; set; }
    
    [Required]
    public required int TotalWeight { get; set; }
}