namespace project1;

public interface IVehicleRepository<T> where T : Vehicle
{
    IList<T> FindAll();

    T GetById(int id);
    void Add(T vehicle);
    void ExcludeFromDealership(T vehicle);
    void Update(T vehicle);
}