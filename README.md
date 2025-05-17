# NewIssuePreparator

A C# console application that integrates with Airtable and Curated services. This application is designed to be run both locally and as part of a GitHub Actions workflow.

## Project Structure

The solution is organized as follows:
- `/NewIssuePreparator` - Contains the main console application

## Required Configuration

### For Local Development
Create a `.env` file in the root directory based on `.env.example` with the following variables:

```env
# Airtable Configuration
AIRTABLE_API_ACCESS_TOKEN=your_token_here
AIRTABLE_BASE_ID=your_base_id
AIRTABLE_TABLE_NAME=your_table_name
AIRTABLE_API_URL=your_api_url
AIRTABLE_VIEW_ID=your_view_id

# Curated Configuration
CURATED_API_TOKEN=your_token_here
CURATED_API_URL=your_api_url
CURATED_PUBLICATION_ID=your_publication_id
```

### For GitHub Actions
Set up the following secrets in your GitHub repository (Settings > Secrets and variables > Actions):

Required Secrets:
- `AIRTABLE_API_ACCESS_TOKEN`: Your Airtable API access token
- `AIRTABLE_BASE_ID`: Your Airtable base ID
- `AIRTABLE_TABLE_NAME`: Your Airtable table name
- `AIRTABLE_API_URL`: Your Airtable API URL
- `AIRTABLE_VIEW_ID`: Your Airtable view ID
- `CURATED_API_TOKEN`: Your Curated API token
- `CURATED_API_URL`: Your Curated API URL
- `CURATED_PUBLICATION_ID`: Your Curated publication ID

## How it works

The application can be run:
1. Locally using the `.env` file for configuration
2. Through GitHub Actions workflow using repository secrets

## Requirements

- .NET 9.0
- GitHub Actions (for automated builds and runs)
- Valid API credentials for both Airtable and Curated services

## Security Note

Never commit your `.env` file or actual API credentials to the repository. Always use:
- `.env` file locally (added to .gitignore)
- GitHub Secrets for GitHub Actions workflows
