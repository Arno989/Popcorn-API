using Newtonsoft.Json;
using System.Collections.Generic;

namespace Popcorn.Models
{
    public class Anime
    {
        [JsonProperty(propertyName: "_id")]
        public string Id { get; set; }
        [JsonProperty(propertyName: "mal_id")]
        public string Mal_Id { get; set; }
        [JsonProperty(propertyName: "title")]
        public string Title { get; set; }
        [JsonProperty(propertyName: "year")]
        public string Year { get; set; }
        [JsonProperty(propertyName: "slug")]
        public string Slug { get; set; }
        [JsonProperty(propertyName: "type")]
        public string Type { get; set; }
        [JsonProperty(propertyName: "genres")]
        public List<string> Genres { get; set; }
        [JsonProperty(propertyName: "images")]
        public Thumbnail Images { get; set; }
        [JsonProperty(propertyName: "rating")]
        public Rating Rates { get; set; }
        [JsonProperty(propertyName: "num_seasons")]
        public int Num_Seasons { get; set; }
    }
}
