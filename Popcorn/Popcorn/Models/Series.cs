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
        [JsonProperty(propertyName: "tvdb_id")]
        public string Tvdb_Id { get; set; }
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
    }
}
