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
    public class EditStudentPage : ContentPage
    {

        private ListView listView;
        private Entry _iDEntry;
        private Entry _firstNameEntry;
        private Entry _lastNameEntry;
        private Entry _Email;
        private Entry _Num;
        private Button _Button;

        student _student = new student();

        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");
            
        public EditStudentPage()
        {
            
            this.Title = "Edit Student";

            var db = new SQLiteConnection(_dbPath);

            StackLayout stackLayout = new StackLayout { Padding = new Thickness(20) };
            StackLayout st = new StackLayout { BackgroundColor = Color.LightYellow , Padding = new Thickness(20) };
            listView = new ListView { BackgroundColor = Color.AliceBlue , SeparatorColor = Color.Red };
            listView.ItemsSource = db.Table<student>().OrderBy(x => x.FirstName).ToList();
            listView.ItemSelected += listView_Item_Selected;

            stackLayout.Children.Add(listView);

            _iDEntry = new Entry();
            _iDEntry.Placeholder = "ID";
            _iDEntry.IsVisible = false;
            st.Children.Add(_iDEntry);


            _firstNameEntry = new Entry();
            _firstNameEntry.Keyboard = Keyboard.Text;
            _firstNameEntry.Placeholder = "First Name";
            st.Children.Add(_firstNameEntry);


            _lastNameEntry = new Entry();
            _lastNameEntry.Keyboard = Keyboard.Text;
            _lastNameEntry.Placeholder = "Last Name";
            st.Children.Add(_lastNameEntry);

            
            _Email = new Entry();
            _Email.Keyboard = Keyboard.Text;
            _Email.Placeholder = "Email";
            st.Children.Add(_Email);


            _Num = new Entry();
            _Num.Keyboard = Keyboard.Text;
            _Num.Placeholder = "Num";
            st.Children.Add(_Num);

            _Button = new Button { BackgroundColor = Color.BlueViolet , TextColor = Color.White};
            _Button.Text = "Update";
            _Button.Clicked += _button_clicked;

            st.Children.Add(_Button);

            stackLayout.Children.Add(st);

            Content = stackLayout;
        }

        private async void _button_clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            student stu = new student()
            {
                Id = Convert.ToInt32(_iDEntry.Text),
                FirstName = _firstNameEntry.Text,
                LastName = _lastNameEntry.Text,
                Email = _Email.Text,
                Num = _Num.Text
            };
            db.Update(stu);
            await Navigation.PopAsync();
        }

        private void listView_Item_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            _student = (student)e.SelectedItem;
            _iDEntry.Text = _student.Id.ToString();
            _firstNameEntry.Text = _student.FirstName;
            _lastNameEntry.Text = _student.LastName;
            _Num.Text = _student.Num;
            _Email.Text = _student.Email;
        }
    }
}