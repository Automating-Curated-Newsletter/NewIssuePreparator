namespace NewIssuePreparator;

class Program
{
    // private const string ReadyForMigrationStatus = "Ready for Curated";

    static async Task Main(string[] args)
    {
        try
        {
            var config = EnvironmentSetup.InitializeConfiguration();

            var records = await config.Airtable.GetContentAsync();

            Console.WriteLine($"Records downloaded from AirTable: {records?.Records.Count()}");

            var nextIssueRecords = records?.Records
                // .Where(x => AirTableRecordExtensions.Status(x) == ReadyForMigrationStatus)
                .Where(x => !string.IsNullOrWhiteSpace(x.TargetCategory()))
                .Take(config.Processing.BatchSize)
                .Select(CuratedLink.Create)
                .ToList();

            Console.WriteLine($"Records ready to be migrated: {nextIssueRecords?.Count}");

            foreach(var link in nextIssueRecords!)
            {
                await config.Curated.AddLinkToNextIssueAsync(link);
            }
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"Configuration Error: {ex.Message}");
            Environment.Exit(1);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Configuration Error: {ex.Message}");
            Environment.Exit(1);
        }
    }
}
