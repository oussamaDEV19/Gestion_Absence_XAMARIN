using Gestion_Absence.views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gestion_Absence
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new SplashScreen());
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
