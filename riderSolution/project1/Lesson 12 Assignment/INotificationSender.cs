namespace project1.Lesson_12_Assignment;

public interface INotificationSender
{
    void SendNotification(User fromUser, User toUser, string message);
}