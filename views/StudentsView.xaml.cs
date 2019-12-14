using App2.Models;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
using Windows.UI.Popups;
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
        private ObservableCollection<Etudiant> ListStudents { get;}

        public StudentsView()
        {
            this.InitializeComponent();
            ListStudents = new ObservableCollection<Etudiant>();
            tableStudents.DataContext = ListStudents;
        }

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
                String forFiliere = @"Select nom_filiere from Filieres where id_filiere='"+ stmt["id_filiere"].ToString() + "'";
                stmtFiliere = con.Prepare(forFiliere);
                stmtFiliere.Step();

                Etudiant etud = new Etudiant(stmt["cne"].ToString(), stmt["nom"].ToString(), stmt["prenom"].ToString(), stmt["adresse"].ToString(),stmt["sexe"].ToString(), stmt["date_naissance"].ToString(), stmtFiliere["nom_filiere"].ToString(), stmt["phone"].ToString(), stmt["cin"].ToString());
                ListStudents.Add(etud);
            }

        }

    }
}
