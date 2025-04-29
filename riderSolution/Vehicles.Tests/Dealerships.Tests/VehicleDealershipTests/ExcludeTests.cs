using Vehicles.Models.VehicleModels;

namespace Vehicles.Tests.Dealerships.Tests.VehicleDealershipTests;

public class ExcludeTests
{
    private readonly VehicleDealership<Vehicle> _dealership = new();
    
    [Fact]
    public void ExcludeFromDealership_ShouldRemoveVehicle()
    {
        var car = new Car(1, "Toyota", "Camry", 2020);
        _dealership.SetVehicles(new List<Vehicle> { car });

        _dealership.ExcludeFromDealership(car);

        var allVehicles = _dealership.FindAll();

        Assert.Empty(allVehicles);
    }

    [Fact]
    public void ExcludeFromDealership_ShouldThrowArgumentException_WhenVehicleIsNull()
    {
        Vehicle nullVehicle = null;

        var exception = Assert.Throws<ArgumentException>(() => _dealership.ExcludeFromDealership(nullVehicle));

        Assert.Equal("Vehicle must not be null.", exception.Message);
    }
}