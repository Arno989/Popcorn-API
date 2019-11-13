using Popcorn.Models;
using Popcorn.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        Movie Context;
        public SingleMoviePage(Movie pContext)
        {
            InitializeComponent();
            Context = pContext;
            Test(Context.ImdbId);
        }

        private async Task Test(string ImdbId)
        {
            Movie movie = await PopcornRepository.GetSingleMovieAsync(ImdbId);
            title.Text = movie.Title;
            year.Text = movie.Year;
            synopsis.Text = movie.Synopsis;
            runtime.Text = movie.Runtime;
        }

        private void btnTrailer_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri(Context.Trailer));
        }
    }
}