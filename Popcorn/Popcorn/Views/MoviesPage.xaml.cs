using DLToolkit.Forms.Controls;
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
    public partial class MoviesPage : ContentPage
    {
        public MoviesPage()
        {
            //SetFlags("CollectionView_Experimental");
            InitializeComponent();
            FlowListView.Init();
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
        

        private async Task ShowContent(String sort)
        {
            cvContent.ItemsSource = await PopcornRepository.GetMoviesAsync("", sort);

            //flvwContent.FlowItemsSource = await PopcornRepository.GetMoviesAsync("", sort);
            //flvwContent.HasUnevenRows = true;
            
                

        }

        private void pickSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowContent(pickSort.SelectedItem.ToString());
        }

        

        private void flvwContent_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //if (e.SelectedItem != null)
            //{
            //    Navigation.PushAsync(new SingleMoviePage((Movie)e.SelectedItem));
            //    ((ListView)sender).SelectedItem = null;
            //}
        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            ViewCell v = sender as ViewCell;
            v.ForceUpdateSize();
        }

        private void cvContent_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            lblyeet.Text = ((Movie)e.CurrentItem).Title;
        }
    }
}
