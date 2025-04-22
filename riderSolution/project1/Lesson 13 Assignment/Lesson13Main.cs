using project1.Lesson_13_Assignment.BusinessLayer;

namespace project1.Lesson_13_Assignment;

public class Lesson13Main
{
    private static IRepository _repository = new Repository();
    static void OptionAddSpeaker()
    {
        Console.WriteLine("Enter the speaker first name:");
        string firstName = Console.ReadLine();
        Console.WriteLine("Enter the speaker last name:");
        string lastName = Console.ReadLine();
        Console.WriteLine("Enter the speaker email:");
        string email = Console.ReadLine();
        Console.WriteLine("Enter the speaker expirience:");
        int exp = int.Parse(Console.ReadLine());
        Console.WriteLine("Does this speaker have a blog:");
        bool hasBlog = bool.Parse(Console.ReadLine() ?? "false");
        
        string blogUrl = null;
        if (hasBlog)
        {
            Console.WriteLine("Enter the speaker's blog url:");
            blogUrl = Console.ReadLine();
        }
        
        Console.WriteLine("Enter which web browser does the speaker use:");
        string browserName = Console.ReadLine();
        
        Console.WriteLine("Enter the speaker certification's count:");
        int certificationsCount = int.Parse(Console.ReadLine());
        var certifications = new List<string>(certificationsCount);
        for (int i = 0; i < certificationsCount; i++)
        {
            Console.WriteLine($"Enter certification {i + 1}:");
            certifications.Add(Console.ReadLine());
        }
        
        Console.WriteLine("Enter the employer:");
        string employer = Console.ReadLine();
        
        Console.WriteLine("Enter the speaker session's count:");
        int sessionCount = int.Parse(Console.ReadLine());
        var sessions = new List<Session>(sessionCount);
        for (int i = 0; i < sessionCount; i++)
        {
            Console.WriteLine($"Enter the session {i + 1} title:");
            string sessionTitle = Console.ReadLine();
            Console.WriteLine($"Enter the session {i + 1} description:");
            string sessionDescription = Console.ReadLine();
            
            sessions.Add(new Session(sessionTitle, sessionDescription));
        }

        var speaker = new Speaker
        {
            FirstName = firstName,
            LastName = lastName, 
            Email = email,
            Exp = exp,
            HasBlog = hasBlog,
            BlogURL = blogUrl,
            Browser = new WebBrowser(browserName, 1),
            Certifications = certifications, Employer = employer,
            Sessions = sessions
        };
        // var speaker = new Speaker
        // {
        //     FirstName = "Oleg",
        //     LastName = "Smac",
        //     Email = "oleg@gmail.com",
        //     Exp = 1,
        //     HasBlog = true,
        //     BlogURL = "https://fdf",
        //     Browser = new WebBrowser("Chrome", 100),
        //     Certifications = new List<string> { "ICPC", "SQL", "C#" },
        //     Employer = "Microsoft",
        //     Sessions = new List<Session>
        //     {
        //         new Session("Modern C#", "Talk about new C# features.")
        //     }
        // };

        int? speakerId = null;
        try
        {
            speakerId = speaker.Register(_repository);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine($"Speaker '{speaker.FirstName} {speaker.LastName}' was{(speakerId == null ? "'t" : "")} saved{(speakerId == null ? "" : $" with id = {speakerId}")}.");
        }
        
    }
    
    static void PrintMenu()
    {
        Console.WriteLine("1. Add speaker");
        Console.WriteLine("0. Exit\n");
    }
    
    internal static void MainFunction()
    {
        while (true)
        {
            PrintMenu();
            Console.Write("Enter choice: ");
            string input = Console.ReadLine();
            if (!int.TryParse(input, out int choice))
            {
                Console.WriteLine("Invalid input. Try again.");
                continue;
            }
            
            if (choice == 0) break;
            if (choice == 1) OptionAddSpeaker();
            else Console.WriteLine("Unknown option. Try again.");

            Console.ReadLine();
        }
    }    
}