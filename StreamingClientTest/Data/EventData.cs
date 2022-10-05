using System;

namespace StreamingClientTest.Data
{
    public class EventData
    {
        public long Timepoint { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Type { get; set; }

        public string ResourceUri { get; set; }

        public string Data { get; set; }
        public string ResourceKind { get; set; }

    }
}
