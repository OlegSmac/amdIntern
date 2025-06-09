using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Vehicles.API.Middlewares;
using Vehicles.Application;
using Vehicles.Infrastructure;
using Vehicles.Infrastructure.Repositories;
using Vehicles.Application.Abstractions;

namespace Vehicles.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = CreateBuilder(args);

        ConfigureServices(builder);

        // Logging configuration
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

        builder.Host.UseSerilog();

        var app = CreateWebApplication(builder);
        Configure(app);

        app.Run();
    }

    public static WebApplicationBuilder CreateBuilder(string[] args) =>
        WebApplication.CreateBuilder(args);

    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        // Add DbContext and repositories:
        builder.Services.AddDbContext<VehiclesDbContext>(options =>
            options.UseSqlServer("Server=localhost,1433;Database=Vehicles;User Id=sa;Password=olegandilie100S&;TrustServerCertificate=true"));

        builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyMarker).Assembly));

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
        builder.Services.AddScoped<IAdminRepository, AdminRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IPostRepository, PostRepository>();
        builder.Services.AddScoped<IModelRepository, ModelRepository>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }

    public static WebApplication CreateWebApplication(WebApplicationBuilder builder) =>
        builder.Build();

    public static void Configure(WebApplication app)
    {
        app.UseSerilogRequestLogging();

        app.Use(async (context, next) =>
        {
            var stopwatch = Stopwatch.StartNew();
            await next.Invoke(context);
            stopwatch.Stop();
            Log.Information($"Request took {stopwatch.ElapsedMilliseconds} ms");
        });

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseExceptionHandler("/error");
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
    }
}
