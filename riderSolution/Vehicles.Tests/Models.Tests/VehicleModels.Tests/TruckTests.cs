using Vehicles.Models.VehicleModels;

namespace Vehicles.Tests.Models.Tests.VehicleModels.Tests;

public class TruckTests
{
    [Fact]
    public void Constructor_ShouldInitializePropertiesCorrectly()
    {
        var truck = new Truck(1, "Volvo", "FH16", 2019, 10000);

        Assert.Equal(150, truck.MaxSpeed);
        Assert.Equal(10000, truck.MaxLoadKg);
        Assert.Equal("Volvo", truck.Info.Brand);
    }

    [Fact]
    public void GetMaterial_ShouldReturnEmptyWhenNoCargo()
    {
        var truck = new Truck(1, "Scania", "R500", 2020, 8000);

        Assert.Equal("Empty", truck.GetMaterial());
    }

    [Fact]
    public void GetLoadKg_ShouldReturnZeroWhenNoCargo()
    {
        var truck = new Truck(1, "MAN", "TGX", 2021, 9000);

        Assert.Equal(0, truck.GetLoadKg());
    }

    [Fact]
    public void UploadTruck_ShouldLoadCargoSuccessfully()
    {
        var truck = new Truck(1, "DAF", "XF", 2020, 5000);
        var cargo = new Cargo("Gravel", 3000);

        truck.UploadTruck(cargo);

        Assert.True(truck.HasCargo());
        Assert.Equal("Gravel", truck.GetMaterial());
        Assert.Equal(3000, truck.GetLoadKg());
    }

    [Fact]
    public void UploadTruck_ShouldThrowWhenMoving()
    {
        var truck = new Truck(1, "Iveco", "Stralis", 2020, 7000);
        var cargo = new Cargo("Sand", 4000);
        truck.TurnOn();
        truck.Drive();
        truck.ChangeSpeed(30);

        var e = Assert.Throws<Exception>(() => truck.UploadTruck(cargo));
        Assert.Equal("Truck should be stopped before uploading.", e.Message);
    }

    [Fact]
    public void UploadTruck_ShouldThrowWhenAlreadyUploaded()
    {
        var truck = new Truck(1, "Mercedes", "Actros", 2020, 6000);
        var cargo1 = new Cargo("Bricks", 3000);
        var cargo2 = new Cargo("Steel", 2000);

        truck.UploadTruck(cargo1);
        var e = Assert.Throws<Exception>(() => truck.UploadTruck(cargo2));
        Assert.Equal("Truck is already uploaded.", e.Message);
    }

    [Fact]
    public void UploadTruck_ShouldThrowWhenCargoTooHeavy()
    {
        var truck = new Truck(1, "Renault", "T High", 2020, 4000);
        var cargo = new Cargo("Concrete", 5000);

        var e = Assert.Throws<Exception>(() => truck.UploadTruck(cargo));
        Assert.Equal("Weight cannot be greater than max load kg.", e.Message);
    }

    [Fact]
    public void UnloadTruck_ShouldClearCargo()
    {
        var truck = new Truck(1, "Ford", "F-Max", 2020, 6000);
        var cargo = new Cargo("Logs", 3000);

        truck.UploadTruck(cargo);
        truck.UnloadTruck();

        Assert.False(truck.HasCargo());
        Assert.Equal(0, truck.GetLoadKg());
    }

    [Fact]
    public void UnloadTruck_ShouldThrowWhenMoving()
    {
        var truck = new Truck(1, "Kamaz", "5490", 2020, 6000);
        var cargo = new Cargo("Iron", 2000);

        truck.UploadTruck(cargo);
        truck.TurnOn();
        truck.Drive();
        truck.ChangeSpeed(20);

        var e = Assert.Throws<Exception>(() => truck.UnloadTruck());
        Assert.Equal("Truck should be stopped before unloading.", e.Message);
    }

    [Fact]
    public void UnloadTruck_ShouldThrowWhenAlreadyUnloaded()
    {
        var truck = new Truck(1, "Tatra", "Phoenix", 2020, 6000);

        var e = Assert.Throws<Exception>(() => truck.UnloadTruck());
        Assert.Equal("Truck is already unloaded.", e.Message);
    }

    [Fact]
    public void Clone_ShouldReturnCopyOfTruck()
    {
        var truck = new Truck(1, "Kenworth", "T680", 2020, 10000);
        var cargo = new Cargo("Coal", 5000);
        truck.UploadTruck(cargo);

        var clone = (Truck)truck.Clone();

        Assert.NotSame(truck, clone);
        Assert.True(clone.HasCargo());
        Assert.Equal("Coal", clone.GetMaterial());
        Assert.Equal(5000, clone.GetLoadKg());
    }
}
