using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Gestion_Absence.views
{
    public class SplashScreen : ContentPage
    {
        public SplashScreen()
        {
            NavigationPage.SetHasNavigationBar(this , false);
            StackLayout s = new StackLayout
            {
                BackgroundColor = Color.FromHex("5ce1e6"),
            };

            Image im = new Image
            {
                Source = "Logo.png",
            };

            s.Children.Add(im);

            this.Content = s;

            OnAppearing();

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(5000);
            await this.Navigation.PushAsync(new SignUpPage());
        }
    }
}