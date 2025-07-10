using Vehicles.Application.Vehicles.Models.Responses;
using Vehicles.Domain.VehicleTypes.Models.VehicleModels;

namespace Vehicles.Application.Abstractions;

public interface IModelRepository
{
    Task<VehicleModel> CreateAsync(Brand brand, Model model, Year year);
    
    Task RemoveAsync(Brand brand, Model model, Year year);
    
    Task<bool> ExistsAsync(Brand brand, Model model, Year year);

    Task<List<string>> GetAllBrandsAsync();
    
    Task<List<string>> GetAllModelsByBrandAsync(string brand);
    
    Task<List<int>> GetAllYearsByBrandAndModelAsync(string brand, string model);
}