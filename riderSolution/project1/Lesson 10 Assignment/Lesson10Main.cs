using System.Net;
using System.Net.Mail;

namespace project1.Lesson_10_Assignment;

public class Lesson10Main
{
    static void PrintMenu()
    {
        Console.WriteLine("1. Subscribe to the newsletter");
        Console.WriteLine("0. Exit\n");
    }
    
    static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    static void OptionSubscribe()
    {
        Console.WriteLine("Enter your email: ");
        string userEmail = Console.ReadLine();
        
        if (!IsValidEmail(userEmail))
        {
            Console.WriteLine("Invalid email format.");
            return;
        }

        string senderEmail = "oleansmacinih@gmail.com";
        string senderPassword = "uxus xrgx sapu wazn";
        string subject = "Subscribing to the newsletter.";
        string body = "Thank you for subscribing to our newsletter.";

        using SmtpClient smtpClient = new SmtpClient();
        smtpClient.Host = "smtp.gmail.com";
        smtpClient.Port = 587;
        smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
        smtpClient.EnableSsl = true;

        MailMessage mailMessage = new MailMessage(senderEmail, userEmail, subject, body);
        try
        {
            smtpClient.Send(mailMessage);
            Console.WriteLine("Message sent successfully.");
        }
        catch (SmtpException smtpEx)
        {
            Console.WriteLine($"SMTP Error: {smtpEx.Message}");
            if (smtpEx.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {smtpEx.InnerException.Message}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Another exception: {e.Message}");
        }
        finally
        {
            mailMessage.Dispose();
        }
    }
    
    internal static void MainFunction()
    {
        while (true)
        {
            PrintMenu();
            int choice = int.Parse(Console.ReadLine());
            
            if (choice == 0) break;
            if (choice == 1) OptionSubscribe();
            else Console.WriteLine("Enter correct option.");

            Console.ReadLine();
        }
    }    
}