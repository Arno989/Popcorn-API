using Popcorn.Models;
using Popcorn.Repositories;
using PopcornTime.Model;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Popcorn.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoviesDetailsPage : ContentPage
    {
        #region Init
        Movie Context = new Movie();
        public MoviesDetailsPage(Movie context)
        {
            InitializeComponent();
            Context = context;
            ShowContent();
        }
        #endregion

        #region Show data
        private void ShowContent()
        {
            imgPoster.Source = Context.Images.Poster;
            lblTitle.Text = Context.Title;
            lblYear.Text = "Released on " + (PopcornRepository.UnixTimeStampToDateTime(Context.Release)).ToString("dddd, dd MMMM yyyy");
            lblContentRating.Text = "Content rating: " + Context.Certification;
            lblSynopsis.Text = Context.Synopsis;
            lblRuntime.Text = "Duration: " + Context.Runtime + " minutes";
            lblPercentage.Text = Context.Rates.Percentage + "%";
            int i = 0;
            foreach (string genre in Context.Genres)
            {
                var lab = new Label { Text = "- " + genre, TextColor = (Color)Application.Current.Resources["SecondaryTextColorLight"], VerticalOptions = LayoutOptions.Start };
                gGenres.Children.Add(lab, 1, i);
                i++;
            }
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

        private async Task ShowActionSheet()
        {
            MovieTorrent t = Context.Torrent;
            string s1080 = t.En.P1080 != null ? $"1080p ({PopcornRepository.GetFileSize(t.En.P1080.Size)}) - {t.En.P1080.Seed} seeders" : "";
            string s720 = t.En.P720 != null ? $"720p ({PopcornRepository.GetFileSize(t.En.P720.Size)}) - {t.En.P720.Seed} seeders" :"";
            string s480 = t.En.P480 != null ? $"480p ({PopcornRepository.GetFileSize(t.En.P480.Size)}) - {t.En.P480.Seed} seeders" : "";

            string action = await DisplayActionSheet("Download Torrent", "Cancel", null, (t.En.P1080 != null ? s1080 : "1080p (unavailable)"), (t.En.P720 != null ? s720 : "720p (unavailable)"), (t.En.P480 != null ? s480 : "480p (unavailable)"));
            
            if (action.Contains(s1080))
            {
                Device.OpenUri(new Uri(Convert.ToString(t.En.P1080.Url)));
            }
            else if(action.Contains(s720))
            {
                Device.OpenUri(new Uri(Convert.ToString(t.En.P720.Url)));
            }
            else if (action.Contains(s480))
            {
                Device.OpenUri(new Uri(Convert.ToString(t.En.P480.Url)));
            }
        }
        #endregion

        #region Event handlers
        private void btnTrailer_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri(Context.Trailer)); //Launcher.CanOpenAsync
        }

        private void btnDownload_Clicked(object sender, EventArgs e)
        {
            ShowActionSheet();
        }
        #endregion
    }
}