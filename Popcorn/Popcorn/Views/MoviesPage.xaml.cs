using DLToolkit.Forms.Controls;
using Popcorn.Models;
using Popcorn.Repositories;
using PopcornTime.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Popcorn.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoviesPage : ContentPage
    {
        public MoviesPage()
        {
            InitializeComponent();
            Init();
            pickSort.SelectedItem = "Trending";
        }

        private void Init()
        {
            foreach (String s in PopcornRepository.MOVIESORTLIST)
            {
                pickSort.Items.Add(s);
            }
        }

        private async Task ShowContent(String sort, String search)
        {
            List<Movie> content = await PopcornRepository.GetMoviesAsync(search, sort);
            cvContent.ItemsSource = content;
            cvContent.CurrentItem = content[0];
        }

        private void pickSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(eSearch.Text == null)
            {
                ShowContent(pickSort.SelectedItem.ToString(), "");
            }
        }

        private void btnSearch_Clicked(object sender, EventArgs e)
        {
            ShowContent(pickSort.SelectedItem.ToString(), eSearch.Text);
        }

        private void cvContent_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            lblTitle.Text = ((Movie)e.CurrentItem).Title;
            lblYear.Text = "Released in " + ((Movie)e.CurrentItem).Year;
            lblSynopsis.Text = ((Movie)e.CurrentItem).Synopsis;
            lblRuntime.Text = "Duration: " + ((Movie)e.CurrentItem).Runtime + " minutes";
        }

        private void btnTrailer_Clicked(object sender, EventArgs e)
        {
            Movie context = cvContent.CurrentItem as Movie;
            Device.OpenUri(new Uri(context.Trailer)); //Launcher.CanOpenAsync
        }

        private void btnDownload_Clicked(object sender, EventArgs e)
        {
            ActionSheet();
        }

        private async Task ActionSheet()
        {
            MovieTorrent t = ((Movie)cvContent.CurrentItem).Torrent;

            string action = await DisplayActionSheet("Download Torrent", "Cancel", null, (t.En.P1080 != null ? "1080p" : "1080p (unavailable)"), (t.En.P720 != null ? "720p" : "720p (unavailable)"), (t.En.P480 != null ? "480p" : "480p (unavailable)"));
            switch (action)
            {
                case "1080p":
                    Device.OpenUri(new Uri(Convert.ToString(t.En.P1080.Url)));
                    break;
                case "720p":
                    Device.OpenUri(new Uri(Convert.ToString(t.En.P720.Url)));
                    break;
                case "480p":
                    Device.OpenUri(new Uri(Convert.ToString(t.En.P480.Url)));
                    break;
                default:
                    break;
            }
        }
    }
}
