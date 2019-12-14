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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace App2
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class CoursView : Page
    {
        ObservableCollection<Cour> ListCours;
        private SQLiteConnection con = new SQLiteConnection("database_uwp.db");
        public CoursView()
        {
            this.InitializeComponent();
            ListCours = new ObservableCollection<Cour>();
        }

        private void win_loaded(object sender, RoutedEventArgs e)
        {
            String query = @"Select * from Cours";
            ISQLiteStatement stmt = con.Prepare(query);
            ISQLiteStatement stmtNbEtudiant;
            ISQLiteStatement stmtNbAbsence;
            while (stmt.Step() == SQLiteResult.ROW)
            {
                query = @"Select id_etudiant from Cour_Etudiant WHERE id_cour='" + stmt["id_cour"].ToString() + "'";
                stmtNbEtudiant = con.Prepare(query);
                int nb_etudiant = 0;
                int nb_absence = 0;
                while (stmtNbEtudiant.Step() == SQLiteResult.ROW)
                {
                    nb_etudiant++;
                    query = @"Select id_absence from Absences WHERE id_etudiant='" + stmtNbEtudiant["id_etudiant"].ToString() + "'";
                    stmtNbAbsence = con.Prepare(query);
                    while(stmtNbAbsence.Step()== SQLiteResult.ROW)
                    {
                        nb_absence++;
                    }
                }
                Cour cour = new Cour(int.Parse(stmt["id_cour"].ToString()), stmt["designation"].ToString(), nb_etudiant,nb_absence);
                ListCours.Add(cour);
            }

        }

    }
}
