using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Popcorn.Models
{
    public class Rating
    {
        [JsonProperty(propertyName: "percentage")]
        public int Percentage { get; set; }
        [JsonProperty(propertyName: "watching")]
        public int Watching { get; set; }
        [JsonProperty(propertyName: "votes")]
        public int Votes { get; set; }
        [JsonProperty(propertyName: "loved")]
        public int Loved { get; set; }
        [JsonProperty(propertyName: "hated")]
        public int Hated { get; set; }
    }

    public class Model
    {
        public string Rating { get; set; }

        public int Score { get; set; }
    }
}
