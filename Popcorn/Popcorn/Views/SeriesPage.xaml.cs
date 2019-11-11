﻿using Popcorn.Models;
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
            foreach (String s in PopcornRepository.MOVIESORTLIST)
            {
                pickSort.Items.Add(s);
            }
        }

        private async Task ShowContent(String sort)
        {
            lvwContent.ItemsSource = await PopcornRepository.GetSeriesAsync("", sort);
        }

        private void pickSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowContent(pickSort.SelectedItem.ToString());
        }

    }
}
