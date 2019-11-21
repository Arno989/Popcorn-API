using Xamarin.Forms;

namespace Popcorn
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTczMzg1QDMxMzcyZTMzMmUzMFh0bmhGSnMzMlExQ21uNnNzNmZKM0VVeTQwSDFXM28xUVdXWGxnQkRSbnc9");
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
