using DLToolkit.Forms.Controls;
using Popcorn.Models;
using Popcorn.Repositories;
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
            cvContent.CurrentItem = cvContent[0];
        }

        private void pickSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(eSearch.Text == null)
            {
                ShowContent(pickSort.SelectedItem.ToString(), "");
            }
        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            ViewCell v = sender as ViewCell;
            v.ForceUpdateSize();
        }

        private void cvContent_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            lblTitle.Text = ((Movie)e.CurrentItem).Title;
            lblYear.Text = ((Movie)e.CurrentItem).Year;
            lblSynopsis.Text = ((Movie)e.CurrentItem).Synopsis;
            lblRuntime.Text = ((Movie)e.CurrentItem).Runtime;
        }

        private void btnTrailer_Clicked(object sender, EventArgs e)
        {
            Movie context = cvContent.CurrentItem as Movie;
            Device.OpenUri(new Uri(context.Trailer)); //Launcher.CanOpenAsync
        }

        private void btnSearch_Clicked(object sender, EventArgs e)
        {
            ShowContent(pickSort.SelectedItem.ToString(), eSearch.Text);
        }
    }
}
