using App.Utils;
using App2.Models;
using App2.Views;
using CsvHelper;
using CsvParse;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using Telerik.UI.Xaml.Controls.Grid;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
namespace App2
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class StudentsView : Page
    {
        private SQLiteConnection con = new SQLiteConnection("database_uwp.db");
        private ObservableCollection<Etudiant> ListStudents { get; set; }
        private Etudiant selectedStudent;

        public StudentsView()
        {
            this.InitializeComponent();
            ListStudents = new ObservableCollection<Etudiant>();
            tableStudents.DataContext = ListStudents;
        }


        private void win_loaded(object sender, RoutedEventArgs e)
        {
            String query = @"Select * from Etudiants";
            ISQLiteStatement stmt = con.Prepare(query);
            ISQLiteStatement stmtFiliere;
            while (stmt.Step() == SQLiteResult.ROW)
            {
                String forFiliere = @"Select nom_filiere from Filieres where id_filiere='" + stmt["id_filiere"].ToString() + "'";
                stmtFiliere = con.Prepare(forFiliere);
                stmtFiliere.Step();

                Etudiant etud = new Etudiant(stmt["cne"].ToString(),stmt["prenom"].ToString(), stmt["nom"].ToString(),  stmt["adresse"].ToString(), stmt["sexe"].ToString(), stmt["date_naissance"].ToString(), stmtFiliere["nom_filiere"].ToString(), stmt["phone"].ToString(), stmt["cin"].ToString());
                ListStudents.Add(etud);
            }

        }

        private void addStudentButton(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddStudentView));
        }
        private async void importExcel(object sender, RoutedEventArgs e)
        {
            
            var picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.List;
            picker.FileTypeFilter.Add(".csv");

            var file = await picker.PickSingleFileAsync();

            // we test in case he close win but did choose the file
            if (file != null)
            {
                using (CsvFileReader csvReader = new CsvFileReader(await file.OpenStreamForReadAsync()))
                {
                    CsvRow row = new CsvRow();
                    csvReader.ReadLine();
                    while (csvReader.ReadRow(row))
                    {
                        string cne = row[0];
                        string cin = row[1];
                        string nom = row[2];
                        string prenom = row[3];
                        string address = row[4];
                        string sexe = row[5];
                        string date_naissance = row[6];
                        string filiere = row[7];
                        string phone = row[8];

                        // check the filiere id before add it to database
                        // check if student exist before add it to database

                        Etudiant etud = new Etudiant(cne, prenom, nom, address, sexe, date_naissance, filiere, phone, cin);
                        ListStudents.Add(etud);
                    }
                }

            }

        }

        private async void exporterExcel(object sender, RoutedEventArgs e)
        {
            var picker = new FileSavePicker();
            picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            picker.FileTypeChoices.Add("CSV", new List<string>() { ".csv" });
            picker.SuggestedFileName = "List_Etudiants";

            StorageFile file = await picker.PickSaveFileAsync();

            // we test in case he close window without saving the file
            if (file != null)
            {
                using (var writer = new StreamWriter(await file.OpenStreamForWriteAsync()))
                using (var csv = new CsvWriter(writer))
                {
                    csv.Configuration.Delimiter = ",";
                    csv.Configuration.RegisterClassMap<EtudiantMap>();
                    csv.WriteRecords(ListStudents);
                }
            }
        }

        private void editStudentButton(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddStudentView),selectedStudent);
        }

        private async void deleteStudentButton(object sender, RoutedEventArgs e)
        {
            ContentDialog res = new ContentDialog
            {
                Title = "Suppression d'un etudiant !",
                Content = "Voulez-vous vraiment supprimer cette étudiant ?",
                PrimaryButtonText = "Supprimer",
                CloseButtonText = "Annuler"
            };

            ContentDialogResult result = await res.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                // delete the student
                if (ListStudents.Contains(selectedStudent))
                {
                    // delete in local table
                    ListStudents.Remove(selectedStudent);
                    // delete in db

                }
            }

            res.Hide();

        }

        private void itemSelected(object sender, DataGridSelectionChangedEventArgs e)
        {
            supprimer.Visibility = Visibility.Visible;
            modifier.Visibility = Visibility.Visible;

            // get the user selected for update and delete
            selectedStudent = (Etudiant) tableStudents.SelectedItem;
            Debug.WriteLine(selectedStudent);
        }

        private async void liveSearchData(object sender, TextChangedEventArgs e)
        {
            String searchItem = searchbox.Text;
            ObservableCollection<Etudiant> list_temp = new ObservableCollection<Etudiant>();
            
            foreach (Etudiant etud in ListStudents)
            {
                if (etud.Adress.Contains(searchItem) || etud.Cne.Contains(searchItem) || etud.Cin.Contains(searchItem) || etud.Phone.Contains(searchItem) || etud.Sexe.Contains(searchItem) || etud.Nom.Contains(searchItem) || etud.Prenom.Contains(searchItem) || etud.Date_naissance.Contains(searchItem) || etud.Filiere.Contains(searchItem))
                {
                    list_temp.Add(etud);
                    Debug.WriteLine("yes exist");
                } 
            }
            
            // still need to be fixed
            /* next :
                addStudent and ModifyStudent
                add cortana
                add Notifications
                
             */

            ListStudents.Clear();
            ListStudents = list_temp;
        }
    }
}
