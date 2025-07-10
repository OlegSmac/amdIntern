namespace Vehicles.Application.Abstractions;

public interface IUnitOfWork
{
    public IVehicleRepository VehicleRepository { get; }
    public IUserRepository UserRepository { get; }
    public ICompanyRepository CompanyRepository { get; }
    public IAdminRepository AdminRepository { get; }
    public ICategoryRepository CategoryRepository { get; }
    public IPostRepository PostRepository { get; }
    public IImageRepository ImageRepository { get; }
    public IModelRepository ModelRepository { get; }
    public INotificationRepository NotificationRepository { get; }
    
    
    Task ExecuteTransactionAsync(Func<Task> transaction);
    Task SaveAsync();
}