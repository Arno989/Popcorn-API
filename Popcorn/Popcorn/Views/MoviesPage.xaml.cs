using Popcorn.Models;
using Popcorn.Repositories;
using PopcornTime.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            pickGenre.SelectedItem = "All";
        }

        private void Init()
        {
            foreach (String s in PopcornRepository.MOVIESORTLIST)
            {
                pickSort.Items.Add(s);
            }

            foreach (String s in PopcornRepository.MOVIEGENRELIST)
            {
                pickGenre.Items.Add(s);
            }
        }

        private async Task ShowContent(String sort, String search, String genre)
        {
            List<Movie> content = await PopcornRepository.GetMoviesAsync(search, sort, genre);
            if (content.Count != 0)
            {
                gImages.IsVisible = true;
                gContent.IsVisible = true;
                gButtons.IsVisible = true;
                gEmpty.IsVisible = false;

                cvContent.ItemsSource = content;
                cvContent.CurrentItem = content[0];
            }
            else
            {
                gImages.IsVisible = false;
                gContent.IsVisible = false;
                gButtons.IsVisible = false;
                gEmpty.IsVisible = true;
            }
        }
        
        private void pickGenre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eSearch.Text == null || eSearch.Text == "")
            {
                ShowContent((pickSort.SelectedItem != null ? pickSort.SelectedItem.ToString() : "Trending"), "", pickGenre.SelectedItem.ToString());
            }
        }

        private void pickSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(eSearch.Text == null || eSearch.Text == "")
            {
                ShowContent(pickSort.SelectedItem.ToString(), "", (pickGenre.SelectedItem != null? pickGenre.SelectedItem.ToString():"All"));
            }
        }

        private void btnSearch_Clicked(object sender, EventArgs e)
        {
            ShowContent(pickSort.SelectedItem.ToString(), eSearch.Text, pickGenre.SelectedItem.ToString());
        }

        private void cvContent_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            Movie m = (Movie)e.CurrentItem;
            lblTitle.Text = m.Title;
            lblYear.Text = "Released in " + m.Year;
            lblSynopsis.Text = m.Synopsis;
            lblRuntime.Text = "Duration: " + m.Runtime + " minutes";
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

        private void eSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string currentText = e.NewTextValue;

            string lastText = "";

            if (!String.IsNullOrEmpty(e.OldTextValue))
            {
                lastText = e.OldTextValue;
            }

            var currentNumb = currentText.Length - currentText.Replace(Environment.NewLine, string.Empty).Length;
            var lastNumb = lastText.Length - lastText.Replace(Environment.NewLine, string.Empty).Length;


            if (currentNumb > lastNumb)
            {
                eSearch.Text = lastText;
                eSearch.Unfocus();
            }
        }
    }
}
