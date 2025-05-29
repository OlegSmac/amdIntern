using Vehicles.Models.VehicleModels;

namespace Vehicles.Tests.Models.Tests.VehicleModels.Tests;

public class CarTests
{
    [Fact]
    public void Constructor_ShouldInitializePropertiesCorrectly()
    {
        var car = new Car(1, "Toyota", "Corolla", 2020, 2);
        
        Assert.Equal(2, car.Passengers);
        Assert.Equal(200, car.MaxSpeed);
        Assert.Equal("Toyota", car.Info.Brand);
    }

    [Fact]
    public void TakePassengers_ShouldIncreasePassengerCount()
    {
        var car = new Car(1, "Toyota", "Corolla", 2020);
        car.TakePassengers(2);

        Assert.Equal(2, car.Passengers);
    }

    [Fact]
    public void TakePassengers_ShouldThrowExceptionWhenCarIsMoving()
    {
        var car = new Car(1, "Toyota", "Corolla", 2020);
        car.TurnOn();
        car.Drive();
        car.ChangeSpeed(10);

        var e = Assert.Throws<Exception>(() => car.TakePassengers(1));
        Assert.Equal("Car should be stopped before taking any passengers.", e.Message);
    }

    [Fact]
    public void DisembarkPassengers_ShouldDecreasePassengerCount()
    {
        var car = new Car(1, "Toyota", "Corolla", 2020, 3);
        car.DisembarkPassengers(2);

        Assert.Equal(1, car.Passengers);
    }
    
    [Fact]
    public void TakePassengers_ShouldThrowWhenPassengerLimitExceeded()
    {
        var car = new Car(1, "Toyota", "Corolla", 2020, 3);
        
        var e = Assert.Throws<ArgumentException>(() => car.TakePassengers(2));
        Assert.Equal("Number of passengers can't be more than 4.", e.Message);
    }
    
    [Fact]
    public void DisembarkPassengers_ShouldThrowWhenTooManyPassengersRemoved()
    {
        var car = new Car(1, "Toyota", "Corolla", 2020, 1);
        
        var e = Assert.Throws<ArgumentException>(() => car.DisembarkPassengers(2));
        Assert.Equal("Car doesn't have this number of passengers.", e.Message);
    }

    [Fact]
    public void Clone_ShouldReturnCopyOfCar()
    {
        var car = new Car(1, "Tesla", "Model S", 2021, 3);
        var clone = (Car)car.Clone();

        Assert.NotSame(car, clone);
        Assert.Equal(car.Passengers, clone.Passengers);
        Assert.Equal(car.Info.Model, clone.Info.Model);
    }
}
