using System.Net.Mail;
using Vehicles.Domain.Notifications.Models;

namespace Vehicles.NotificationsProcessing.Services;

public class EmailService : IEmailService
{
    public async Task SendAsync(Notification notification)
    {
        string? recipientEmail = null;

        if (notification is UserNotification userNotification)
        {
            recipientEmail = userNotification.User?.ApplicationUser?.Email;
        }
        else if (notification is CompanyNotification companyNotification)
        {
            recipientEmail = companyNotification.Company?.ApplicationUser?.Email;
        }

        if (string.IsNullOrWhiteSpace(recipientEmail))
        {
            Console.WriteLine("Email not sent: recipient email not found.");
            return;
        }

        using var mail = new MailMessage(
            from: "no-reply@vehicles.com",
            to: recipientEmail,
            subject: notification.Title,
            body: notification.Body);
        
        mail.IsBodyHtml = true;

        using var smtp = new SmtpClient("localhost", 25);
        await smtp.SendMailAsync(mail);
    }

}