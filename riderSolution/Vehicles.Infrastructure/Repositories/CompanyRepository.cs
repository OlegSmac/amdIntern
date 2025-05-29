using Microsoft.EntityFrameworkCore;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Notifications.Models;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Infrastructure.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly VehiclesDbContext _context;

    public CompanyRepository(VehiclesDbContext context)
    {
        _context = context;
    }

    public async Task<Company> CreateAsync(Company company)
    {
        await _context.Companies.AddAsync(company);
        return company;
    }

    public async Task<Company?> GetByIdAsync(int id)
    {
        return await _context.Companies.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Company>> GetAllAsync()
    {
        return await _context.Companies.ToListAsync();
    }

    public async Task RemoveAsync(int id)
    {
        var toDelete = await _context.Companies.FirstOrDefaultAsync(c => c.Id == id);
        if (toDelete != null)
        {
            _context.Companies.Remove(toDelete);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Company> UpdateAsync(Company company)
    {
        _context.Companies.Update(company);
        await _context.SaveChangesAsync();
        return company;
    }

    public async Task<CompanyNotification> CreateCompanyNotification(CompanyNotification notification)
    {
        await _context.CompanyNotifications.AddAsync(notification);
        return notification;
    }
}