using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Vehicles.Cars.Commands;
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
    
    
    
    static async Task Main()
    {
        //wait CreateCar();
        //await GetCar(4);
        //await UpdateCar();
        //await RemoveCar(4);
    }
}