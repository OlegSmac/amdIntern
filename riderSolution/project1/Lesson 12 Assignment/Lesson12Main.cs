namespace project1.Lesson_12_Assignment;

public class Lesson12Main
{
    static readonly Dictionary<int, INotificationSender> notificationChannels = new()
    {
        { 1, new EmailNotification() },
        { 2, new SmsNotification() },
        { 3, new PushNotification() }
    };

    private static List<User> users = new()
    {
        new User("OlegSmac", "oleansmacinih@gmail.com", "+37379240713", "26542"),
        new User("Nikita", "nikita@gmail.com", "079534453", "42244"),
        new User("Tatiana", "tatiana@gmail.com", "+37379101317", "42245")
    };
    
    private static NotificationService service = new NotificationService();

    static User GetUser(Func<User, bool> compare)
    {
        return users.Where(compare).FirstOrDefault();
    }

    static void OptionSendNotification()
    {
        Console.WriteLine("Enter sender nickname:");
        var senderNickname = Console.ReadLine();
        var fromUser = GetUser(user => user.Nickname == senderNickname);
        
        if (fromUser == null)
        {
            Console.WriteLine("User not found.");
            return;
        }
        
        Console.WriteLine("Enter recipient nickname:");
        var recipientNickname = Console.ReadLine();
        var toUser = GetUser(user => user.Nickname == recipientNickname);

        if (toUser == null)
        {
            Console.WriteLine("User not found.");
            return;
        }

        Console.WriteLine("Enter your message:");
        var message = Console.ReadLine() ?? "";

        Console.WriteLine("\nChoose notification channel:");
        foreach (var entry in notificationChannels)
        {
            Console.WriteLine($"{entry.Key}. {entry.Value.GetType().Name.Replace("Notification", "")}");
        }

        Console.Write("Your choice: ");
        if (!int.TryParse(Console.ReadLine(), out int choice))
        {
            Console.WriteLine("Invalid input.");
            return;
        }

        if (notificationChannels.TryGetValue(choice, out INotificationSender channel))
        {
            service.Notify(fromUser, toUser, message, channel);
        }
        else Console.WriteLine("Invalid choice.");
    }

    static void OptionAddUser()
    {
        Console.WriteLine("Enter nickname:");
        var userNickname = Console.ReadLine();
        Console.WriteLine("Enter email:");
        var userEmail = Console.ReadLine();
        Console.WriteLine("Enter phone:");
        var userPhone = Console.ReadLine();
        Console.WriteLine("Enter device id:");
        var userDeviceId = Console.ReadLine();

        if (GetUser(user =>
                user.Nickname == userNickname ||
                user.Email == userEmail ||
                user.PhoneNumber == userPhone ||
                user.DeviceId == userDeviceId) != null)
        {
            Console.WriteLine("A user with the same nickname, email, phone, or device ID already exists.");
        }
        else
        {
            users.Add(new User(userNickname, userEmail, userPhone, userDeviceId));
            Console.WriteLine("User was added.");
        }
    }
    
    static void PrintMenu()
    {
        Console.WriteLine("1. Add user");
        Console.WriteLine("2. Send notification");
        Console.WriteLine("0. Exit\n");
    }
    
    internal static void MainFunction()
    {
        while (true)
        {
            PrintMenu();
            int choice = int.Parse(Console.ReadLine());
            
            if (choice == 0) break;
            if (choice == 1) OptionAddUser();
            else if (choice == 2) OptionSendNotification();
            else Console.WriteLine("Enter correct option.");

            Console.ReadLine();
        }
    }    
}