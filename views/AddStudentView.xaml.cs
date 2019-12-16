using App2.Utils;
using SQLitePCL;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace App2.Views
{
    public sealed partial class AddStudentView : Page
    {
        private SQLiteConnection con ;
        private List<ComboItem> listFilieres ;
        private List<ComboItem> listGender ;

        internal List<ComboItem> ListFilieres { get => ListFilieres; set => ListFilieres = value; }
        internal List<ComboItem> ListGender { get => ListGender; set => ListGender = value; }

        public AddStudentView()
        {
            // init props 
            con = new SQLiteConnection("database_uwp.db");
            listFilieres = new List<ComboItem>();
            listGender = new List<ComboItem>();

            // fill sexe combobox
            ListGender.Add(new ComboItem("F", "Féminin"));
            ListGender.Add(new ComboItem("M", "Masculin"));

            this.InitializeComponent();
            comboFiliere.DataContext = ListFilieres;
            comboGender.DataContext = ListGender;
        }

        private void cancelAddStudent(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(StudentsView));
        }

        private void addStudent(object sender, RoutedEventArgs e)
        {

        }

        private void loaded(object sender, RoutedEventArgs e)
        {
                        
            // fill filiere combobox
            String query = @"Select * from Filieres";
            ISQLiteStatement stmt = con.Prepare(query);
            while (stmt.Step() == SQLiteResult.ROW)
            {
                ComboItem item = new ComboItem(stmt["id_filiere"].ToString(), stmt["nom_filiere"].ToString());
                ListFilieres.Add(item);
            }
        }
    }
}
