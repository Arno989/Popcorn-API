using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PopcornTime.Model
{
    public class EpisodeTorrent
    {
        [JsonProperty(propertyName: "1080p")]
        public P720Episode P1080 { get; set; }
        [JsonProperty(propertyName: "720p")]
        public P720Episode P720 { get; set; }
        [JsonProperty(propertyName: "480p")]
        public P480Episode P480 { get; set; }
    }

    public class P1080Episode
    {
        [JsonProperty(propertyName: "provider")]
        public string Provider { get; set; }
        [JsonProperty(propertyName: "peers")]
        public int Peers { get; set; }
        [JsonProperty(propertyName: "seeds")]
        public int Seeds { get; set; }
        [JsonProperty(propertyName: "url")]
        public string Url { get; set; }
    }
    public class P720Episode
    {
        [JsonProperty(propertyName: "provider")]
        public string Provider { get; set; }
        [JsonProperty(propertyName: "peers")]
        public int Peers { get; set; }
        [JsonProperty(propertyName: "seeds")]
        public int Seeds { get; set; }
        [JsonProperty(propertyName: "url")]
        public string Url { get; set; }
    }
    public class P480Episode
    {
        [JsonProperty(propertyName: "provider")]
        public string Provider { get; set; }
        [JsonProperty(propertyName: "peers")]
        public int Peers { get; set; }
        [JsonProperty(propertyName: "seeds")]
        public int Seeds { get; set; }
        [JsonProperty(propertyName: "url")]
        public string Url { get; set; }
    }
}
