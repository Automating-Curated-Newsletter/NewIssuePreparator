using System;

namespace NewIssuePreparator
{
    public class EnvironmentConfig
    {
        public AirtableConfig Airtable { get; }
        public CuratedConfig Curated { get; }

        public EnvironmentConfig()
        {
            Airtable = new AirtableConfig();
            Curated = new CuratedConfig();
        }

        public class AirtableConfig
        {
            public string ApiAccessToken { get; }
            public string BaseId { get; }
            public string TableName { get; }
            public string ApiUrl { get; }

            public AirtableConfig()
            {
                ApiAccessToken = Environment.GetEnvironmentVariable("AIRTABLE_API_ACCESS_TOKEN") ?? 
                    throw new ArgumentNullException("AIRTABLE_API_ACCESS_TOKEN is not set in environment variables");
                BaseId = Environment.GetEnvironmentVariable("AIRTABLE_BASE_ID") ?? 
                    throw new ArgumentNullException("AIRTABLE_BASE_ID is not set in environment variables");
                TableName = Environment.GetEnvironmentVariable("AIRTABLE_TABLE_NAME") ?? 
                    throw new ArgumentNullException("AIRTABLE_TABLE_NAME is not set in environment variables");
                ApiUrl = Environment.GetEnvironmentVariable("AIRTABLE_API_URL") ?? 
                    throw new ArgumentNullException("AIRTABLE_API_URL is not set in environment variables");
            }
        }

        public class CuratedConfig
        {
            public string ApiToken { get; }
            public string ApiUrl { get; }
            public string PublicationId { get; }

            public CuratedConfig()
            {
                ApiToken = Environment.GetEnvironmentVariable("CURATED_API_TOKEN") ?? 
                    throw new ArgumentNullException("CURATED_API_TOKEN is not set in environment variables");
                ApiUrl = Environment.GetEnvironmentVariable("CURATED_API_URL") ?? 
                    throw new ArgumentNullException("CURATED_API_URL is not set in environment variables");
                PublicationId = Environment.GetEnvironmentVariable("CURATED_PUBLICATION_ID") ?? 
                    throw new ArgumentNullException("CURATED_PUBLICATION_ID is not set in environment variables");
            }
        }
    }
} 