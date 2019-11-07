using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Popcorn.Models
{
    public class Movie
    {
        //Movie objects
        [JsonProperty(propertyName: "_id")]
        public string Id { get; set; }
        [JsonProperty(propertyName: "imdb_id")]
        public string ImdbId { get; set; }
        [JsonProperty(propertyName: "title")]
        public string Title { get; set; }
        [JsonProperty(propertyName: "year")]
        public string Year { get; set; }
        [JsonProperty(propertyName: "synopsis")]
        public string Synopsis { get; set; }
        [JsonProperty(propertyName: "runtime")]
        public string Runtime { get; set; }
        [JsonProperty(propertyName: "trailer")]
        public string Trailer { get; set; }
        [JsonProperty(propertyName: "genres")]
        public List<string> Genres { get; set; }

        //step in objects
        //[JsonProperty(propertyName: "torrents")]
        //public MovieTorrent Torrent { get; set; }
        //[JsonProperty(propertyName: "images")]
        //public Thumbnail Images { get; set; }
        //[JsonProperty(propertyName: "rating")]
        //public Rating Rates { get; set; }

        //Conversion from timespan to datetime needed.
        //[JsonProperty(propertyName: "released")]
        // public DateTime Released { get; set; }


    }
}
