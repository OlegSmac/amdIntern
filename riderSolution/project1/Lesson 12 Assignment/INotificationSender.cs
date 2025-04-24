namespace project1.Lesson_12_Assignment;

public interface INotificationSender<T>
{
    void SendNotification(User fromUser, User toUser, T message);
}