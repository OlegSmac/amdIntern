using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Vehicles.API;
using Vehicles.Infrastructure;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.Tests.ControllerTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private DbConnection _connection;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<VehiclesDbContext>));
            if (dbContextDescriptor != null)
            {
                services.Remove(dbContextDescriptor);
            }

            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            services.AddDbContext<VehiclesDbContext>(options =>
            {
                options.UseSqlite(_connection);
            });

            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<VehiclesDbContext>();
            db.Database.EnsureCreated();

            SeedTestData(db);
        });

        builder.UseEnvironment("Development");
    }

    private void SeedTestData(VehiclesDbContext db)
    {
        var car = new Car
        {
            Brand = "Mercedes",
            Model = "C",
            Year = 2007,
            TransmissionType = "Manual",
            FuelType = "Benzin",
            Color = "Color",
            EnginePower = 150,
            Mileage = 20000,
            MaxSpeed = 322,
            EngineVolume = 1.7,
            FuelConsumption = 12,
            BodyType = "Sedan",
            Seats = 5,
            Doors = 4
        };

        var motorcycle = new Motorcycle
        {
            Brand = "Yamaha",
            Model = "R7",
            Year = 2011,
            TransmissionType = "Manual",
            FuelType = "Benzin",
            Color = "Color",
            EnginePower = 150,
            Mileage = 20000,
            MaxSpeed = 322,
            EngineVolume = 1.7,
            FuelConsumption = 12,
            HasSidecar = false
        };

        db.Vehicles.Add(car);
        db.Vehicles.Add(motorcycle);
        db.SaveChanges();
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        _connection?.Dispose();
        _connection = null;
    }
}
