using Popcorn.Models;
using Popcorn.Repositories;
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
        #region Init
        List<Movie> Context = new List<Movie>();
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
        #endregion

        #region Show data
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
                cvContent.Position = 0;
                ShowDetails((Movie)cvContent.CurrentItem);
            }
            else
            {
                gImages.IsVisible = false;
                gContent.IsVisible = false;
                gButtons.IsVisible = false;
                gEmpty.IsVisible = true;
            }
        }

        private async Task ShowDetails(Movie m)
        {
            lblTitle.Text = m.Title;
            lblYear.Text = "Released in " + m.Year;
            lblSynopsis.Text = m.Synopsis;
            lblRuntime.Text = "Duration: " + m.Runtime + " minutes";
        }
        #endregion

        #region Event handlers
        #region Top
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
        #endregion

        #region Mid
        private void cvContent_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            ShowDetails((Movie)cvContent.CurrentItem);

            if (cvContent.Position == 0)
                imgLeft.IsVisible = false;
            else
                imgLeft.IsVisible = true;

            if (cvContent.Position == Context.Count)
                imgRight.IsVisible = false;
            else
                imgRight.IsVisible = true;
        }

        private void cvTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MoviesDetailsPage((Movie)cvContent.CurrentItem));
        }

        private void LeftTapped(object sender, EventArgs e)
        {
            if (cvContent.Position != 0)
                cvContent.Position = cvContent.Position -= 1;
            //cvContent.ScrollTo();
        }

        private void RightTapped(object sender, EventArgs e)
        {
            if (cvContent.Position != 50)
                cvContent.Position = cvContent.Position += 1;
        }
        #endregion

        #region Bottom
        private void btnTrailer_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri(((Movie)cvContent.CurrentItem).Trailer)); //Launcher.CanOpenAsync
        }

        private void btnDetails_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MoviesDetailsPage((Movie)cvContent.CurrentItem));
        }
        #endregion
        #endregion
    }
}
