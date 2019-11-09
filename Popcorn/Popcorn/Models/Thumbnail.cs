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
        private ImageSource _Poster = ImageSource.FromResource("Popcorn.Assets.PopcornTimeDefaultImage.png");
        [JsonProperty(propertyName: "poster")]
        public ImageSource Poster
        {
            get { return _Poster; }
            set { _Poster = value; }
        }

        private ImageSource _FanArt = ImageSource.FromResource("Popcorn.Assets.PopcornTimeDefaultImage.png");
        [JsonProperty(propertyName: "fanart")]
        public ImageSource FanArt
        {
            get { return _FanArt; }
            set { _FanArt = value; }
        }

        private ImageSource _Banner = ImageSource.FromResource("Popcorn.Assets.PopcornTimeDefaultImage.png");
        [JsonProperty(propertyName: "banner")]
        public ImageSource Banner
        {
            get { return _Banner; }
            set { _Banner = value; }
        }

        public bool CheckImage(ImageSource url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url.Parent.ToString());
            request.Method = "HEAD";
            try
            {
                request.GetResponse();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
