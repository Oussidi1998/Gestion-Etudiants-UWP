using App2.Models;
using App2.Utils;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace App2.Views
{
    public sealed partial class AddStudentView : Page
    {
        private SQLiteConnection con ;
        private ObservableCollection<ComboItem> listFilieres ;
        private ObservableCollection<ComboItem> listGender ;
        private Etudiant etudiantForModify;

        internal ObservableCollection<ComboItem> ListFilieres { get => ListFilieres; set => ListFilieres = value; }
        internal ObservableCollection<ComboItem> ListGender { get => ListGender; set => ListGender = value; }

        public AddStudentView()
        {
            
           InitializeComponent();
           DataContext = this;
        }

        private void cancelAddStudent(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(StudentsView));
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            etudiantForModify = (Etudiant)e.Parameter;

        }

        private void addStudent(object sender, RoutedEventArgs e)
        {

        }

        private void loaded(object sender, RoutedEventArgs e)
        {
            if (etudiantForModify != null)
            {

            }
            else
            {


            }

            // init props 
            con = new SQLiteConnection("database_uwp.db");
            listFilieres = new ObservableCollection<ComboItem>();
            listGender = new ObservableCollection<ComboItem>();

            // fill sexe combobox
            listGender.Add(new ComboItem("F", "Féminin"));
            listGender.Add(new ComboItem("M", "Masculin"));


            // fill filiere combobox
            String query = @"Select * from Filieres";
            ISQLiteStatement stmt = con.Prepare(query);
            while (stmt.Step() == SQLiteResult.ROW)
            {
                ComboItem item = new ComboItem(stmt["id_filiere"].ToString(), stmt["nom_filiere"].ToString());
                listFilieres.Add(item);
            }
        }
    }
}
