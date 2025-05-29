using System.ComponentModel.DataAnnotations;

namespace Vehicles.Domain.VehicleTypes.Models;

public class Motorcycle : Vehicle
{
    [Required]
    public required bool HasSidecar { get; set; }
}