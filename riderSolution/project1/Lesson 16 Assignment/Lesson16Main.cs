using project1.Lesson_16_Assignment.Services;
using project1.Lesson_16_Assignment.Text;

namespace project1.Lesson_16_Assignment;

public class Lesson16Main
{
    private static IText _text;
    private static TextConfig _config = new();
    static void OptionSetText()
    {
        Console.WriteLine("Enter text:");
        string text = Console.ReadLine();
        
        _text = TextService.CreateText(text);
        Console.WriteLine("Text was saved.\n");
    }
    
    static void OptionDisplayText()
    {
        Console.WriteLine(_text.GetText() + "\n");
    }

    static void OptionMakeBold()
    {
        _config.Bold = true;
        _text = TextService.FormatText(_text, _config);
        
        Console.WriteLine("Text is bold.\n");
    }
    
    static void OptionRemoveBold()
    {
        _config.Bold = false;
        _text = TextService.FormatText(_text, _config);
        
        Console.WriteLine("Bold format was removed.\n");
    }

    static void OptionMakeItalic()
    {
        _config.Italic = true;
        _text = TextService.FormatText(_text, _config);
        
        Console.WriteLine("Text is italic.\n");
    }
    
    static void OptionRemoveItalic()
    {
        _config.Italic = false;
        _text = TextService.FormatText(_text, _config);
        
        Console.WriteLine("Italic format was removed.\n");
    }

    static void OptionMakeUnderline()
    {
        _config.Underline = true;
        _text = TextService.FormatText(_text, _config);
        
        Console.WriteLine("Text is underline.\n");
    }
    
    static void OptionRemoveUnderline()
    {
        _config.Underline = false;
        _text = TextService.FormatText(_text, _config);
        
        Console.WriteLine("Underline format was removed.\n");
    }

    static void OptionSetColor()
    {
        Console.WriteLine("Enter color:");
        string color = Console.ReadLine();
        Console.WriteLine("Enter background:");
        string background = Console.ReadLine();
        
        _config.Color = color;
        _config.Background = background;
        _text = TextService.FormatText(_text, _config);
        
        Console.WriteLine($"Text has {color} color and {background} background.\n");
    }

    static void OptionSetBaseTextColor()
    {
        _config.Color = "Black";
        _config.Background = "White";
        _text = TextService.FormatText(_text, _config);
        
        Console.WriteLine("Text has black color and white background.\n");
    }
    
    static void PrintMenu()
    {
        Console.WriteLine("1. Set text");
        Console.WriteLine("2. Display text");
        Console.WriteLine("3. Make bold");
        Console.WriteLine("4. Make italic");
        Console.WriteLine("5. Make underline");
        Console.WriteLine("6. Remove bold");
        Console.WriteLine("7. Remove italic");
        Console.WriteLine("8. Remove underline");
        Console.WriteLine("9. Set text color and background");
        Console.WriteLine("10. Set base text color and background");
        
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
            if (choice == 1) OptionSetText();
            else if (choice == 2) OptionDisplayText();
            else if (choice == 3) OptionMakeBold();
            else if (choice == 4) OptionMakeItalic();
            else if (choice == 5) OptionMakeUnderline();
            else if (choice == 6) OptionRemoveBold();
            else if (choice == 7) OptionRemoveItalic();
            else if (choice == 8) OptionRemoveUnderline();
            else if (choice == 9) OptionSetColor();
            else if (choice == 10) OptionSetBaseTextColor();
            else Console.WriteLine("Unknown option. Try again.");
            
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}