using System.Net.Mail;

namespace project1.Lesson_12_Assignment;

public class EmailNotification<T> : INotificationSender<T>
{
    public void SendNotification(User fromUser, User toUser, T message)
    {
        Console.WriteLine($"[EMAIL] From: {fromUser.Email} | To: {toUser.Email} | Message: {message}");
        
        try
        {
            using var client = new SmtpClient("localhost", 25);
            using var mail = new MailMessage(fromUser.Email, toUser.Email, "Mail message", message.ToString());
            client.Send(mail);
            Console.WriteLine("Email sent successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EMAIL] Failed to send: {ex.Message}");
        }
    }
}

public class SmsNotification<T> : INotificationSender<T>
{
    public void SendNotification(User fromUser, User toUser, T message)
    {
        Console.WriteLine($"[SMS] From: {fromUser.PhoneNumber} | To: {toUser.PhoneNumber} | Message: {message}");
    }
}

public class PushNotification<T> : INotificationSender<T>
{
    public void SendNotification(User fromUser, User toUser, T message)
    {
        Console.WriteLine($"[PUSH] From: {fromUser.DeviceId} | To: {toUser.DeviceId} | Message: {message}");
    }
}
