using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xamarin.Forms;

namespace Popcorn.Models
{
    public class Thumbnail
    {
        [JsonProperty(propertyName: "poster")] //= ImageSource.FromUri(new Uri("Popcorn.Assets.PopcornTimeDefaultImage.png"));
        public ImageSource Poster { get; set; }// = ImageSource.FromUri(new Uri("Popcorn.Assets.PopcornTimeDefaultImage.png"));
        [JsonProperty(propertyName: "fanart")]
        public ImageSource FanArt { get; set; }// = ImageSource.FromUri(new Uri("Popcorn.Assets.PopcornTimeDefaultImage.png"));
        [JsonProperty(propertyName: "banner")]
        public ImageSource Banner { get; set; }// = ImageSource.FromUri(new Uri("Popcorn.Assets.PopcornTimeDefaultImage.png"));
    }
}
