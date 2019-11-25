using Popcorn.Models;
using Popcorn.Repositories;
using PopcornTime.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Popcorn.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EpisodesPage : ContentPage
    {
        #region Init
        List<Episode> Context = new List<Episode>();
        public EpisodesPage(List<Episode> context, int season, ImageSource poster)
        {
            InitializeComponent();
            imgLeft.Source = ImageSource.FromResource($"Popcorn.Assets.baseline_keyboard_arrow_left_black_48.png");
            imgRight.Source = ImageSource.FromResource($"Popcorn.Assets.baseline_keyboard_arrow_right_black_48.png");
            Context = context;
            cvContent.ItemsSource = context;
            ShowContent(context, season, poster);
            cvContent.Position = context.IndexOf(context.Where(p => p.SeasonNr == season).FirstOrDefault());
        }
        #endregion

        #region Show data
        private void ShowContent(List<Episode> context, int season, ImageSource poster)
        {
            foreach (Episode e in context)
            {
                //if (e.SeasonNr == season)
                e.Poster = poster;
            }
            cvContent.ItemsSource = context;
        }

        private async Task ShowActionSheet()
        {
            EpisodeTorrent t = ((Episode)cvContent.CurrentItem).Torrents;

            string s1080 = t.P1080 != null ? $"1080p - {t.P1080.Seeds} seeders" : "";
            string s720 = t.P720 != null ? $"720p - {t.P720.Seeds} seeders" : "";
            string s480 = t.P480 != null ? $"480p - {t.P480.Seeds} seeders" : "";

            string action = await DisplayActionSheet("Download Torrent", "Cancel", null, (t.P1080 != null ? s1080 : "1080p (unavailable)"), (t.P720 != null ? s720 : "720p (unavailable)"), (t.P480 != null ? s480 : "480p (unavailable)"));

            if (action == s1080)
            {
                Device.OpenUri(new Uri(Convert.ToString(t.P1080.Url)));
            }
            else if (action == s720)
            {
                Device.OpenUri(new Uri(Convert.ToString(t.P720.Url)));
            }
            else if (action == s480)
            {
                Device.OpenUri(new Uri(Convert.ToString(t.P480.Url)));
            }
        }
        #endregion

        #region Event handlers
        private void cvContent_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            Episode ep = e.CurrentItem as Episode;
            lblTitle.Text = ep.Title;
            lblEpisode.Text = "S" + ep.SeasonNr + "E" + ep.EpisodeNr;
            lblSynopsis.Text = ep.Synopsis;
            lblAirdate.Text = "First Aired on " + (PopcornRepository.UnixTimeStampToDateTime(ep.AirDate)).ToString("dddd, dd MMMM yyyy");

            if (cvContent.Position == 0)
                imgLeft.IsVisible = false;
            else
                imgLeft.IsVisible = true;

            if (cvContent.Position == Context.Count)
                imgRight.IsVisible = false;
            else
                imgRight.IsVisible = true;
        }

        private void LeftTapped(object sender, EventArgs e)
        {
            if (cvContent.Position != 0)
                cvContent.Position = cvContent.Position -= 1;
        }

        private void RightTapped(object sender, EventArgs e)
        {
            if (cvContent.Position != 50)
                cvContent.Position = cvContent.Position += 1;
        }

        private void btnDownload_Clicked(object sender, EventArgs e)
        {
            ShowActionSheet();
        }
        #endregion
    }
}