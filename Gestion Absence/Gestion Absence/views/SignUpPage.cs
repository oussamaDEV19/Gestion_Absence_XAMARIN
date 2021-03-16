
using System.Collections.Generic;
using SQLite;
using System;

using System.IO;
using System.Linq;
using System.Text;
using Gestion_Absence.models;
using Xamarin.Forms;

namespace Gestion_Absence.views
{
    public class SignUpPage : ContentPage
    {
        private Entry _iDEntry;
        private Entry _Email;
        private Entry _Num;
        private Entry _FirstName;
        private Entry _LastName;
        private Entry _module;
        private Entry _Password;
        private Button _Button;
        private Button _Button2;

        Teacher _teacher = new Teacher();

        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");

        Label warning = new Label { TextColor = Color.Red, FontSize = 15 };

        public SignUpPage()
        {
            var db = new SQLiteConnection(_dbPath);
            db.CreateTable<Teacher>();
            db.CreateTable<student>();

            NavigationPage.SetHasNavigationBar(this, false);

            StackLayout st = new StackLayout { BackgroundColor = Color.LightYellow, Padding = new Thickness(20) };

            _FirstName = new Entry
            {
                BackgroundColor = Color.White,
                PlaceholderColor = Color.Violet,
                TextColor = Color.Black
            };
            _FirstName.Keyboard = Keyboard.Text;
            _FirstName.Placeholder = "First Name";
            st.Children.Add(_FirstName);

            _LastName = new Entry
            {
                BackgroundColor = Color.White,
                PlaceholderColor = Color.Violet,
                TextColor = Color.Black
            };
            _LastName.Keyboard = Keyboard.Text;
            _LastName.Placeholder = "Last Name";
            st.Children.Add(_LastName);

            _Email = new Entry
            {
                BackgroundColor = Color.White,
                PlaceholderColor = Color.Violet,
                TextColor = Color.Black
            };
            _Email.Keyboard = Keyboard.Text;
            _Email.Placeholder = "Email";
            st.Children.Add(_Email);

            _Num = new Entry
            {
                BackgroundColor = Color.White,
                PlaceholderColor = Color.Violet,
                TextColor = Color.Black
            };
            _Num.Keyboard = Keyboard.Text;
            _Num.Placeholder = "Num";
            st.Children.Add(_Num);

            _module = new Entry
            {
                BackgroundColor = Color.White,
                PlaceholderColor = Color.Violet,
                TextColor = Color.Black
            };
            _module.Keyboard = Keyboard.Text;
            _module.Placeholder = "Module";
            st.Children.Add(_module);

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
            _Button.Text = "SignUp";
            _Button.Clicked += _button_clicked;

            _Button2 = new Button { BackgroundColor = Color.BlueViolet, TextColor = Color.White };
            _Button2.Text = "LogIn";
            _Button2.Clicked += _button_clicked_2;


            st.Children.Add(_Button);
            st.Children.Add(_Button2);


            st.Children.Add(warning);
            

            Content = st;
        }

        private async void _button_clicked_2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        private async void _button_clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_FirstName.Text) && !string.IsNullOrWhiteSpace(_LastName.Text) && !string.IsNullOrWhiteSpace(_Email.Text) && !string.IsNullOrWhiteSpace(_Password.Text) && !string.IsNullOrWhiteSpace(_Num.Text) && !string.IsNullOrWhiteSpace(_module.Text))
            {
                var db = new SQLiteConnection(_dbPath);
                

                var maxPK = db.Table<Teacher>().OrderByDescending(c => c.Id).FirstOrDefault();

                Teacher teach = new Teacher()
                {
                    Id = (maxPK == null ? 1 : maxPK.Id + 1),
                    FirstName = _FirstName.Text,
                    LastName = _LastName.Text,
                    Num = _Num.Text,
                    Email = _Email.Text.Trim(),
                    Password = _Password.Text.Trim()

                };
                db.Insert(teach);

                var maxPK3 = db.Table<student>().OrderByDescending(c => c.Id).FirstOrDefault();

                
                /*
                student std = new student {
                    Email = "oussama@gmail.com",
                    FirstName = "oussama",
                    LastName = "el aaoumari",
                    Id = (maxPK == null ? 1 : maxPK3.Id + 1),
                    Nb_Absent = 0,
                    Nb_Present = 0,
                    Num = "063946765"
                };
                db.Insert(std);
                */
                db.CreateTable<module>();
                var maxPK2 = db.Table<module>().OrderByDescending(c => c.Id).FirstOrDefault();

                


                module mod = new module()
                {
                    Id = (maxPK2 == null ? 1 : maxPK2.Id + 1),
                    Name = _module.Text
                    
                };
                db.Insert(mod);
                
                await DisplayAlert("Alert", teach.FirstName + " " + teach.LastName + " Added", "Log In");
                await Navigation.PushAsync(new LoginPage());
            }
            else
            {
                warning.Text = "Please Fill All the Fields !! ";
            }
        }
    }
}