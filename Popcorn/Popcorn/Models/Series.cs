using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Popcorn.Models
{
    public class Series
    {
        [JsonProperty(propertyName: "_id")]
        public string Id { get; set; }
        [JsonProperty(propertyName: "imdb_id")]
        public string Imdb_Id { get; set; }
        [JsonProperty(propertyName: "title")]
        public string Title { get; set; }
        [JsonProperty(propertyName: "year")]
        public string Year { get; set; }
        [JsonProperty(propertyName: "num_seasons")]
        public int Seasons { get; set; }
        [JsonProperty(propertyName: "rating")]
        public Rating Rates { get; set; }
        [JsonProperty(propertyName: "images")]
        public Thumbnail Images { get; set; }
        // bijkomende info
        [JsonProperty(propertyName: "genres")]
        public List<string> Genres { get; set; }
        [JsonProperty(propertyName: "synopsis")]
        public string Synopsis { get; set; }
        [JsonProperty(propertyName: "runtime")]
        public string Runtime { get; set; }
        [JsonProperty(propertyName: "status")]
        public string Status { get; set; }
        [JsonProperty(propertyName: "air_time")]
        public string AirTime { get; set; }
        [JsonProperty(propertyName: "air_day")]
        public string AirDay { get; set; }
        [JsonProperty(propertyName: "country")]
        public string Country { get; set; }
        [JsonProperty(propertyName: "episodes")]
        public List<Episode> Episodes { get; set; }
    }
}
