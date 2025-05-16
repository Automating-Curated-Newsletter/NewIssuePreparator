using System;

namespace NewIssuePreparator;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            var config = EnvironmentSetup.InitializeConfiguration();

            var records = (await config.Airtable.ApiUrl.GetContentAsync(
                config.Airtable.ApiAccessToken, config.Airtable.FetchFromView
            )).Parse();

            Console.WriteLine($"Records downloaded: {records?.Records.Count()}");

            var nextIssueRecords = records?.Records
                .Where(x => AirTableRecordExtensions.Status(x) == "Next issue")
                .Where(x => !string.IsNullOrWhiteSpace(x.TargetCategory()))
                .Take(49)
                .Select(CuratedLink.Create)
                .ToList();

            Console.WriteLine($"Records found: {nextIssueRecords?.Count}");

            foreach(var link in nextIssueRecords!)
            {
                await config.Curated.ApiUrl
                    .ForPublication(config.Curated.PublicationId)
                    .AddLinkToNextIssueAsync(config.Curated.ApiToken, link);
            }
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"Configuration Error: {ex.Message}");
            Environment.Exit(1);
        }
    }
}
