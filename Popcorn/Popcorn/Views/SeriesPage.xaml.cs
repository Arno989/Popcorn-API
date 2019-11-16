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
            Series s = await PopcornRepository.GetSingleSeriesAsync(id);
            lblYear.Text = s.Year;
            lblSynopsis.Text = s.Synopsis;
            lblRuntime.Text = s.Runtime;
            lblStatus.Text = s.Status;
            lblAirtime.Text = s.AirDay + " " + s.AirTime + " in " + s.Country;

            // year
            //synopsis
            // runtime in mins
            //status
            //     if "returning series" air day + time
            //last updated
            //genres list
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
            lblTitle.Text = ((Series)e.CurrentItem).Title;
            ShowDetails(((Series)e.CurrentItem).Imdb_Id);
        }

        private void btnSearch_Clicked(object sender, EventArgs e)
        {
            ShowContent(pickSort.SelectedItem.ToString(), eSearch.Text);
        }

        private void btnSeasons_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SeasonsPage((Series)cvContent.CurrentItem));
        }
    }
}
