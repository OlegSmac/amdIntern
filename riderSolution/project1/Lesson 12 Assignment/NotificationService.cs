namespace project1.Lesson_12_Assignment;

public class NotificationService<T>
{
    static readonly Dictionary<NotificationType, INotificationSender<T>> notificationChannels = new() //change to enum 
    {
        { NotificationType.Email, new EmailNotification<T>() },
        { NotificationType.SMS, new SmsNotification<T>() },
        { NotificationType.Push, new PushNotification<T>() }
    };
    
    public void Notify(User fromUser, User toUser, T message, NotificationType channel)
    {
        //Console.WriteLine($"{fromUser.Nickname} is sending a message to {toUser.Nickname}...\n");
        notificationChannels[channel].SendNotification(fromUser, toUser, message);
    }
    
    public enum NotificationType
    {
        Email = 1,
        SMS = 2,
        Push = 3
    }
}
