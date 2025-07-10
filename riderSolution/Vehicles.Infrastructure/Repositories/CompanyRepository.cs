using Microsoft.EntityFrameworkCore;
using Vehicles.Application.Abstractions;
using Vehicles.Application.PaginationModels;
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

    public async Task<Company?> GetByIdAsync(string id)
    {
        return await _context.Companies
            .Include(c => c.ApplicationUser)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task RemoveAsync(string id)
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
    
    public async Task<PaginatedResult<Company>> GetPagedCompanies(int pageIndex, int pageSize)
    {
        var query = _context.Companies.AsQueryable();

        var total = await query.CountAsync();

        var items = await query
            .OrderBy(c => c.Name)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedResult<Company>
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            Total = total,
            Items = items
        };
    }

    public async Task<List<string>> GetCompanySubscribers(string companyId)
    {
        return await _context.Subscriptions
            .Where(s => s.CompanyId == companyId)
            .Select(s => s.User.Id)
            .ToListAsync();
    }
}