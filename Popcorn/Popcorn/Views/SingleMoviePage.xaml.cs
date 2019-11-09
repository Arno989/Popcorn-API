using Popcorn.Models;
using Popcorn.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Popcorn.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SingleMoviePage : ContentPage
    {
        public SingleMoviePage(string ImdbId)
        {
            InitializeComponent();
            Test(ImdbId);
        }

        private async Task Test(string ImdbId)
        {
            Movie movie = await PopcornRepository.GetSingleMovieAsync(ImdbId);
            title.Text = movie.Title;
            year.Text = movie.Year;
            synopsis.Text = movie.Synopsis;
            runtime.Text = movie.Runtime;
            trailer.Text = movie.Trailer;
        }
    }
}