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
            pickSort.SelectedItem = pickSort.Items[0];
        }

        private void Init()
        {
            foreach (String s in PopcornRepository.ANIMESORTLIST)
            {
                pickSort.Items.Add(s);
            }
        }

        private async Task ShowContent(String sort, String search)
        {
            List<Anime> content = await PopcornRepository.GetAnimeAsync(search, sort);
            cvContent.ItemsSource = content;
            cvContent.CurrentItem = content[0];
        }

        private void pickSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eSearch.Text == null)
            {
                ShowContent(pickSort.SelectedItem.ToString(), "");
            }
        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            ViewCell v = sender as ViewCell;
            v.ForceUpdateSize();
        }

        private void cvContent_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e) // title, year, genres (list), num-seasons
        {
            
            lblTitle.Text = ((Anime)e.CurrentItem).Title;
            lblYear.Text = ((Anime)e.CurrentItem).Year;
            //get anime by id

            //status
            //seasons (4 seasons         >)
        }

        private void btnSearch_Clicked(object sender, EventArgs e)
        {
            ShowContent(pickSort.SelectedItem.ToString(), eSearch.Text);
        }
    }
}