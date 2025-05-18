using System.Net;
using System.Net.Http.Headers;
using System.Text;
using static NewIssuePreparator.EnvironmentConfig;

namespace NewIssuePreparator;

public static class CuratedExtensions
{
    public static string ForPublication(this string CuratedApiEndpoint, string publicationId)
        => $"{CuratedApiEndpoint}/{publicationId}";

    private static string ComposePostUrl(this string CuratedApiEndpoint, CuratedLink link)
    {
        StringBuilder builder = new($"{CuratedApiEndpoint}/links?");

        builder.Append($"url={WebUtility.UrlEncode(link.Url)}");
        builder.Append($"&title={WebUtility.UrlEncode(link.Title)}");
        builder.Append($"&category={WebUtility.UrlEncode(link.Category.ToLower())}");

        if (!string.IsNullOrWhiteSpace(link.Image))
        {
            builder.Append($"&image={WebUtility.UrlEncode(link.Image)}");
        }

        return builder.ToString();
    }

    public static async Task<bool> AddLinkToNextIssueAsync(this CuratedConfig curatedConfig, CuratedLink link)
    {
        var url = curatedConfig.ApiUrl.ForPublication(curatedConfig.PublicationId).ComposePostUrl(link);
        Console.WriteLine($"Sending: ({link.Category}) {link.Title}");

        using HttpClient client = new();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", $"token=\"{curatedConfig.ApiToken}\"");
        var response = await client.PostAsync(url, null);

        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = response.IsSuccessStatusCode ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine($"API call {(response.IsSuccessStatusCode ? "successful" : $"failed with status {response.StatusCode}")}");
        Console.ForegroundColor = originalColor;
        
        return response.IsSuccessStatusCode;
    }
}
