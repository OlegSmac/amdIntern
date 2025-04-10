using System.Text;

namespace project1.Lesson_5_Assignment;

public class Lesson5Main
{
    private static string[] SplitWords(string text)
    {
        return text.Split([' ', ',', '.', '!', '?', '\n', '\t'], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }
    
    private static int CountWords(string text)
    {
        return SplitWords(text).Length;
    }

    private static int CountSentences(string text)
    {
        int sentences = 0;
        
        foreach (char c in text)
        {
            if (c == '.' || c == '!' || c == '?') sentences++; 
        }
        
        return sentences;
    }

    private static int CountWord(string text, string word)
    {
        text = text.ToLower();
        word = word.ToLower();
        
        string[] words = SplitWords(text);
        int count = 0;
        foreach (string w in words)
        {
            if (w == word) count++;
        }
        
        return count;

        //return KMP.FindAllSubstringsCount(text, word);
    }

    private static string ReverseString(string text)
    {
        int n = text.Length;
        StringBuilder sb = new StringBuilder(n);
        for (int i = n - 1; i >= 0; i--)
        {
            sb.Append(text[i]);
        }
        
        return sb.ToString();
    }

    private static string ReplaceSequenceInText(string text, string previousSequence, string newSequence)
    {
        return text.Replace(previousSequence, newSequence);
    }

    static void PrintMenu()
    {
        Console.WriteLine("1. Display the word count of this string");
        Console.WriteLine("2. Display the sentence count of this string");
        Console.WriteLine("3. Display how often the word appears in this string");
        Console.WriteLine("4. Display this string in reverse");
        Console.WriteLine("5. In the given string, replace all occurrences of some substring with new substring and display the new string");
        Console.WriteLine("0. Exit\n");
    }

    static void OptionHowOftenWordAppears(string text)
    {
        Console.WriteLine("Enter a word: ");
        string word = Console.ReadLine().ToLower();
        
        Console.WriteLine($"Word \"{word}\" appears in text {CountWord(text, word)} times.\n");
    }

    static void OptionReplaceSequenceInText(string text)
    {
        Console.WriteLine("Enter a sequence: ");
        string previousSequence = Console.ReadLine();
        Console.WriteLine("Enter a new sequence: ");
        string newSequence = Console.ReadLine();
        
        Console.WriteLine($"Replaced string:\n{ReplaceSequenceInText(text, previousSequence, newSequence)}");
    }

    internal static void MainFunction()
    {
        string text = @"In object-oriented programming, encapsulation is a fundamental principle that involves bundling data and methods that operate on that data within a single unit or class. 
                        This concept allows for the hiding of implementation details from the outside world and exposing only the necessary interfaces for interacting with the object. 
                        By encapsulating data and methods together, we promote code reusability, maintainability, and flexibility.One of the key benefits of encapsulation is the ability to enforce access control on the members of a class. 
                        This means we can specify which parts of the class are accessible to the outside world and which are not. 
                        By using access modifiers such as public, private, and protected, we can control the visibility of members, ensuring that they are only accessed in appropriate ways. 
                        For example, we may have a class representing a bank account with properties such as balance and methods for depositing and withdrawing funds. 
                        By making the balance property private and providing public methods for depositing and withdrawing funds, we encapsulate the internal state of the account and ensure that it can only be modified in a controlled manner. 
                        Encapsulation also allows us to enforce data validation and maintain invariants within our classes. By providing controlled access to data through methods, we can ensure that it is always in a valid state. 
                        For instance, when setting the balance of a bank account, we can check that the new balance is not negative before updating the internal state of the object. 
                        Overall, encapsulation is a powerful concept in object-oriented programming that promotes modularity, reusability, and maintainability. 
                        By bundling data and methods together within a class and controlling access to them, we can create more robust and flexible software systems.";
        
        
        // - Display the word count of this string
        // - Display the sentence count of this string
        // - Display how often the word "encapsulation" appears in this string
        // - Display this string in reverse, without using any C# language feature. (Create your own algorith)
        // - In the given string, replace all occurances of "object-oriented programming" with "OOP" and display the new string

        while (true)
        {
            PrintMenu();
            int choice = int.Parse(Console.ReadLine());
            
            if (choice == 0) break;
            if (choice == 1) Console.WriteLine($"Words count: {CountWords(text)}");
            else if (choice == 2) Console.WriteLine($"Sentences count: {CountSentences(text)}");
            else if (choice == 3) OptionHowOftenWordAppears(text);
            else if (choice == 4) Console.WriteLine($"Reverse string:\n{ReverseString(text)}\n");
            else if (choice == 5) OptionReplaceSequenceInText(text);
            else Console.WriteLine("Enter correct option.");

            Console.ReadLine();
        }
        
        // Console.WriteLine($"Words count: {CountWords(text)}");
        // Console.WriteLine($"Sentences count: {CountSentences(text)}");
        //
        // string word = "encapsulation";
        // Console.WriteLine($"Word \"{word}\" appears in text {CountWord(text, word)} times.\n");
        //
        // Console.WriteLine($"Reverse string:\n{ReverseString(text)}\n");
        //
        // string previousSequence = "object-oriented programming", newSequence = "OOP";
        // Console.WriteLine($"Replaced string:\n{ReplaceSequenceInText(text, previousSequence, newSequence)}");
        
        
    }
}