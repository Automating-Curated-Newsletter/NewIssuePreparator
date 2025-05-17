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
            private const string API_ACCESS_TOKEN_KEY = "AIRTABLE_API_ACCESS_TOKEN";
            private const string BASE_ID_KEY = "AIRTABLE_BASE_ID";
            private const string TABLE_NAME_KEY = "AIRTABLE_TABLE_NAME";
            private const string API_URL_KEY = "AIRTABLE_API_URL";
            private const string VIEW_ID_KEY = "AIRTABLE_VIEW_ID";

            public string ApiAccessToken { get; }
            public string BaseId { get; }
            public string TableName { get; }
            public string ApiUrl { get; }
            public string FetchFromView { get; }
            
            public AirtableConfig()
            {
                ApiAccessToken = Environment.GetEnvironmentVariable(API_ACCESS_TOKEN_KEY) ?? 
                    throw new ArgumentNullException($"{API_ACCESS_TOKEN_KEY} is not set in environment variables");
                BaseId = Environment.GetEnvironmentVariable(BASE_ID_KEY) ?? 
                    throw new ArgumentNullException($"{BASE_ID_KEY} is not set in environment variables");
                TableName = Environment.GetEnvironmentVariable(TABLE_NAME_KEY) ?? 
                    throw new ArgumentNullException($"{TABLE_NAME_KEY} is not set in environment variables");
                ApiUrl = Environment.GetEnvironmentVariable(API_URL_KEY) ?? 
                    throw new ArgumentNullException($"{API_URL_KEY} is not set in environment variables");
                FetchFromView = Environment.GetEnvironmentVariable(VIEW_ID_KEY) ?? 
                    throw new ArgumentNullException($"{VIEW_ID_KEY} is not set in environment variables");
            }
        }

        public class CuratedConfig
        {
            private const string API_TOKEN_KEY = "CURATED_API_TOKEN";
            private const string API_URL_KEY = "CURATED_API_URL";
            private const string PUBLICATION_ID_KEY = "CURATED_PUBLICATION_ID";

            public string ApiToken { get; }
            public string ApiUrl { get; }
            public string PublicationId { get; }

            public CuratedConfig()
            {
                ApiToken = Environment.GetEnvironmentVariable(API_TOKEN_KEY) ?? 
                    throw new ArgumentNullException($"{API_TOKEN_KEY} is not set in environment variables");
                ApiUrl = Environment.GetEnvironmentVariable(API_URL_KEY) ?? 
                    throw new ArgumentNullException($"{API_URL_KEY} is not set in environment variables");
                PublicationId = Environment.GetEnvironmentVariable(PUBLICATION_ID_KEY) ?? 
                    throw new ArgumentNullException($"{PUBLICATION_ID_KEY} is not set in environment variables");
            }
        }
    }
} 