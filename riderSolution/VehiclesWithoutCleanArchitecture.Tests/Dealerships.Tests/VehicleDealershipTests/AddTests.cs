using Vehicles.Models.VehicleModels;

namespace Vehicles.Tests.Dealerships.Tests.VehicleDealershipTests;

public class AddTests
{
    private readonly VehicleDealership<Vehicle> _dealership = new();
    
    [Fact]
    public void Add_ValidTruck_AddsSuccessfully()
    {
        var truck = new Truck(1, "Volvo", "FH", 2020, 1000);
        _dealership.Add(truck);

        Assert.Equal(truck, _dealership.FindById(1));
    }

    [Fact]
    public void Add_ValidCar_AddsSuccessfully()
    {
        var car = new Car(2, "Toyota", "Corolla", 2019);
        _dealership.Add(car);

        Assert.Equal(car, _dealership.FindById(2));
    }

    [Fact]
    public void Add_ValidMotorcycle_AddsSuccessfully()
    {
        var moto = new Motorcycle(3, "Yamaha", "MT-07", 2021);
        _dealership.Add(moto);

        Assert.Equal(moto, _dealership.FindById(3));
    }

    [Fact]
    public void Add_CarWithPassengers_ThrowsException()
    {
        var car = new Car(4, "Ford", "Focus", 2018);
        car.TakePassengers(1);

        Assert.Throws<Exception>(() => _dealership.Add(car));
    }

    [Fact]
    public void Add_TruckWithCargo_ThrowsException()
    {
        var truck = new Truck(5, "MAN", "TGX", 2017, 1200);
        truck.UploadTruck(new Cargo("Bricks", 800));

        Assert.Throws<Exception>(() => _dealership.Add(truck));
    }

    [Fact]
    public void Add_MotorcycleTurnedOn_ThrowsException()
    {
        var moto = new Motorcycle(6, "Honda", "CBR600", 2022);
        moto.TurnOn();

        Assert.Throws<Exception>(() => _dealership.Add(moto));
    }
}