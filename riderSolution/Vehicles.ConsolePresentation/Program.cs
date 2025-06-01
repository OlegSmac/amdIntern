using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Vehicles.Cars.Commands;
using Vehicles.Application.Vehicles.Vehicle.Queries;
using Vehicles.Application.Vehicles.Vehicles.Commands;
using Vehicles.Application.Vehicles.Vehicles.Responses;
using Vehicles.Infrastructure;
using Vehicles.Infrastructure.Repositories;

namespace Vehicles.ConsolePresentation;

public class Program
{
    public static IMediator mediator = Init();

    static IMediator Init()
    {
        var diContainer = new ServiceCollection()
            .AddDbContext<VehiclesDbContext>(options => 
                options.UseSqlServer("Server=localhost,1433;Database=Vehicles;User Id=sa;Password=olegandilie100S&;TrustServerCertificate=true"))
            .AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(CreateCar).Assembly))
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IVehicleRepository, VehicleRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<ICompanyRepository, CompanyRepository>()
            .AddScoped<IAdminRepository, AdminRepository>()
            .AddScoped<ICategoryRepository, CategoryRepository>()
            .AddScoped<IPostRepository, PostRepository>()
            .AddScoped<IModelRepository, ModelRepository>()
            .BuildServiceProvider();
        
        return diContainer.GetRequiredService<IMediator>();
    }
    
    public async static Task CreateCar()
    {
        Console.WriteLine("Creating car ...");

        var command = new CreateCar("Volkswagen", "Golf", 1994, 180, "Manual", 
            1.8, 90, "Benzin", 9, "Silvery", 372000,
            "Universal", 5, 5);
        
        var result = await mediator.Send(command);
        Console.WriteLine($"Car created: brand: {result.Brand}, model: {result.Model}");
    }
    
    public static async Task UpdateCar()
    {
        Console.WriteLine("Updating car ...");

        var command = new UpdateCar(4, "Volkswagen", "Golf", 1995, 180, "Manual",
            1.8, 90, "Benzin", 9, "Silvery", 372000,
            "Universal", 5, 4);
        
        var result = await mediator.Send(command);
        Console.WriteLine($"Car updated: brand: {result.Brand}, year: {result.Year}, doors: {result.Doors}");
    }

    public static async Task<VehicleDto> GetCar(int id)
    {
        Console.WriteLine("Getting car ...");
        
        var command = new GetVehicleById(id);
        
        var result = await mediator.Send(command);
        Console.WriteLine($"Car found: brand: {result.Brand}, model: {result.Model}");
        
        return result;
    }

    public static async Task RemoveCar(int Id)
    {
        Console.WriteLine("Removing car ...");

        var command = new RemoveVehicle(4);
        
        await mediator.Send(command);
        Console.WriteLine($"Car with id {Id} removed");
    }
    
    static async Task Main()
    {
        //await CreateCar();
        //await GetCar(4);
        //await UpdateCar();
        //await RemoveCar(4);
    }
}