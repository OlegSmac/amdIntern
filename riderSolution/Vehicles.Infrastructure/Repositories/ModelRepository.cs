using Microsoft.EntityFrameworkCore;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Vehicles.Models.Responses;
using Vehicles.Domain.VehicleTypes.Models.VehicleModels;

namespace Vehicles.Infrastructure.Repositories;

public class ModelRepository : IModelRepository
{
    private readonly VehiclesDbContext _context;

    public ModelRepository(VehiclesDbContext context)
    {
        _context = context;
    }
    
    public async Task<VehicleModel> CreateAsync(Brand brand, Model model, Year year)
    {
        var existingBrand = await _context.Brands.FirstOrDefaultAsync(b => b.Name == brand.Name);
        if (existingBrand == null)
        {
            await _context.Brands.AddAsync(brand);
            existingBrand = brand;
        }
        
        var existingModel = await _context.Models.FirstOrDefaultAsync(m => m.Name == model.Name);
        if (existingModel == null)
        {
            model.Brand = existingBrand;
            await _context.Models.AddAsync(model);
            existingModel = model;
        }
        
        var existingYear = await _context.Years.FirstOrDefaultAsync(y => y.YearNum == year.YearNum);
        if (existingYear == null)
        {
            await _context.Years.AddAsync(year);
            existingYear = year;
        }
        
        if (!existingModel.Years.Any(y => y.Id == existingYear.Id))
        {
            existingModel.Years.Add(existingYear);
        }

        return new VehicleModel
        {
            Brand = existingBrand.Name,
            Model = existingModel.Name,
            Year = existingYear.YearNum
        };
    }

    public async Task RemoveAsync(Brand brand, Model model, Year year)
    {
        var existingBrand = await _context.Brands
            .Include(b => b.Models)
            .FirstOrDefaultAsync(b => b.Name == brand.Name);

        if (existingBrand == null) return;

        var existingModel = await _context.Models
            .Include(m => m.Years)
            .Include(m => m.Brand)
            .FirstOrDefaultAsync(m => m.Name == model.Name && m.Brand.Name == brand.Name);

        if (existingModel == null) return;

        var existingYear = await _context.Years
            .Include(y => y.Models)
            .FirstOrDefaultAsync(y => y.YearNum == year.YearNum);

        if (existingYear == null) return;
        
        //Removing model
        if (existingModel.Years.Contains(existingYear)) existingModel.Years.Remove(existingYear);
        if (!existingModel.Years.Any()) _context.Models.Remove(existingModel);
        
        //Removing year
        if (!existingYear.Models.Any(m => m.Id != existingModel.Id || existingModel.Years.Contains(existingYear)))
        {
            _context.Years.Remove(existingYear);
        }
        
        //Removing brand
        if (!existingBrand.Models.Any(m => m.Id != existingModel.Id || m.Years.Any()))
        {
            _context.Brands.Remove(existingBrand);
        }
    }

    public async Task<bool> ExistsAsync(Brand brand, Model model, Year year)
    {
        var existingModel = await _context.Models
            .Include(m => m.Years)
            .Include(m => m.Brand)
            .FirstOrDefaultAsync(m =>
                m.Name == model.Name &&
                m.Brand.Name == brand.Name &&
                m.Years.Any(y => y.YearNum == year.YearNum));

        return existingModel != null;
    }

    public async Task<List<string>> GetAllBrandsAsync()
    {
        return await _context.Models
            .Select(m => m.Brand.Name)
            .Distinct()
            .ToListAsync();
    }

    public async Task<List<string>> GetAllModelsByBrandAsync(string brand)
    {
        return await _context.Models
            .Where(m => m.Brand.Name == brand)
            .Select(m => m.Name)
            .Distinct()
            .ToListAsync();
    }

    public async Task<List<int>> GetAllYearsByBrandAndModelAsync(string brand, string model)
    {
        return await _context.Models
            .Where(m => m.Brand.Name == brand && m.Name == model)
            .SelectMany(m => m.Years.Select(y => y.YearNum))
            .Distinct()
            .ToListAsync();
    }

}