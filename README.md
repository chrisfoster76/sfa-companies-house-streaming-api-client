# sfa-companies-house-streaming-api-client
Test client for Companies House Streaming API

1. Clone this repo
2. Publish DB Project to local server
3. Add app.config to the root, contents as below

```
{
    "AppConfig": {
        "DatabaseConnectionString": "<db connection string>",
        "StreamingApiUser": "<companies house api username>"
    } 
}
```