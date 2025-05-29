using Vehicles.Models.VehicleModels;

namespace Vehicles.Tests.Dealerships.Tests.VehicleDealershipTests;

public class SetTests
{
    private readonly VehicleDealership<Vehicle> _dealership = new();
    
    [Fact]
    public void SetVehicles_ShouldSetVehiclesCorrectly()
    {
        var vehicles = new List<Vehicle>
        {
            new Car(1, "Toyota", "Camry", 2020),
            new Truck(2, "Volvo", "FH", 2021, 10000)
        };

        _dealership.SetVehicles(vehicles);

        var allVehicles = _dealership.FindAll();

        Assert.Equal(2, allVehicles.Count);
        Assert.Contains(allVehicles, v => v.GetId() == 1 && v.Info.Brand == "Toyota");
        Assert.Contains(allVehicles, v => v.GetId() == 2 && v.Info.Brand == "Volvo");
    }
}