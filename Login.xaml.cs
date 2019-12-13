using SQLitePCL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App2
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private SQLiteConnection con = new SQLiteConnection("database.db");
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {

            String query = @"Select * from Users where login='"+ userInput.Text + "' and password='"+ mtpInput.Password+"'";
            ISQLiteStatement stmt = con.Prepare(query);
            int nbr = 0;
            while (stmt.Step() == SQLiteResult.ROW){
                nbr++;
            }
            if (nbr <= 0){
                var messageDialog = new MessageDialog("Le nom d'utilisateur ou  mot de passe incorrect");
                await messageDialog.ShowAsync();
            }else
                 Frame.Navigate(typeof(Main));
            
        }

        private void app_load(object sender, RoutedEventArgs e)
        {

            // to create the database when it's the first time
           /* String filQuery = @"CREATE TABLE IF NOT EXISTS Filieres(id_filiere Integer Primary key AUTOINCREMENT not null,nom_filiere varchar(25))";
            String UserQuery = @"CREATE TABLE IF NOT EXISTS Users(id_user Integer Primary key AUTOINCREMENT not null,login varchar(25),password varchar(25))";
            String CourQuery = @"CREATE TABLE IF NOT EXISTS Cours(id_cour Integer Primary key AUTOINCREMENT not null,designation varchar(25))";
            String EtudQuery = @"CREATE TABLE IF NOT EXISTS Etudiants(id_etudiant  Integer Primary key AUTOINCREMENT not null,cne varchar(10) unique,cin varchar(10) unique,nom varchar(25),prenom varchar(25),adresse varchar(100),sexe char CHECK(sexe in ('F', 'M')),date_naissance date,phone varchar(15),id_filiere Integer,FOREIGN KEY (id_filiere) references Filieres(id_filiere) ON DELETE CASCADE ON UPDATE NO ACTION)";
            String AbsQuery = @"CREATE TABLE IF NOT EXISTS Absences(id_absence Integer Primary key AUTOINCREMENT not null,date_absence datetime,justifier tinyint(1),justification text,id_etudiant Integer,id_cour Integer, FOREIGN KEY (id_etudiant) REFERENCES Etudiants(id_etudiant) ON DELETE CASCADE ON UPDATE NO ACTION,FOREIGN KEY (id_cour) references Cours(id_cour) ON DELETE CASCADE ON UPDATE NO ACTION)";
            String bothQuery = @"CREATE TABLE IF NOT EXISTS Cour_Etudiant(id_etudiant Integer,id_cour Integer,PRIMARY KEY(id_etudiant,id_cour), FOREIGN KEY (id_etudiant)  references Etudiants(id_etudiant) ON DELETE CASCADE ON UPDATE NO ACTION,FOREIGN KEY (id_cour) references Cours(id_cour) ON DELETE CASCADE ON UPDATE NO ACTION)";

            ISQLiteStatement stmt = con.Prepare(filQuery);
            stmt.Step();
            ISQLiteStatement stmt2 = con.Prepare(UserQuery);
            stmt2.Step();
            ISQLiteStatement stmt3 = con.Prepare(CourQuery);
            stmt3.Step();
            ISQLiteStatement stmt4 = con.Prepare(EtudQuery);
            stmt4.Step();
            ISQLiteStatement stmt5 = con.Prepare(AbsQuery);
            stmt5.Step();
            ISQLiteStatement stmt6 = con.Prepare(bothQuery);
            stmt6.Step();*/



        }
    }
}
