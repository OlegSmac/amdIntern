using Microsoft.EntityFrameworkCore.Storage;
using Vehicles.Application.Abstractions;

namespace Vehicles.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly VehiclesDbContext _context;
    
    public IVehicleRepository VehicleRepository { get; private set; }
    public IUserRepository UserRepository { get; private set; }
    public ICompanyRepository CompanyRepository { get; private set; }
    public IAdminRepository AdminRepository { get; private set; }
    public ICategoryRepository CategoryRepository { get; private set; }
    public IPostRepository PostRepository { get; private set; }
    public IModelRepository ModelRepository { get; private set; }
    

    public UnitOfWork(VehiclesDbContext context, IVehicleRepository vehicleRepository, IUserRepository userRepository, 
        ICompanyRepository companyRepository, IAdminRepository adminRepository, ICategoryRepository categoryRepository, 
        IPostRepository postRepository, IModelRepository modelRepository)
    {
        _context = context;
        VehicleRepository = vehicleRepository;
        UserRepository = userRepository;
        CompanyRepository = companyRepository;
        AdminRepository = adminRepository;
        CategoryRepository = categoryRepository;
        PostRepository = postRepository;
        ModelRepository = modelRepository;
    }

    public async Task ExecuteTransactionAsync(Func<Task> transaction)
    {
        try
        {
            await _context.Database.BeginTransactionAsync();
            await transaction();
            await _context.Database.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await _context.Database.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}