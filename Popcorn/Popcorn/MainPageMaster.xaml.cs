using Popcorn.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Popcorn
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageMaster : ContentPage
    {
        public ListView ListView;

        public MainPageMaster()
        {
            InitializeComponent();

            BindingContext = new MainPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class MainPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MainPageMasterMenuItem> MenuItems { get; set; }

            public MainPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<MainPageMasterMenuItem>(new[]
                {
                    new MainPageMasterMenuItem { Id = 0, Title = "Movies"},
                    new MainPageMasterMenuItem { Id = 1, Title = "Series" },
                    new MainPageMasterMenuItem { Id = 2, Title = "Anime" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }

        private void MenuItemsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            switch ((e.SelectedItem as MainPageMasterMenuItem).Title)
            {
                case "Movies":
                    ((MasterDetailPage)Parent).Detail = new NavigationPage(new MoviesPage());
                    break;
                case "Series":
                    ((MasterDetailPage)Parent).Detail = new NavigationPage(new SeriesPage());
                    break;
                case "Anime":
                    ((MasterDetailPage)Parent).Detail = new NavigationPage(new AnimePage());
                    break;
            }
        }
    }
}