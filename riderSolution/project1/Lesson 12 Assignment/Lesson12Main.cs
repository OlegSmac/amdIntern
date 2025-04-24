namespace project1.Lesson_12_Assignment;

public class Lesson12Main
{
    private static NotificationService<string> service = new NotificationService<string>();

    static User GetSender()
    {
        Console.WriteLine("Enter sender nickname:");
        var senderNickname = Console.ReadLine();
        return UserService.GetUser(user => user.Nickname == senderNickname);
    }

    static User GetReceiver()
    {
        Console.WriteLine("Enter recipient nickname:");
        var recipientNickname = Console.ReadLine();
        return UserService.GetUser(user => user.Nickname == recipientNickname);
    }

    static void PrintChannelTypes()
    {
        Console.WriteLine("\nChoose notification channel:");
        foreach (var entry in Enum.GetValues(typeof(NotificationService<>.NotificationType)))
        {
            Console.WriteLine($"{entry}");
        }
    }
    
    static void OptionSendNotification()
    {
        User fromUser = GetSender();
        if (fromUser == null)
        {
            Console.WriteLine("User not found.");
            return;
        }
        
        User toUser = GetReceiver();
        if (toUser == null)
        {
            Console.WriteLine("User not found.");
            return;
        }

        Console.WriteLine("Enter your message:");
        string message = Console.ReadLine() ?? "";

        PrintChannelTypes();

        Console.Write("Your choice: ");
        if (!int.TryParse(Console.ReadLine(), out int choice))
        {
            Console.WriteLine("Invalid input.");
            return;
        }

        service.Notify(fromUser, toUser, message, (NotificationService<string>.NotificationType)choice);
    }
    
    static User GetUserInput()
    {
        Console.WriteLine("Enter nickname:");
        var nickname = Console.ReadLine();
        Console.WriteLine("Enter email:");
        var email = Console.ReadLine();
        Console.WriteLine("Enter phone:");
        var phone = Console.ReadLine();
        Console.WriteLine("Enter device id:");
        var deviceId = Console.ReadLine();

        return new User(nickname, email, phone, deviceId);
    }

    static void TryAddUser(User user)
    {
        if (UserValidator.CanUserBeAdded(user))
        {
            UserService.AddUser(user);
            Console.WriteLine("User was added.");
        }
        else
        {
            Console.WriteLine("A user with the same nickname, email, phone, or device ID already exists.");
        }
    }
    
    static void OptionAddUser()
    {
        User inputUser = GetUserInput();
        TryAddUser(inputUser);
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