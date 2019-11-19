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
        Series se;
        public SeriesPage()
        {
            InitializeComponent();
            Init();
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

        private async Task ShowContent(String sort, String search, String genre)
        {
            List<Series> content = await PopcornRepository.GetSeriesAsync(search, sort, genre);
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

        private void cvContent_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            ShowDetails(((Series)e.CurrentItem).Imdb_Id);
        }

        private async Task ShowDetails(String id)
        {
            se = await PopcornRepository.GetSingleSeriesAsync(id);
            lblTitle.Text = se.Title;
            lblYear.Text = "First aired in " + se.Year;
            lblSynopsis.Text = se.Synopsis;
            lblRuntime.Text = "Avg Duration: " + se.Runtime + " minutes";
            lblStatus.Text = PopcornRepository.FirstCharToUpper(se.Status);
            btnSeasons.Text = (se.Seasons > 1 ? "View all " + se.Seasons + " seasons" : "View episodes");
            switch (se.Status)
            {
                case "returning series":
                    lblAirtime.Text = "Plays on " + se.AirDay + " at " + se.AirTime + " in " + se.Country.ToUpper();
                    break;
                default:
                    break;
            }
        }

        private void btnSeasons_Clicked(object sender, EventArgs e)
        {
            if(se.Seasons > 1)
                Navigation.PushAsync(new SeasonsPage(se));
            else
                Navigation.PushAsync(new EpisodesPage(se , 1));
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
