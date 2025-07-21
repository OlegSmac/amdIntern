using System.Diagnostics;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;
using Vehicles.API.Extensions;
using Vehicles.API.Middlewares;
using Vehicles.API.Options;
using Vehicles.API.Services;
using Vehicles.Application;
using Vehicles.Infrastructure;
using Vehicles.Infrastructure.Repositories;
using Vehicles.Application.Abstractions;
using Vehicles.Application.PaginationModels;
using Vehicles.Domain.Notifications.Models;
using DotNetEnv;
using FluentValidation.AspNetCore;
using Vehicles.API.Contracts.DTOValidations.Vehicles;
using Vehicles.API.Hubs;
using Vehicles.Application.Requests.Auth.Commands;
using Vehicles.Application.Requests.Notifications.Queries;

namespace Vehicles.API;

public class Program
{
    public static void Main(string[] args)
    {
        Env.Load();
        
        var builder = WebApplication.CreateBuilder(args);
        
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

        builder.Host.UseSerilog();
        
        ConfigureServices(builder); 

        var app = builder.Build();
        Configure(app);
        app.Run();
    }

    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        var connectionString = Environment.GetEnvironmentVariable("VEHICLES_DB_CONNECTION");
        
        builder.Services.AddDbContext<VehiclesDbContext>(options =>
            options.UseSqlServer(connectionString));
        
        builder.RegisterAuthentication();
        builder.Services.AddSwagger();
        
        builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyMarker).Assembly));
        
        builder.Services.AddTransient<IRequestHandler<GetNotificationsPaged<UserNotification>, PaginatedResult<UserNotification>>, GetNotificationsPagedHandler<UserNotification>>();
        builder.Services.AddTransient<IRequestHandler<GetNotificationsPaged<CompanyNotification>, PaginatedResult<CompanyNotification>>, GetNotificationsPagedHandler<CompanyNotification>>();
        builder.Services.AddTransient<IRequestHandler<GetNotificationsPaged<AdminNotification>, PaginatedResult<AdminNotification>>, GetNotificationsPagedHandler<AdminNotification>>();

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
        builder.Services.AddScoped<IAdminRepository, AdminRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IPostRepository, PostRepository>();
        builder.Services.AddScoped<IImageRepository, ImageRepository>();
        builder.Services.AddScoped<IModelRepository, ModelRepository>();
        builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
        builder.Services.AddScoped<INotificationSender, NotificationService>();
        builder.Services.AddScoped<IStatisticsRepository, StatisticsRepository>();
        
        builder.Services.AddScoped<IRegistrationHandler, UserRegistrationHandler>();
        builder.Services.AddScoped<IRegistrationHandler, CompanyRegistrationHandler>();
        builder.Services.AddScoped<IRegistrationHandler, AdminRegistrationHandler>();
        builder.Services.AddScoped<RegistrationHandlerFactory>();
        builder.Services.AddScoped<RegistrationService>();
        builder.Services.AddScoped<IdentityService>();
        
        builder.Services.Configure<MinioSettings>(options =>
        {
            builder.Configuration.GetSection("Minio").Bind(options);
            options.AccessKey = Environment.GetEnvironmentVariable("MINIO_ACCESS_KEY") ?? options.AccessKey;
            options.SecretKey = Environment.GetEnvironmentVariable("MINIO_SECRET_KEY") ?? options.SecretKey;
        });
        builder.Services.AddSingleton<MinioService>();
        
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
        
        builder.Services.AddControllers()
            .AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<VehicleDTOValidator>();
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.TypeNameHandling = TypeNameHandling.None;
            });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        
        builder.Services.AddSignalR();
    }

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
        app.UseCors();
        
        app.UseAuthentication(); 
        app.UseAuthorization();
        
        app.MapHub<NotificationHub>("/hubs/notifications");
        
        app.MapControllers();
    }
}
