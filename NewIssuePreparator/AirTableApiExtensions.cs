using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using static NewIssuePreparator.EnvironmentConfig;

namespace NewIssuePreparator;

public static class AirTableApiExtensions
{
    private static string ComposeAirTableApiUrl(this AirtableConfig airtableConfig)
    {
        StringBuilder builder = new(airtableConfig.ApiUrl);
        builder.Append($"/{airtableConfig.BaseId}/{airtableConfig.TableName}");

        if (!string.IsNullOrWhiteSpace(airtableConfig.FetchFromView))
            builder.Append($"?view={airtableConfig.FetchFromView}");

        return builder.ToString();
    }

    public static async Task<AirTableRecords?> GetContentAsync(this AirtableConfig airtableConfig)
    {
        string requestUri = airtableConfig.ComposeAirTableApiUrl();

        AirTableRecords? rawRecords = null;
        try
        {
            using HttpClient client = new();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", airtableConfig.ApiAccessToken);
            string v = await client.GetStringAsync(requestUri);

            Console.WriteLine(v);
            
            rawRecords = v.Parse();
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return rawRecords;
    }

    public static AirTableRecords? Parse(this string rawRecords)
    {
        var newRawRecords = rawRecords.Replace(":true", ":\"true\"");

        try
        {
            return JsonSerializer.Deserialize<AirTableRecords>(newRawRecords, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true

            });
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}
