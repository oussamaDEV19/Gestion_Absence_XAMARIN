using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Gestion_Absence.views
{
    public class HomePage : ContentPage
    {
        public HomePage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            this.Title = "Select Option";

            StackLayout stackLayout = new StackLayout { Padding = new Thickness(20) };

            Button button = new Button { BackgroundColor = Color.DarkSalmon, TextColor = Color.White, Margin = new Thickness(0, 25, 0, 0) };
            button.Text = "Add Student";
            button.Clicked += Button_Clicked;

            stackLayout.Children.Add(button);

            Button button2 = new Button { BackgroundColor = Color.Orange, TextColor = Color.White, Margin = new Thickness(0, 25, 0, 0) };
            button2.Text = "Absence";
            button2.Clicked += Button2_Clicked;

            stackLayout.Children.Add(button2);

            Button button3 = new Button { BackgroundColor = Color.HotPink, TextColor = Color.White, Margin = new Thickness(0, 25, 0, 0) };
            button3.Text = "Edite Student";
            button3.Clicked += Button3_Clicked;

            stackLayout.Children.Add(button3);


            Button button4 = new Button { BackgroundColor = Color.YellowGreen, TextColor = Color.White, Margin = new Thickness(0, 25, 0, 0) };
            button4.Text = "Delete Student";
            button4.Clicked += Button4_Clicked;

            stackLayout.Children.Add(button4);

            Button button5 = new Button { BackgroundColor = Color.BlueViolet, TextColor = Color.White, Margin = new Thickness(0, 25, 0, 0) };
            button5.Text = "Get Statistics";
            button5.Clicked += Button5_Clicked;

            stackLayout.Children.Add(button5);

            Button button6 = new Button { BackgroundColor = Color.IndianRed, FontSize = 30 , TextColor = Color.White , Margin = new Thickness(0 , 150 , 0 , 0) , Padding = new Thickness(0 , 40 , 0 , 40)};
            button6.Text = "Lougout";
            button6.Clicked += Button6_Clicked;

            Button button7 = new Button { BackgroundColor = Color.CadetBlue, TextColor = Color.White, Margin = new Thickness(0, 25, 0, 0) };
            button7.Text = "Add New Module";
            button7.Clicked += Button7_Clicked;

            stackLayout.Children.Add(button7);
            stackLayout.Children.Add(button6);

            Content = stackLayout;
        }

        private async void Button7_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddModulePage());
        }

        private async void Button6_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        private async void Button5_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GetStatisticsPage());
        }

        private async void Button4_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DeleteStudentPage());
        }
        private async void Button3_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditStudentPage());
        }

        private async void Button2_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AbsencePage());
        }
        
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddStudentPage());
        }

        
    }
}