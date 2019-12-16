﻿using App2.Models;
using SQLitePCL;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace App2
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class FilieresView : Page
    {
        private SQLiteConnection con = new SQLiteConnection("database_uwp.db");
        private ObservableCollection<Filiere> ListFilieres;
        public FilieresView()
        {
            this.InitializeComponent();
            ListFilieres = new ObservableCollection<Filiere>();
            tableFilieres.DataContext = ListFilieres;
        }

        private void win_loaded(object sender, RoutedEventArgs e)
        {
            String query = @"Select * from Filieres";
            ISQLiteStatement stmt = con.Prepare(query);
            ISQLiteStatement stmtForTotal;
            ISQLiteStatement stmtNbAbsence;
            while (stmt.Step() == SQLiteResult.ROW)
            {
                query = @"Select id_etudiant from Etudiants WHERE id_filiere='" + stmt["id_filiere"].ToString() + "'";
                stmtForTotal = con.Prepare(query);
                int nb_etudiant = 0;
                int nb_absence = 0;
                while (stmtForTotal.Step() == SQLiteResult.ROW)
                {
                    nb_etudiant++;
                    query = @"Select id_absence from Absences WHERE id_etudiant='" + stmtForTotal["id_etudiant"].ToString() + "'";
                    stmtNbAbsence = con.Prepare(query);
                    while (stmtNbAbsence.Step() == SQLiteResult.ROW)
                    {
                        nb_absence++;
                    }
                }
                Filiere fil = new Filiere(int.Parse(stmt["id_filiere"].ToString()),stmt["nom_filiere"].ToString(), nb_etudiant, nb_absence);
                ListFilieres.Add(fil);
            }
        }
    }
}
