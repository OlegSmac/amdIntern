namespace project1.Lesson_15_Assignment.Users;

public class Staff : ISubscriber
{
    public int Id { get; set; }
    public string Name { get; }

    public Staff(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public async Task ReceiveMessage(string message)
    {
        Console.WriteLine($"Staff {Name} received message: {message}");
    }
}