using Vehicles.Exceptions;
using Vehicles.Models.VehicleModels;

namespace Vehicles.Tests.Dealerships.Tests.VehicleDealershipTests;

public class UpdateTests
{
    private readonly VehicleDealership<Vehicle> _dealership = new();
    
    [Fact]
    public void Update_ShouldUpdateVehicleInfo()
    {
        int id = 1;
        string brand = "Toyota", model = "Camry";
        int previousYear = 2020, newYear = 2021;
        
        var car = new Car(id, brand, model, previousYear);
        _dealership.SetVehicles(new List<Vehicle> { car });
        var updatedCar = new Car(id, brand, model, newYear);

        _dealership.Update(updatedCar);

        var vehicleInDealership = _dealership.FindById(1);
        Assert.Equal(newYear, vehicleInDealership.Info.Year);
        Assert.Equal(brand, vehicleInDealership.Info.Brand);
    }

    [Fact]
    public void Update_ShouldThrowVehicleNotFoundException_WhenVehicleDoesNotExist()
    {
        var vehicle = new Car(100, "Honda", "Civic", 2019);

        var exception = Assert.Throws<VehicleNotFoundException>(() => _dealership.Update(vehicle));

        Assert.Equal("Vehicle with id 100 must exist in dealership before updating.", exception.Message);
    }

    [Fact]
    public void Update_ShouldThrowException_WhenVehicleSpeedIsNotZero()
    {
        var vehicle = new Car(1, "Toyota", "Camry", 2020);
        _dealership.Add(vehicle);
        vehicle.TurnOn();
        vehicle.ChangeSpeed(10);

        var exception = Assert.Throws<Exception>(() => _dealership.Update(vehicle));

        Assert.Equal("Vehicle should be stopped before adding in dealership.", exception.Message);
    }

    [Fact]
    public void Update_ShouldThrowException_WhenVehicleIsTurnedOn()
    {
        var vehicle = new Car(1, "Toyota", "Camry", 2020);
        _dealership.Add(vehicle);
        vehicle.TurnOn();

        var exception = Assert.Throws<Exception>(() => _dealership.Update(vehicle));

        Assert.Equal("Vehicle should be turned off.", exception.Message);
    }

    [Fact]
    public void Update_ShouldThrowException_WhenCarHasPassengers()
    {
        var car = new Car(1, "Toyota", "Camry", 2020);
        _dealership.Add(car);
        car.TakePassengers(1);

        var exception = Assert.Throws<Exception>(() => _dealership.Update(car));

        Assert.Equal("Car shouldn't has passengers.", exception.Message);
    }

    [Fact]
    public void Update_ShouldThrowException_WhenTruckHasCargo()
    {
        var truck = new Truck(1, "Volvo", "FH", 2021, 10000);
        _dealership.Add(truck);
        truck.UploadTruck(new Cargo("Bricks", 800));

        var exception = Assert.Throws<Exception>(() => _dealership.Update(truck));

        Assert.Equal("Truck shouldn't have cargo.", exception.Message);
    }
}