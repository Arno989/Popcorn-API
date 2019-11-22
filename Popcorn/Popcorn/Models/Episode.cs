using Newtonsoft.Json;
using PopcornTime.Model;
using System;
using Xamarin.Forms;

namespace Popcorn.Models
{
    public class Episode
    {
        [JsonProperty(propertyName: "torrents")]
        public EpisodeTorrent Torrents { get; set; }

        [JsonProperty(propertyName: "first_aired")]
        public double AirDate { get; set; }
        [JsonProperty(propertyName: "overview")]
        public String Synopsis { get; set; }
        [JsonProperty(propertyName: "title")]
        public String Title { get; set; }
        [JsonProperty(propertyName: "episode")]
        public int EpisodeNr { get; set; }
        [JsonProperty(propertyName: "season")]
        public int SeasonNr { get; set; }
        [JsonProperty(propertyName: "tvbd_id")]
        public int tvdb_id { get; set; }

        public ImageSource Poster { get; set; }
    }
}
