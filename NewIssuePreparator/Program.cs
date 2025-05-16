using System;

namespace NewIssuePreparator;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            var config = EnvironmentSetup.InitializeConfiguration();
            EnvironmentSetup.DisplayConfiguration(config);
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"Configuration Error: {ex.Message}");
            Environment.Exit(1);
        }
    }
}
