using System.ComponentModel.DataAnnotations;

namespace Vehicles.Domain.VehicleTypes.Models;

public class Motorcycle : Vehicle
{
    [Required]
    public bool HasSidecar { get; set; }
}