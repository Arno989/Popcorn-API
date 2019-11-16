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
    public partial class EpisodesPage : ContentPage
    {
        public EpisodesPage(Series context, int season)
        {
            InitializeComponent();
        }
    }
}