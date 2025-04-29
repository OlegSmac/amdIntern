using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Vehicles.Models.VehicleModels;
using Vehicles.Repositories;
using Vehicles.Services;
using Xunit;

public class VehicleDealershipServiceTests
{
    private readonly Mock<IVehicleRepository<Vehicle>> _repositoryMock = new();
    private readonly VehicleDealershipService<Vehicle> _service;

    public VehicleDealershipServiceTests()
    {
        _service = new VehicleDealershipService<Vehicle>(_repositoryMock.Object);
    }

    [Fact]
    public void GetVehicleById_ReturnsExpectedVehicle()
    {
        var vehicle = new Car(1, "Toyota", "Camry", 2020);
        _repositoryMock.Setup(r => r.FindById(1)).Returns(vehicle);

        var result = _service.GetVehicleById(1);

        Assert.Equal(vehicle, result);
    }

    [Fact]
    public void GetAllVehicles_ReturnsList()
    {
        var vehicles = new List<Vehicle>
        {
            new Car(1, "Toyota", "Camry", 2020),
            new Truck(2, "Volvo", "FH", 2021, 10000),
            new Motorcycle(3, "Harley", "Davidson", 2020)
        };

        _repositoryMock.Setup(r => r.FindAll()).Returns(vehicles);

        var result = _service.GetAllVehicles();

        Assert.Equal(3, result.Count);
    }

    [Fact]
    public async Task SetVehiclesAsync_CallRepositorySuccess()
    {
        var vehicles = new List<Vehicle> { new Car(4, "Toyota", "Corolla", 2020) };
        _service.SetVehiclesAsync(vehicles);
        
        _repositoryMock.Verify(r => r.SetVehicles(vehicles), Times.Once);
    }

    [Fact]
    public async Task AddVehicleAsync_CallsRepositorySuccess()
    {
        var vehicle = new Truck(5, "Mercedes", "Actros", 2022, 15000);

        await _service.AddVehicleAsync(vehicle);

        _repositoryMock.Verify(r => r.Add(vehicle), Times.Once);
    }

    [Fact]
    public async Task ExcludeVehicleAsync_CallsRepositorySuccess()
    {
        var vehicle = new Car(6, "Honda", "Civic", 2019);

        await _service.ExcludeVehicleAsync(vehicle);

        _repositoryMock.Verify(r => r.ExcludeFromDealership(vehicle), Times.Once);
    }

    [Fact]
    public async Task UpdateVehicleAsync_CallsRepositorySuccess()
    {
        var vehicle = new Car(7, "Ford", "Fiesta", 2020);

        await _service.UpdateVehicleAsync(vehicle);

        _repositoryMock.Verify(r => r.Update(vehicle), Times.Once);
    }
}
