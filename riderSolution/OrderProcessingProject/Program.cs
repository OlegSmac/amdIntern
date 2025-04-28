using OrderProcessingProject;

class Program
{
    static async Task Main(string[] args)
    {
        OrderService.StartOrderProcessing();
        
        Console.ReadLine();
    }
}