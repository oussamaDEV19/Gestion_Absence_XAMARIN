using Gestion_Absence.models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Gestion_Absence.views
{
    public class AbsencePage : ContentPage
    {
        private ListView listView;
        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");
        SearchBar searchBar;
        Picker picker = new Picker { Title = "Select a Module", TitleColor = Color.Red , HorizontalTextAlignment = TextAlignment.Center};
        private string module_selected;
        public AbsencePage()
        {
            


            
            this.Title = "Absence";

            var db = new SQLiteConnection(_dbPath);

            List<module> all_modules = db.Table<module>().ToList();

            for (int i = 0; i < all_modules.Count; i++)
            {
                picker.Items.Add(all_modules[i].Name);
            }

            picker.SelectedIndexChanged += picker_data_module;

            


            StackLayout stackLayout = new StackLayout { Padding = new Thickness(20) };
            stackLayout.Children.Add(picker);

            listView = new ListView();
            listView.ItemsSource = db.Table<student>().OrderBy(x => x.FirstName).ToList();
            searchBar = new SearchBar {
                Placeholder = "Search student...",
                PlaceholderColor = Color.Blue,
                TextColor = Color.Blue,
                TextTransform = TextTransform.Lowercase,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(SearchBar)),
                FontAttributes = FontAttributes.Italic
            };
            searchBar.TextChanged += OnTextChanged;

            listView.ItemSelected += OnAlertYesNoClicked;

            stackLayout.Children.Add(searchBar);
            stackLayout.Children.Add(listView);

            Content = stackLayout;
        }

        private void picker_data_module(object sender, EventArgs e)
        {
            this.module_selected = picker.Items[picker.SelectedIndex];
            var db = new SQLiteConnection(_dbPath);
            //listView.ItemsSource = db.Table<module>().OrderBy(x => x.std_mdl).ToList().Where(name => name.FirstName.ToLower().Contains(KeyWord.ToLower()));

            /*
            List<module> all_modules = db.Table<module>().ToList();

            for (int i = 0; i < all_modules.Count; i++)
            {
                if(all_modules[i].Name == module_selected)
                {
                    List<student> lst = all_modules[i].std_mdl.ToList();
                    
                    for (int j = 0; i < lst.Count; i++)
                    {
                        listView.ItemsSource += lst[j].FirstName;
                    }
                    
                }

            }
            */
            List<student> all_students = db.Table<student>().Where(name => name.module == module_selected).ToList();
            listView.ItemsSource = all_students;
        }

        private async void OnAlertYesNoClicked(object sender, SelectedItemChangedEventArgs e)
        {
            student stud = (student) e.SelectedItem;
            bool answer = await DisplayAlert(stud.FirstName + " " + stud.LastName, "Would you like to make this person Absent ", "Yes", "No");
            var db = new SQLiteConnection(_dbPath);
            if (answer)
            {
                
                List<student> All_student = db.Table<student>().ToList();
                for (int i = 0; i < All_student.Count; i++)
                {
                    
                    if (All_student[i].Id == stud.Id)
                    {
                        student stu = new student()
                        {
                            Id = Convert.ToInt32(All_student[i].Id),
                            FirstName = All_student[i].FirstName,
                            LastName = All_student[i].LastName,
                            Email = All_student[i].Email,
                            Num = All_student[i].Num,
                            Nb_Absent = All_student[i].Nb_Absent + 1,
                            Nb_Present = All_student[i].Nb_Present,
                            module = All_student[i].module
                        };
                        db.Update(stu);
                    }else
                    {
                        student stu = new student()
                        {
                            Id = Convert.ToInt32(All_student[i].Id),
                            FirstName = All_student[i].FirstName,
                            LastName = All_student[i].LastName,
                            Email = All_student[i].Email,
                            Num = All_student[i].Num,
                            Nb_Present = All_student[i].Nb_Present + 1,
                            Nb_Absent = All_student[i].Nb_Absent,
                            module = All_student[i].module
                        };
                        db.Update(stu);
                    }
                }
                
                await Navigation.PopAsync();
            }
            else
            {
                await Navigation.PopAsync();
            }
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            var KeyWord = searchBar.Text;
            listView.ItemsSource = db.Table<student>().OrderBy(x => x.FirstName).ToList().Where(name => name.FirstName.ToLower().Contains(KeyWord.ToLower()));

        }
    }
}