using Vehicles.Models.VehicleModels;

namespace Vehicles.Tests.Models.Tests.VehicleModels.Tests;

public class MotorcycleTests
{
    [Fact]
    public void Constructor_ShouldInitializePropertiesCorrectly()
    {
        var motorcycle = new Motorcycle(1, "Yamaha", "MT-07", 2021, true);

        Assert.Equal(180, motorcycle.MaxSpeed);
        Assert.Equal("Yamaha", motorcycle.Info.Brand);
        Assert.Equal("MT-07", motorcycle.Info.Model);
    }

    [Fact]
    public void PutSidecar_ShouldAddSidecar()
    {
        var motorcycle = new Motorcycle(1, "Kawasaki", "Z650", 2022);

        motorcycle.PutSidecar();
        Assert.True(motorcycle.HasSidecar);
    }

    [Fact]
    public void PutSidecar_ShouldThrowWhenMoving()
    {
        var motorcycle = new Motorcycle(1, "Honda", "CBR500R", 2020);
        motorcycle.TurnOn();
        motorcycle.Drive();
        motorcycle.ChangeSpeed(50);

        var e = Assert.Throws<Exception>(() => motorcycle.PutSidecar());
        Assert.Equal("Bike should be stopped before putting sidecar.", e.Message);
    }

    [Fact]
    public void PutSidecar_ShouldThrowWhenAlreadyPresent()
    {
        var motorcycle = new Motorcycle(1, "Suzuki", "SV650", 2021, true);

        var e = Assert.Throws<Exception>(() => motorcycle.PutSidecar());
        Assert.Equal("Sidecar is already present.", e.Message);
    }

    [Fact]
    public void RemoveSidecar_ShouldRemoveSidecar()
    {
        var motorcycle = new Motorcycle(1, "Ducati", "Monster", 2022, true);

        motorcycle.RemoveSidecar();
        Assert.False(motorcycle.HasSidecar);
    }

    [Fact]
    public void RemoveSidecar_ShouldThrowWhenMoving()
    {
        var motorcycle = new Motorcycle(1, "BMW", "R1250", 2023, true);
        motorcycle.TurnOn();
        motorcycle.Drive();
        motorcycle.ChangeSpeed(30);

        var e = Assert.Throws<Exception>(() => motorcycle.RemoveSidecar());
        Assert.Equal("Bike should be stopped before removing sidecar.", e.Message);
    }

    [Fact]
    public void RemoveSidecar_ShouldThrowWhenNotPresent()
    {
        var motorcycle = new Motorcycle(1, "Harley", "Street 750", 2022);

        var e = Assert.Throws<Exception>(() => motorcycle.RemoveSidecar());
        Assert.Equal("Sidecar isn't present now.", e.Message);
    }

    [Fact]
    public void Clone_ShouldReturnCopyOfMotorcycle()
    {
        var motorcycle = new Motorcycle(1, "Aprilia", "Tuono", 2022, true);
        var clone = (Motorcycle)motorcycle.Clone();

        Assert.NotSame(motorcycle, clone);
        Assert.Equal(motorcycle.Info.Model, clone.Info.Model);
    }
}
