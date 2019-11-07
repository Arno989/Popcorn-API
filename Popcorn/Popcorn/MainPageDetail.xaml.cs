using Popcorn.Models;
using Popcorn.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Popcorn
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageDetail : ContentPage
    {
        public MainPageDetail()
        {
            InitializeComponent();
            ShowTrendingMovies();
            //Test();
        }

        private async Task ShowTrendingMovies()
        {
            




            List<Movie> Movies = await PopcornRepository.GetTrendingMoviesAsync("");
            lvwCards.ItemsSource = Movies;
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
        //        // */
        //    }
        //}

        private async Task Test()
        {
            List<Movie> Movies = new List<Movie>();
            List<Series> Series = new List<Series>();
            List<Anime> Animes = new List<Anime>();

            Movies = await PopcornRepository.GetTrendingMoviesAsync("");
            Movie SingleMovie = await PopcornRepository.GetSingleMovieAsync("tt1431045");
            Series = await PopcornRepository.GetTrendingSeriesAsync("");
            Series SingleSeries = await PopcornRepository.GetSingleSeriesAsync("tt0898266");
            Animes = await PopcornRepository.GetTrendingAnimeAsync("");
            Anime SingleAnime = await PopcornRepository.GetSingleAnimeAsync("11844");
        }
        
    }
}