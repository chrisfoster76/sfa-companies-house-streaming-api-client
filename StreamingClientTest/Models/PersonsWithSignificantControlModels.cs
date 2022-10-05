using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StreamingClientTest.Models
{
    public class EventData
    {
        [JsonPropertyName("fields_changed")]
        public List<string> FieldsChanged { get; set; }

        [JsonPropertyName("timepoint")]
        public long Timepoint { get; set; }
        [JsonPropertyName("published_at")]
        public DateTime PublishedAt { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class StreamingApiEvent
    {
        [JsonPropertyName("resource_kind")]
        public string ResourceKind { get; set; }
        
        [JsonPropertyName("resource_uri")]
        public string ResourceUri  { get; set; }
        
        [JsonPropertyName("resource_id")]
        public string ResourceId { get; set; }

        [JsonPropertyName("event")]
        public EventData EventData { get; set; }

        [JsonPropertyName("data")]
        public JsonElement Data { get; set; }
    }


}
