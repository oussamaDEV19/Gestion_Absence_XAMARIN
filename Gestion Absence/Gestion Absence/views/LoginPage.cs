using Gestion_Absence.models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Gestion_Absence.views
{
    public class LoginPage : ContentPage
    {
        private Entry _iDEntry;
        private Entry _Email;
        private Entry _Password;
        private Button _Button;
        private Button _Button2;

        Teacher _teacher = new Teacher();

        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");

        Label warning = new Label { TextColor = Color.Red, FontSize = 15 };

        public LoginPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            StackLayout st = new StackLayout { BackgroundColor = Color.LightYellow, Padding = new Thickness(20) };

            _Email = new Entry
            {
                BackgroundColor = Color.White,
                PlaceholderColor = Color.Violet,
                TextColor = Color.Black
            };
            _Email.Keyboard = Keyboard.Text;
            _Email.Placeholder = "Email";
            st.Children.Add(_Email);

            _Password = new Entry
            {
                BackgroundColor = Color.White,
                PlaceholderColor = Color.Violet,
                TextColor = Color.Black
            };
            _Password.Keyboard = Keyboard.Text;
            _Password.Placeholder = "Password";
            st.Children.Add(_Password);

            _Button = new Button { BackgroundColor = Color.BlueViolet, TextColor = Color.White };
            _Button.Text = "LogIn";
            _Button.Clicked += _button_clicked;


            _Button2 = new Button { BackgroundColor = Color.BlueViolet, TextColor = Color.White };
            _Button2.Text = "SignUp";
            _Button2.Clicked += _button2_clicked;


            st.Children.Add(_Button);

            st.Children.Add(_Button2);

            st.Children.Add(warning);


            Content = st;
        }

        private async void _button2_clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }

        private async void _button_clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_Email.Text) && !string.IsNullOrWhiteSpace(_Password.Text))
            {
                if (check_user(_Email.Text , _Password.Text))
                {
                    await Navigation.PushAsync(new HomePage());
                }
                else
                {
                    warning.Text = "Error : check your email or password !! ";
                }
                
            }
            else
            {
                warning.Text = "Please Fill All the Fields !! ";
            }
        }

        public  Boolean check_user(string email , string password)
        {
            var db = new SQLiteConnection(_dbPath);

            List<Teacher> All_teacher = db.Table<Teacher>().ToList();
            for (int i = 0; i < All_teacher.Count; i++)
            {

                if (All_teacher[i].Email == email.Trim() && All_teacher[i].Password == password.Trim())
                {
                    return true;
                }
            }
            return false;
        }
    }
}