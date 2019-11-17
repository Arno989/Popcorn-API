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
    public partial class SeasonsPage : ContentPage
    {
        public SeasonsPage(Series context)
        {
            InitializeComponent();
            ListSeasons(context);
        }

        private async Task ListSeasons(Series context)
        {
            context = await PopcornRepository.GetSingleSeriesAsync(context.Imdb_Id);
            for (int i = 1; i <= context.Seasons; i++)
            {
                gContent.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) } );
                var se = new Label { Text = $"Season {i}" };
                se.BackgroundColor = Color.White;
                se.FontSize = 18;
                se.Padding = 16;
                se.VerticalOptions = LayoutOptions.CenterAndExpand;

                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += (s, e) =>
                {
                    Navigation.PushAsync(new EpisodesPage(context , i));
                };
                se.GestureRecognizers.Add(tapGestureRecognizer);

                gContent.Children.Add(se, 0, i - 1);
            };
        }
    }
}