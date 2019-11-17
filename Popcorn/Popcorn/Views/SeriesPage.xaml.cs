using Popcorn.Models;
using Popcorn.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
        }

        private void Init()
        {
            foreach (String s in PopcornRepository.SERIESORTLIST)
            {
                pickSort.Items.Add(s);
            }
        }

        private async Task ShowContent(String sort, String search)
        {
            List<Series> content = await PopcornRepository.GetSeriesAsync(search, sort);
            cvContent.ItemsSource = content;
            cvContent.CurrentItem = content[0];
        }

        private async Task ShowDetails(String id)
        {
            se = await PopcornRepository.GetSingleSeriesAsync(id);
            lblTitle.Text = se.Title;
            lblYear.Text = "First aired in " + se.Year;
            lblSynopsis.Text = se.Synopsis;
            lblRuntime.Text = "Avg Duration: " + se.Runtime + " minutes";
            lblStatus.Text = PopcornRepository.FirstCharToUpper(se.Status);
            btnSeasons.Text = (se.Seasons > 1? "View all " + se.Seasons + " seasons" : "View episodes");
            switch (se.Status)
            {
                case "returning series":
                    lblAirtime.Text = "Plays on " + se.AirDay + " at " + se.AirTime + " in " + se.Country.ToUpper();
                    break;
                default:
                    break;
            }
        }

        private void pickSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eSearch.Text == null)
            {
                ShowContent(pickSort.SelectedItem.ToString(), "");
            }
        }

        private void cvContent_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            ShowDetails(((Series)e.CurrentItem).Imdb_Id);
        }

        private void btnSearch_Clicked(object sender, EventArgs e)
        {
            ShowContent(pickSort.SelectedItem.ToString(), eSearch.Text);
        }

        private void btnSeasons_Clicked(object sender, EventArgs e)
        {
            if(se.Seasons > 1)
                Navigation.PushAsync(new SeasonsPage(se));
            else
                Navigation.PushAsync(new EpisodesPage(se , 1));
        }
    }
}
