namespace Vehicles.Application.Abstractions;

public interface IUnitOfWork
{
    public IVehicleRepository VehicleRepository { get; }
    public IUserRepository UserRepository { get; }
    public ICompanyRepository CompanyRepository { get; }
    public IAdminRepository AdminRepository { get; }
    public ICategoryRepository CategoryRepository { get; }
    public IPostRepository PostRepository { get; }
    
    Task SaveAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}