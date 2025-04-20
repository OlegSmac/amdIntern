namespace project1.Lesson_12_Assignment;

public class NotificationService
{
    public void Notify(User fromUser, User toUser, string message, INotificationSender channel)
    {
        //Console.WriteLine($"{fromUser.Nickname} is sending a message to {toUser.Nickname}...\n");
        channel.SendNotification(fromUser, toUser, message);
    }
}

