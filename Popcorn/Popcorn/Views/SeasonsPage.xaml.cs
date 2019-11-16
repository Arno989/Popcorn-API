using Popcorn.Models;
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

        private void ListSeasons(Series context)
        {
            for (int i = 1; i <= context.Seasons; i++)
            {
                gSeasons.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                var se = new Label { Text = $"Season {i}" };

                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += (s, e) =>
                {
                    Navigation.PushAsync(new EpisodesPage(context, i));
                };
                se.GestureRecognizers.Add(tapGestureRecognizer);

                gSeasons.Children.Add(se, 0, i - 1);
            };
        }
    }
}