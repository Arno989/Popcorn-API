using Popcorn.Models;
using Popcorn.Repositories;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Popcorn.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeriesDetailsPage : ContentPage
    {
        #region Init
        Series Context = new Series();
        public SeriesDetailsPage(Series context)
        {
            InitializeComponent();
            Context = context;
            ShowContent();
        }
        #endregion

        #region Show data
        private async Task ShowContent()
        {
            Context = await PopcornRepository.GetSingleSeriesAsync(Context.Imdb_Id);
            imgPoster.Source = Context.Images.Poster;
            lblTitle.Text = Context.Title;
            lblYear.Text = "Released in " + Context.Year;
            lblNetwork.Text = "Network: " + Context.Network;
            lblSynopsis.Text = Context.Synopsis;
            lblRuntime.Text = "Avg duration: " + Context.Runtime + " minutes";
            lblPercentage.Text = Context.Rates.Percentage + "%";
            lblStatus.Text = PopcornRepository.FirstCharToUpper(Context.Status);
            switch (Context.Status)
            {
                case "returning series":
                    lblAirtime.Text = "Plays on " + Context.AirDay + " at " + Context.AirTime + " in " + Context.Country.ToUpper();
                    break;
                case "ended":
                    lblAirtime.Text = "Played on " + Context.AirDay + " at " + Context.AirTime + " in " + Context.Country.ToUpper();
                    break;
                default:
                    break;
            }
            lblSeasons.Text = Context.Seasons + Context.Seasons > 1 ? "Seasons" : "Season";
            int i = 0;
            foreach (string genre in Context.Genres)
            {
                var lab = new Label { Text = "- " + genre, TextColor = (Color)Application.Current.Resources["SecondaryTextColorLight"], VerticalOptions = LayoutOptions.Start };
                gGenres.Children.Add(lab, 1, i);
                i++;
            }

            btnSeasons.Text = (Context.Seasons > 1 ? "View all available seasons" : "View available episodes");
            ShowRatingGraph();
        }

        private void ShowRatingGraph()
        {
            List<Model> data = new List<Model>
            {
                new Model() { Rating = "Loved", Score = Context.Rates.Percentage },
                new Model() { Rating = "Hated", Score = 100 - Context.Rates.Percentage }
            };

            DoughnutSeries doughnutSeries = new DoughnutSeries()
            {
                ItemsSource = data,
                XBindingPath = "Rating",
                YBindingPath = "Score",
                ColorModel = new ChartColorModel()
                {
                    Palette = ChartColorPalette.Custom,
                    CustomBrushes = new ChartColorCollection() { Color.FromHex("#0F9D58"), Color.FromHex("#D23F31") }
                },
                DoughnutCoefficient = 0.75,
                EnableAnimation = true,
                AnimationDuration = 0.75,
            };
            chRating.Series.Add(doughnutSeries);
        }
        #endregion

        #region Event handlers
        private void btnSeasons_Clicked(object sender, EventArgs e)
        {
            if (Context.Seasons > 1)
                Navigation.PushAsync(new SeasonsPage(Context));
            else
                Navigation.PushAsync(new EpisodesPage(Context.Episodes, 1, Context.Images.Poster));
        }
        #endregion
    }
}