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
    public class AddModulePage : ContentPage
    {
        private Entry _modueNameEntry;
        private Button _saveButton;

        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");
        public AddModulePage()
        {
            
            this.Title = "Add Module";

            StackLayout stacklayout = new StackLayout { Padding = new Thickness(20) };


            _modueNameEntry = new Entry();
            _modueNameEntry.Keyboard = Keyboard.Text;
            _modueNameEntry.Placeholder = "Module Name ";
            stacklayout.Children.Add(_modueNameEntry);

            _saveButton = new Button();
            _saveButton.Text = "Add Module";
            _saveButton.Clicked += _saveButton_Clicked;
            stacklayout.Children.Add(_saveButton);

            Content = stacklayout;
        }

        private async void _saveButton_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            

            var maxPK = db.Table<module>().OrderByDescending(c => c.Id).FirstOrDefault();

            module mod = new module()
            {
                Id = (maxPK == null ? 1 : maxPK.Id + 1),
                Name = _modueNameEntry.Text
            };
            db.Insert(mod);
            await DisplayAlert("Alert", "Module : " + mod.Name + " Added", "OK");
            await Navigation.PopAsync();
        }
    }
}