namespace Vehicles.Application.Abstractions;

public interface IUnitOfWork
{
    public IVehicleRepository VehicleRepository { get; }
    public IUserRepository UserRepository { get; }
    public ICompanyRepository CompanyRepository { get; }
    public IAdminRepository AdminRepository { get; }
    public ICategoryRepository CategoryRepository { get; }
    public IPostRepository PostRepository { get; }
    public IModelRepository ModelRepository { get; }
    
    
    Task ExecuteTransactionAsync(Func<Task> transaction);
    Task SaveAsync();
}