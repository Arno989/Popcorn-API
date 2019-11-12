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
    public partial class AnimePage : ContentPage
    {
        public AnimePage()
        {
            InitializeComponent();
            Init();
            pickSort.SelectedItem = "Popularity";
        }

        private void Init()
        {
            foreach (String s in PopcornRepository.ANIMESORTLIST)
            {
                pickSort.Items.Add(s);
            }
        }

        private async Task ShowContent(String sort)
        {
            lvwContent.ItemsSource = await PopcornRepository.GetAnimeAsync("", sort);
        }

        private void pickSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowContent(pickSort.SelectedItem.ToString());
        }

        
    }
}