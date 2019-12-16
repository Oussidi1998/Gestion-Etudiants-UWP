using App.Utils;
using App2.Models;
using App2.Views;
using CsvHelper;
using CsvParse;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace App2
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class StudentsView : Page
    {
        private SQLiteConnection con = new SQLiteConnection("database_uwp.db");
        private ObservableCollection<Etudiant> ListStudents { get; }

        public StudentsView()
        {
            this.InitializeComponent();
            ListStudents = new ObservableCollection<Etudiant>();
            tableStudents.DataContext = ListStudents;
        }

        // this function just to check where is the location of database
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            StorageFolder localCache = ApplicationData.Current.LocalCacheFolder;
            await Launcher.LaunchFolderAsync(localCache);
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

            /*using (var reader = new StreamReader(await file.OpenStreamForReadAsync()))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.RegisterClassMap<EtudiantMap>();

                var records = csv.GetRecords<Etudiant>();
                
                foreach (Etudiant etud in records){
                    ListStudents.Add(etud);
                }
            }*/

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
        public void clearFilter(object sender, RoutedEventArgs e)
        {

        }

        private void selectedColumnList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
