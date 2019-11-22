using Popcorn.Models;
using Popcorn.Repositories;
using System;
using System.Collections.Generic;
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
            List<int> seasons = new List<int>();
            List<Episode> eps = context.Episodes;

            eps.Sort(delegate (Episode x, Episode y)
            {
                int ix = Convert.ToInt32($"10{x.SeasonNr.ToString("00")}" + $"20{x.EpisodeNr.ToString("00")}");
                int iy = Convert.ToInt32($"10{y.SeasonNr.ToString("00")}" + $"20{y.EpisodeNr.ToString("00")}");
                if (ix == 0 && iy == 0) return 0;
                else if (ix == 0) return -1;
                else if (iy == 0) return 1;
                else return ix.CompareTo(iy);
            });

            foreach (Episode e in eps)
            {
                if (!seasons.Contains(e.SeasonNr))
                {
                    seasons.Add(e.SeasonNr);
                }
            }

            seasons.Sort();
            int i = 0;
            foreach (int ie in seasons)
            {
                gContent.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) } );
                var se = new Label { Text = $"Season {ie}" };
                se.BackgroundColor = Color.White;
                se.FontSize = 18;
                se.Padding = 16;
                se.TextColor = (Color)Application.Current.Resources["SecondaryTextColorLight"];
                se.VerticalOptions = LayoutOptions.CenterAndExpand;

                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += (s, e) =>
                {
                    Navigation.PushAsync(new EpisodesPage(eps , ie, context.Images.Poster));
                };
                se.GestureRecognizers.Add(tapGestureRecognizer);

                gContent.Children.Add(se, 0, i);
                i++;
            };
        }
    }
}