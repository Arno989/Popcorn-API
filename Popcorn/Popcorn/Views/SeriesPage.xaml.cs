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
    public partial class SeriesPage : ContentPage
    {
        #region Inits
        List<Series> Context = new List<Series>();
        Series se;
        public SeriesPage()
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
            foreach (String s in PopcornRepository.SERIESORTLIST)
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
            Context = await PopcornRepository.GetSeriesAsync(search, sort, genre);
            if (Context.Count != 0)
            {
                gImages.IsVisible = true;
                gContent.IsVisible = true;
                gButtons.IsVisible = true;
                gEmpty.IsVisible = false;

                cvContent.ItemsSource = Context;
                cvContent.Position = 0;
                ShowDetails(((Series)cvContent.CurrentItem).Imdb_Id);
            }
            else
            {
                gImages.IsVisible = false;
                gContent.IsVisible = false;
                gButtons.IsVisible = false;
                gEmpty.IsVisible = true;
            }
        }

        private async Task ShowDetails(String id)
        {
            se = await PopcornRepository.GetSingleSeriesAsync(id);
            lblTitle.Text = se.Title;
            lblYear.Text = "First aired in " + se.Year;
            lblSynopsis.Text = se.Synopsis;
            lblRuntime.Text = "Avg Duration: " + se.Runtime + " minutes";
            lblStatus.Text = PopcornRepository.FirstCharToUpper(se.Status);
            btnSeasons.Text = (se.Seasons > 1 ? "View all " + se.Seasons + " available seasons" : "View available episodes");
            switch (se.Status)
            {
                case "returning series":
                    lblAirtime.Text = "Plays on " + se.AirDay + " at " + se.AirTime + " in " + se.Country.ToUpper();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Event handlers
        #region Top
        private void pickGenre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eSearch.Text == null)
            {
                ShowContent((pickSort.SelectedItem != null ? pickSort.SelectedItem.ToString() : "Trending"), "", pickGenre.SelectedItem.ToString());
            }
        }

        private void pickSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eSearch.Text == null)
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
            ShowDetails(((Series)cvContent.CurrentItem).Imdb_Id);

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
            Navigation.PushAsync(new SeriesDetailsPage((Series)cvContent.CurrentItem));
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
        #endregion

        #region Bottom
        private void btnSeasons_Clicked(object sender, EventArgs e)
        {
            if (se.Seasons > 1)
                Navigation.PushAsync(new SeasonsPage(se));
            else
                Navigation.PushAsync(new EpisodesPage(se.Episodes, 1, se.Images.Poster));
        }

        private void btnDetails_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SeriesDetailsPage((Series)cvContent.CurrentItem));
        }
        #endregion
        #endregion
    }
}
