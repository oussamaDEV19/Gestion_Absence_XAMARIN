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
    public class AddStudentPage : ContentPage
    {
        private Entry _firstNameEntry;
        private Entry _lastNameEntry;
        private Entry _Email;
        private Entry _Num;
        private Button _saveButton;
        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");
        private string module_selected;

        
        Picker picker = new Picker { Title = "Select a Module", TitleColor = Color.Red };
        
        public AddStudentPage()
        {
            var db = new SQLiteConnection(_dbPath);
            
            this.Title = "Add Student";

            StackLayout stacklayout = new StackLayout { Padding = new Thickness(20) } ;

            List<module> all_modules = db.Table<module>().ToList();

            for (int i = 0; i < all_modules.Count; i++)
            {
                picker.Items.Add(all_modules[i].Name);
            }

            picker.SelectedIndexChanged += picker_data_module;

            stacklayout.Children.Add(picker);

            _firstNameEntry = new Entry();
            _firstNameEntry.Keyboard = Keyboard.Text;
            _firstNameEntry.Placeholder = "First Name ";
            stacklayout.Children.Add(_firstNameEntry);


            _lastNameEntry = new Entry();
            _lastNameEntry.Keyboard = Keyboard.Text;
            _lastNameEntry.Placeholder = "Last Name ";
            stacklayout.Children.Add(_lastNameEntry);

            _Email = new Entry();
            _Email.Keyboard = Keyboard.Text;
            _Email.Placeholder = "Email ";
            stacklayout.Children.Add(_Email);

            _Num = new Entry();
            _Num.Keyboard = Keyboard.Text;
            _Num.Placeholder = "Phone Number ";
            stacklayout.Children.Add(_Num);

            _saveButton = new Button();
            _saveButton.Text = "Add";
            _saveButton.Clicked += _saveButton_Clicked;
            stacklayout.Children.Add(_saveButton);

            Content = stacklayout;

        }

        private void picker_data_module(object sender , EventArgs e)
        {
            this.module_selected = picker.Items[picker.SelectedIndex];
        }

        private async void _saveButton_Clicked(object sender, EventArgs e)
        {

            var db = new SQLiteConnection(_dbPath);

            var maxPK = db.Table<student>().OrderByDescending(c => c.Id).FirstOrDefault();

            student stud = new student()
            {
                Id = (maxPK == null ? 1 : maxPK.Id + 1),
                Nb_Absent = 0,
                Nb_Present = 0,
                FirstName = _firstNameEntry.Text,
                LastName = _lastNameEntry.Text,
                Num = _Num.Text,
                Email = _Email.Text,
                module = module_selected
            };
            db.Insert(stud);
            /*
            List<module> module = db.Table<module>().Where(name => name.Name == module_selected).ToList();
            int id_module = module[0].Id;

            var stdd_ids = module[0].std_mdl_id.ToList();
            stdd_ids.Add(stud.Id);
            

            module stu = new module()
            {
                Id = id_module,
                Name = module_selected,
                std_mdl_id = stdd_ids
            };
            db.Update(stu);
            */
            await DisplayAlert("Alert", stud.FirstName + " " + stud.LastName + " Added" , "OK");
            await Navigation.PopAsync();
        }
    }
}