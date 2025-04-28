namespace project1.Lesson_15_Assignment.Users;

public interface ISubscriber
{
    Task ReceiveMessage(string message);
}