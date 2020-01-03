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
        private ObservableCollection<String> listFilieres ;
        private ObservableCollection<String> listGender ;
        private Etudiant etudiantForModify;

        internal ObservableCollection<String> ListFilieres { get => ListFilieres; set => ListFilieres = value; }
        internal ObservableCollection<String> ListGender { get => ListGender; set => ListGender = value; }

        public AddStudentView()
        {
            
           InitializeComponent();
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
        private void modifyStudent(object sender, RoutedEventArgs e)
        {

        }

        private void loaded(object sender, RoutedEventArgs e)
        {
            if (etudiantForModify != null)
            {
                // page accessoires
                page_title.Text = "Modifier un(e) Etudiant(e)";
                buttonActions.Content = "Modifier";
                buttonActions.Click -= addStudent;
                buttonActions.Click += modifyStudent;
                // fill fields with data
                nomInput.Text = etudiantForModify.Nom;
                prenomInput.Text = etudiantForModify.Prenom;
                cneInput.Text = etudiantForModify.Cne;
                cinInput.Text = etudiantForModify.Cin;
                phoneInput.Text = etudiantForModify.Phone;
                comboGender.Text = etudiantForModify.Sexe;
                comboFiliere.Text = etudiantForModify.Filiere;
                adresseInput.Text = etudiantForModify.Adress;
                // for date of birth
            }

            // init props 
            con = new SQLiteConnection("database_uwp.db");
            listFilieres = new ObservableCollection<String>();
            listGender = new ObservableCollection<String>();

            // fill sexe combobox
            listGender.Add("F");
            listGender.Add("M");


            // fill filiere combobox
            String query = @"Select * from Filieres";
            ISQLiteStatement stmt = con.Prepare(query);
            while (stmt.Step() == SQLiteResult.ROW)
            {
                listFilieres.Add(stmt["nom_filiere"].ToString());
            }
        }
    }
}
