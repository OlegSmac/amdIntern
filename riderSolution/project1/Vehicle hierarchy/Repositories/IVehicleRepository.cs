namespace project1;

public interface IVehicleRepository<T> where T : Vehicle
{
    IList<T> FindAll();

    T GetById(int id);
    Task Add(T vehicle);
    Task ExcludeFromDealership(T vehicle);
    Task Update(T vehicle);
}