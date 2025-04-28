using project1.Lesson_16_Assignment.Text;

namespace project1.Lesson_16_Assignment.Services;

public static class TextService
{
    public static IText CreateText(string text)
    {
        return new PlainText(text);
    }
    
    public static IText FormatText(IText text, TextConfig config)
    {
        Console.WriteLine("Created text:");
        Console.WriteLine(text.GetText());
        
        if (config.Bold) Console.Write("Bold ");
        if (config.Italic) Console.Write("Italic ");
        if (config.Underline) Console.Write("Underline ");
        Console.Write($"Color: {config.Color} ");
        Console.Write($"Background: {config.Background} ");
        Console.WriteLine();
        
        return text;
    }
    
}