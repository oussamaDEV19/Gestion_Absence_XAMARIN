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
    public class GetStatisticsPage : ContentPage
    {
        private ListView listView;
        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");
        SearchBar searchBar;
        student _student = new student();
        Label present = new Label { Padding = new Thickness(20), HorizontalOptions = LayoutOptions.Center , TextColor = Color.White , FontSize = 20};
        Label absent = new Label { Padding = new Thickness(20), HorizontalOptions = LayoutOptions.Center  , TextColor = Color.White , FontSize = 20 };


        public GetStatisticsPage()
        {
            
            this.Title = "Students Statistics";

            var db = new SQLiteConnection(_dbPath);

            StackLayout stackLayout = new StackLayout { Padding = new Thickness(20) };

            listView = new ListView();
            listView.ItemsSource = db.Table<student>().OrderBy(x => x.FirstName).ToList();
            listView.ItemSelected += listView_Item_Selected;

            searchBar = new SearchBar
            {
                Placeholder = "Search student...",
                PlaceholderColor = Color.Blue,
                TextColor = Color.Blue,
                TextTransform = TextTransform.Lowercase,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(SearchBar)),
                FontAttributes = FontAttributes.Italic
            };
            searchBar.TextChanged += OnTextChanged;

            stackLayout.Children.Add(searchBar);
            stackLayout.Children.Add(listView);

            StackLayout st = new StackLayout { BackgroundColor = Color.BlueViolet };
            st.Children.Add(present);
            st.Children.Add(absent);

            stackLayout.Children.Add(st);

            Content = stackLayout;
        }


        private void listView_Item_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            _student = (student)e.SelectedItem;
            present.Text = "Present : " + _student.Nb_Present.ToString() + " times";
            absent.Text = "Absent : "  + _student.Nb_Absent.ToString() + "times";
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            var KeyWord = searchBar.Text;
            listView.ItemsSource = db.Table<student>().OrderBy(x => x.FirstName).ToList().Where(name => name.FirstName.ToLower().Contains(KeyWord.ToLower()));

        }
    }
}