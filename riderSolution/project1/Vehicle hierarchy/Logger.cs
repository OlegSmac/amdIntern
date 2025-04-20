namespace project1;

public static class Logger
{
    public static async Task Log(string methodName, bool success, string errorMessage = null)
    {
        string logDir = "/home/thinkpad/Amdaris/amdIntern/riderSolution/project1/Vehicle hierarchy/logs";
        if (!Directory.Exists(logDir)) Directory.CreateDirectory(logDir);
        
        DateTime dateTime = DateTime.Now;
        string fileName = Path.Combine(logDir, $"Logs_{dateTime:yyyy-MM-dd}.txt");
        string outcome = success ? "Success" : $"Failure: {errorMessage}";
        string log = $"{dateTime:yyyy-MM-dd HH:mm:ss} method: {methodName}, outcome: {outcome}\n";
        
        // Console.WriteLine($"Current directory: {Environment.CurrentDirectory}");
        // Console.WriteLine($"Log file path: {fileName}");

        await File.AppendAllTextAsync(fileName, log);
    }
}