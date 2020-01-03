using App2.Models;
using SQLitePCL;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Popups;
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
        private Filiere selectedFiliere;
        public FilieresView()
        {
            this.InitializeComponent();
            ListFilieres = new ObservableCollection<Filiere>();
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

        private void itemSelected(object sender, Telerik.UI.Xaml.Controls.Grid.DataGridSelectionChangedEventArgs e)
        {
            selectedFiliere = (Filiere)tableFilieres.SelectedItem;
            supprimerFiliere.Visibility = Visibility.Visible;
            modifierFiliere.Visibility = Visibility.Visible;
            filiereLabel.Text = selectedFiliere.Nom_filiere;
        }
        private async void deleteFiliereAction(object sender, RoutedEventArgs e)
        {
            ContentDialog res = new ContentDialog
            {
                Title = "Suppression d'une Filiere !",
                Content = "Voulez-vous vraiment supprimer cette filière ?",
                PrimaryButtonText = "Supprimer",
                CloseButtonText = "Annuler"
            };

            ContentDialogResult result = await res.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                // delete the student
                if (ListFilieres.Contains(selectedFiliere))
                {
                    // delete in db
                    string query = @"Delete from Filieres WHERE id_filiere='" + selectedFiliere.Id_filiere.ToString() + "'";
                    ISQLiteStatement stmt = con.Prepare(query);
                    stmt.Step();

                    // show tooltip
                    MessageDialog dialog = new MessageDialog("Supprimée");
                    await dialog.ShowAsync();

                    // delete in local table
                    ListFilieres.Remove(selectedFiliere);
                    // clear the textbox
                    filiereLabel.Text = "";
                }
            }

            res.Hide();
        }

        private void modifyFiliereAction(object sender, RoutedEventArgs e)
        {
            // update in database

            // real time update in table
            if (ListFilieres.Contains(selectedFiliere))
            {
                var item = ListFilieres.Where(fil => fil.Id_filiere == selectedFiliere.Id_filiere).Single();

                if (item != null)
                {
                    item.Nom_filiere = filiereLabel.Text;
                }

            }
        }

        private async void ajouterFiliere(object sender, RoutedEventArgs e)
        {
            string filiere = filiereLabel.Text;
            string msg = "Bien Ajoutée";
            // check already exist
            string query = @"SELECT * FROM Filieres WHERE nom_filiere='" + filiere + "'";
            ISQLiteStatement stmt = con.Prepare(query);
            if (stmt.Step() == SQLiteResult.ROW)
            {
                msg = "Cette Filiere déjà existe";
            }
            else
            {
                // add it in db
                query = @"INSERT INTO Filieres(nom_filiere) VALUES('" + filiere + "')";
                stmt = con.Prepare(query);
                stmt.Step();

                // add it in table
                ListFilieres.Clear();
                win_loaded(sender, e);
            }

            // show reaction
            MessageDialog dialog = new MessageDialog(msg);
            await dialog.ShowAsync();

        }
    }
}
