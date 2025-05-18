namespace NewIssuePreparator;

public class AirTableRecords
{
    public IEnumerable<AirTableRecord> Records { get; set; } = new List<AirTableRecord>();
}

public class AirTableRecord
{
    public string Id { get; set; } = default!;
    public string CreatedTime { get; set; } = default!;
    public Dictionary<string, object> Fields { get; set; } = new Dictionary<string, object>();
}

public static class AirTableRecordExtensions
{
    public static string Name(this AirTableRecord record) => record.Fields.GetStringOrEmpty("Name");
    public static string Status(this AirTableRecord record) => record.Fields.GetStringOrEmpty("Status");
    public static string Url(this AirTableRecord record) => record.Fields.GetStringOrEmpty("URL");
    public static string Source(this AirTableRecord record) => record.Fields.GetStringOrEmpty("Source");
    public static string Notes(this AirTableRecord record) => record.Fields.GetStringOrEmpty("Notes");
    public static string TargetCategory(this AirTableRecord record) => record.Fields.GetStringOrEmpty("TargetCategory");
    public static string NewUrl(this AirTableRecord record) => record.Fields.GetStringOrEmpty("NewURL");
    public static string NewDescription(this AirTableRecord record) => record.Fields.GetStringOrEmpty("NewDescription");

    private static string GetStringOrEmpty(this Dictionary<string, object> dict, string key)
    {
        if (!dict.ContainsKey(key)) return string.Empty;
        var value = dict[key];
        return value?.ToString() ?? string.Empty;
    }

    private static bool IsYouTubeLink(this AirTableRecord record)
    {
        var newUrl = record.NewUrl();

        if (string.IsNullOrWhiteSpace(newUrl))
        {
            newUrl = record.Url();
        }

        return newUrl.StartsWith("https://www.youtube.com") || newUrl.StartsWith("https://youtu.be");
    }

    private static string GetYouTubeThumbnail(this AirTableRecord record)
    {
        var youtubeUrl = record.GetUrl();
        var youtubeId = youtubeUrl.GetYoutubeClipId();

        return $"https://img.youtube.com/vi/{youtubeId}/mqdefault.jpg";
    }

    public static string GetTitle(this AirTableRecord record) 
        => !string.IsNullOrWhiteSpace(record.NewDescription()) ? record.NewDescription() : record.Name();

    public static string GetUrl(this AirTableRecord record)
        => !string.IsNullOrWhiteSpace(record.NewUrl()) ? record.NewUrl() : record.Url();

    public static string GetImage(this AirTableRecord record)
        => record.IsYouTubeLink() ? record.GetYouTubeThumbnail() : string.Empty;
}
