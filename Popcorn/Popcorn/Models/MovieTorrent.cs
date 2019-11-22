using Newtonsoft.Json;

namespace PopcornTime.Model
{
    public class MovieTorrent
    {
        [JsonProperty(propertyName: "en")]
        public En En { get; set; }
    }

    public class En
    {
        [JsonProperty(propertyName: "1080p")]
        public P1080Movie P1080 { get; set; }
        [JsonProperty(propertyName: "720p")]
        public P720Movie P720 { get; set; }
        [JsonProperty(propertyName: "480p")]
        public P720Movie P480 { get; set; }
    }

    public class P1080Movie
    {
        [JsonProperty(propertyName: "provider")]
        public string Provider { get; set; }
        [JsonProperty(propertyName: "filesize")]
        public string Filesize { get; set; }
        [JsonProperty(propertyName: "size")]
        public long Size { get; set; }
        [JsonProperty(propertyName: "peer")]
        public int Peer { get; set; }
        [JsonProperty(propertyName: "seed")]
        public int Seed { get; set; }
        [JsonProperty(propertyName: "url")]
        public string Url { get; set; }
    }

    public class P720Movie
    {
        [JsonProperty(propertyName: "provider")]
        public string Provider { get; set; }
        [JsonProperty(propertyName: "filesize")]
        public string Filesize { get; set; }
        [JsonProperty(propertyName: "size")]
        public long Size { get; set; }
        [JsonProperty(propertyName: "peer")]
        public int Peer { get; set; }
        [JsonProperty(propertyName: "seed")]
        public int Seed { get; set; }
        [JsonProperty(propertyName: "url")]
        public string Url { get; set; }
    }

    public class P480Movie
    {
        [JsonProperty(propertyName: "provider")]
        public string Provider { get; set; }
        [JsonProperty(propertyName: "filesize")]
        public string Filesize { get; set; }
        [JsonProperty(propertyName: "size")]
        public long Size { get; set; }
        [JsonProperty(propertyName: "peer")]
        public int Peer { get; set; }
        [JsonProperty(propertyName: "seed")]
        public int Seed { get; set; }
        [JsonProperty(propertyName: "url")]
        public string Url { get; set; }
    }
}
