namespace project1.Lesson_15_Assignment.Users;

public class Customer : ISubscriber
{
    public int Id { get; set; }
    public string Name { get; }

    public Customer(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public void ReceiveMessage(string message)
    {
        Console.WriteLine($"Customer {Name} received message: {message}");
    }
}