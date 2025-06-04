using Vehicles.Domain.VehicleTypes.Models.VehicleModels;

namespace Vehicles.Application.Vehicles.Vehicles.Responses;

public class ModelDto
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
}