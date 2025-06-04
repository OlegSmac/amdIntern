using System.Diagnostics;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vehicles.Application;
using Vehicles.Infrastructure;
using Vehicles.Infrastructure.Repositories;
using Vehicles.Application.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext and repositories:
builder.Services.AddDbContext<VehiclesDbContext>(options => 
    options.UseSqlServer("Server=localhost,1433;Database=Vehicles;User Id=sa;Password=olegandilie100S&;TrustServerCertificate=true"));

// Add MediatR handlers (from your Application assembly)
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyMarker).Assembly));

// Add repositories and UnitOfWork implementations:
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

var app = builder.Build();

app.Use(async (context, next) =>
{
    var stopwatch = Stopwatch.StartNew();

    await next.Invoke(context);

    stopwatch.Stop();
    app.Logger.LogInformation($"Request took {stopwatch.ElapsedMilliseconds} ms");
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();