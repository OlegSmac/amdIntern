namespace project1;

public interface IRepository<T> where T : Vehicle
{
    IList<T> FindAll();
    void Add(T vehicle);
    T TakeById(int id);
    void Update(T vehicle);
}