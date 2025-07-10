using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.Infrastructure.Repositories;

public class VehicleRepository : EFCoreRepository, IVehicleRepository
{
    public VehicleRepository(VehiclesDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    { }
}