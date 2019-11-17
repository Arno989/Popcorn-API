using Android.App;
using Popcorn.Models;
using Popcorn.Repositories;
using PopcornTime.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Popcorn.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EpisodesPage : ContentPage
    {
        public EpisodesPage(Series context, int season)
        {
            InitializeComponent();
            ShowContent(context, season);
        }

        private void ShowContent(Series context, int season)
        {
            List<Episode> eps = new List<Episode>();
            foreach (Episode e in context.Episodes)
            {
                e.Poster = context.Images.Poster;
                eps.Add(e);
            }
            cvContent.ItemsSource = eps;
        }

        private void cvContent_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            Episode ep = e.CurrentItem as Episode;
            lblTitle.Text = ep.Title;
            lblEpisode.Text = "S" + ep.SeasonNr + "E" + ep.EpisodeNr;
            lblSynopsis.Text = ep.Synopsis;
            lblAirdate.Text = "First Aired on " + (PopcornRepository.UnixTimeStampToDateTime(ep.AirDate)).ToString("dddd, dd MMMM yyyy");
        }

        private void btnDownload_Clicked(object sender, EventArgs e)
        {
            ActionSheet();
        }

        private async Task ActionSheet()
        {
            EpisodeTorrent t = ((Episode)cvContent.CurrentItem).Torrents;
            
            string action = await DisplayActionSheet("Download Torrent", "Cancel", null, (t.P1080 != null ? "1080p" : "1080p (unavailable)"),(t.P720 != null ? "720p" : "720p (unavailable)"), (t.P480 != null ? "480p" : "480p (unavailable)"));
            switch (action)
            {
                case "1080p":
                    Device.OpenUri(new Uri(Convert.ToString(t.P1080.Url)));
                    break;
                case "720p":
                    Device.OpenUri(new Uri(Convert.ToString(t.P720.Url)));
                    break;
                case "480p":
                    Device.OpenUri(new Uri(Convert.ToString(t.P480.Url)));
                    break;
                default:
                    break;
            }
        }
    }
}