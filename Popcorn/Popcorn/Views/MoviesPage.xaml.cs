using Popcorn.Models;
using Popcorn.Repositories;
using PopcornTime.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.SfChart.XForms;

namespace Popcorn.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoviesPage : ContentPage
    {
        List<Movie> Context = new List<Movie>();
        int index = 0;

        public MoviesPage()
        {
            InitializeComponent();
            Init();

            imgLeft.Source = ImageSource.FromResource($"Popcorn.Assets.baseline_keyboard_arrow_left_black_48.png");
            imgRight.Source = ImageSource.FromResource($"Popcorn.Assets.baseline_keyboard_arrow_right_black_48.png");
            
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
            Context = await PopcornRepository.GetMoviesAsync(search, sort, genre);
            if (Context.Count != 0)
            {
                gImages.IsVisible = true;
                gContent.IsVisible = true;
                gButtons.IsVisible = true;
                gEmpty.IsVisible = false;

                cvContent.ItemsSource = Context;
                cvContent.CurrentItem = Context[0];
                index = 0;
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
            if (eSearch.Text == null || eSearch.Text == "")
            {
                ShowContent(pickSort.SelectedItem.ToString(), "", (pickGenre.SelectedItem != null ? pickGenre.SelectedItem.ToString() : "All"));
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
            Navigation.PushAsync(new DetailsPage((Movie)cvContent.CurrentItem));
        }

        private void cvTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DetailsPage((Movie)cvContent.CurrentItem));
        }

        private void LeftTapped(object sender, EventArgs e)
        {

        }

        private void RightTapped(object sender, EventArgs e)
        {

        }
    }
}
