using Microsoft.EntityFrameworkCore;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Infrastructure.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly VehiclesDbContext _context;

    public AdminRepository(VehiclesDbContext context)
    {
        _context = context;
    }

    public async Task<Admin> CreateAsync(Admin admin)
    {
        await _context.Admins.AddAsync(admin);
        return admin;
    }

    public async Task<Admin?> GetByIdAsync(int id)
    {
        return await _context.Admins.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<List<Admin>> GetAllAsync()
    {
        return await _context.Admins.ToListAsync();
    }

    public async Task RemoveAsync(int id)
    {
        var toDelete = await _context.Admins.FirstOrDefaultAsync(a => a.Id == id);
        if (toDelete != null)
        {
            _context.Admins.Remove(toDelete);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Admin> UpdateAsync(Admin admin)
    {
        _context.Admins.Update(admin);
        await _context.SaveChangesAsync();
        return admin;
    }
}