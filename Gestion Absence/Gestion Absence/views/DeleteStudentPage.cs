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
    public class DeleteStudentPage : ContentPage
    {   private ListView listView;
        private Button _Button;

        student _student = new student();

        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");

        public DeleteStudentPage()
        {
            
            this.Title = "Delete Student";

            var db = new SQLiteConnection(_dbPath);

            StackLayout stackLayout = new StackLayout { Padding = new Thickness(20) };

            listView = new ListView();
            listView.ItemsSource = db.Table<student>().OrderBy(x => x.FirstName).ToList();
            listView.ItemSelected += listView_Item_Selected;

            stackLayout.Children.Add(listView);

            _Button = new Button { BackgroundColor = Color.BlueViolet, TextColor = Color.White };
            _Button.Text = "Delete";
            _Button.Clicked += _button_clicked;

            stackLayout.Children.Add(_Button);

            Content = stackLayout;
        }

        private async void _button_clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            db.Table<student>().Delete(X => X.Id == _student.Id);
            await Navigation.PopAsync();
        }

        private void listView_Item_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            _student = (student)e.SelectedItem;
        }
    }
}