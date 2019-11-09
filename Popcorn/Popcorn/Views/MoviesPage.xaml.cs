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
            InitializeComponent();
            ShowMovies();
            //Test();
        }

        private async Task ShowMovies()
        {
            lvwTrending.ItemsSource = await PopcornRepository.GetMoviesAsync("", "trending");
            //lvwPopularity.ItemsSource = await PopcornRepository.GetMoviesAsync("", "popularity");
            //lvwLastAdded.ItemsSource = await PopcornRepository.GetMoviesAsync("", "last added");
            //lvwYear.ItemsSource = await PopcornRepository.GetMoviesAsync("", "year");
            //lvwTitle.ItemsSource = await PopcornRepository.GetMoviesAsync("", "title");
            //lvwRating.ItemsSource = await PopcornRepository.GetMoviesAsync("", "rating");
        }

        //bool hasAppearedOnce = false;
        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    if (!hasAppearedOnce)
        //    {

        //        hasAppearedOnce = true;
        //        var padding = (1440 - lvwCards.Height) / 2;

        //        lvwCards.HeightRequest = MessagesLayoutFrame.Width;
        //        MessagesLayoutFrameInner.WidthRequest = MessagesLayoutFrame.Width;
        //        MessagesLayoutFrameInner.Padding = new Thickness(0);
        //        MessagesLayoutFrame.Padding = new Thickness(0);
        //        MessagesLayoutFrame.IsClippedToBounds = true;
        //        Xamarin.Forms.AbsoluteLayout.SetLayoutBounds(MessagesLayoutFrameInner, new Rectangle(0, 0 - padding, AbsoluteLayout.AutoSize, lvwCards.Height - padding));
        //        MessagesLayoutFrameInner.IsClippedToBounds = true;
        //        
        //    }
        //}

        private async Task Test()
        {
            List<Movie> Movies = new List<Movie>();
            List<Series> Series = new List<Series>();
            List<Anime> Animes = new List<Anime>();

            Movies = await PopcornRepository.GetMoviesAsync("", "trending");
            Movie SingleMovie = await PopcornRepository.GetSingleMovieAsync("tt1431045");
            Series = await PopcornRepository.GetTrendingSeriesAsync("");
            Series SingleSeries = await PopcornRepository.GetSingleSeriesAsync("tt0898266");
            Animes = await PopcornRepository.GetTrendingAnimeAsync("");
            Anime SingleAnime = await PopcornRepository.GetSingleAnimeAsync("11844");
        }

    }
}
