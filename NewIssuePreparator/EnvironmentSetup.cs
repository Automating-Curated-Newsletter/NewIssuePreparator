using System;
using System.IO;

namespace NewIssuePreparator;

public static class EnvironmentSetup
{
    public static EnvironmentConfig InitializeConfiguration()
    {
        try
        {
            if (Environment.GetEnvironmentVariable("GITHUB_ACTIONS") == null)
            {
                DotNetEnv.Env.Load();
            }
            
            return new EnvironmentConfig();
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Error: .env file not found. If running locally, please create one based on .env.example");
            Environment.Exit(1);
            return null; // This line will never be reached due to Environment.Exit
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Configuration initialization error: {ex.Message}");
            Environment.Exit(1);
            return null; // This line will never be reached due to Environment.Exit
        }
    }

    public static void DisplayConfiguration(EnvironmentConfig config)
    {
        Console.WriteLine("Airtable Configuration:");
        Console.WriteLine($"API Access Token: {config.Airtable.ApiAccessToken}");
        Console.WriteLine($"Base ID: {config.Airtable.BaseId}");
        Console.WriteLine($"Table Name: {config.Airtable.TableName}");
        Console.WriteLine($"API URL: {config.Airtable.ApiUrl}");

        Console.WriteLine("\nCurated Configuration:");
        Console.WriteLine($"API Token: {config.Curated.ApiToken}");
        Console.WriteLine($"API URL: {config.Curated.ApiUrl}");
        Console.WriteLine($"Publication ID: {config.Curated.PublicationId}");
    }
} 