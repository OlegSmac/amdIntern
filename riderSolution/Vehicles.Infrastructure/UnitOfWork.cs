using Microsoft.EntityFrameworkCore.Storage;
using Vehicles.Application.Abstractions;

namespace Vehicles.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly VehiclesDbContext _context;
    private IDbContextTransaction? _transaction;
    
    public IVehicleRepository VehicleRepository { get; private set; }
    public IUserRepository UserRepository { get; private set; }
    public ICompanyRepository CompanyRepository { get; private set; }
    public IAdminRepository AdminRepository { get; private set; }
    public ICategoryRepository CategoryRepository { get; private set; }
    public IPostRepository PostRepository { get; private set; }
    

    public UnitOfWork(VehiclesDbContext context, IVehicleRepository vehicleRepository, IUserRepository userRepository, 
        ICompanyRepository companyRepository, IAdminRepository adminRepository, ICategoryRepository categoryRepository, 
        IPostRepository postRepository)
    {
        _context = context;
        VehicleRepository = vehicleRepository;
        UserRepository = userRepository;
        CompanyRepository = companyRepository;
        AdminRepository = adminRepository;
        CategoryRepository = categoryRepository;
        PostRepository = postRepository;
    }

    public async Task BeginTransactionAsync()
    {
        if (_transaction == null)
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}