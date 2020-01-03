using App2.Models;
using Microsoft.UI.Xaml.Controls;
using SQLitePCL;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace App2
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class CoursView : Page
    {
        private ObservableCollection<Cour> listCours;
        private SQLiteConnection con = new SQLiteConnection("database_uwp.db");
        private Cour selectedCour;

        internal ObservableCollection<Cour> ListCours
        {
            get => listCours;
            set
            {
                listCours = value;
            }
        }

        public CoursView()
        {
            InitializeComponent();
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

        private void coursSelected(object sender, Telerik.UI.Xaml.Controls.Grid.DataGridSelectionChangedEventArgs e)
        {
            selectedCour = (Cour)tableCours.SelectedItem;
            supprimerCour.Visibility = Visibility.Visible;
            modifierCour.Visibility = Visibility.Visible;
            CourLabel.Text = selectedCour.Intitulé;
        }

        private async void deleteCourAction(object sender, RoutedEventArgs e)
        {
            ContentDialog res = new ContentDialog
            {
                Title = "Suppression d'un Cour !",
                Content = "Voulez-vous vraiment supprimer ce cour ?",
                PrimaryButtonText = "Supprimer",
                CloseButtonText = "Annuler"
            };

            ContentDialogResult result = await res.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                // delete the student
                if (ListCours.Contains(selectedCour))
                {
                    // delete in db
                    string query = @"Delete from Cours WHERE id_cour='" + selectedCour.Id_cour.ToString() + "'";
                    ISQLiteStatement stmt = con.Prepare(query);
                    stmt.Step();

                    // show tooltip
                    MessageDialog dialog = new MessageDialog("Supprimé");
                    await dialog.ShowAsync();

                    // delete in local table
                    ListCours.Remove(selectedCour);
                    // clear the textbox
                    CourLabel.Text = "";
                }
            }

            res.Hide();
        }

        private void modifyCourAction(object sender, RoutedEventArgs e)
        {
            if (ListCours.Contains(selectedCour))
            {
               var item =  ListCours.Where(cour=>cour.Id_cour==selectedCour.Id_cour).Single();
               
                if(item != null)
                {
                    item.Intitulé = CourLabel.Text;
                }

            }
        }

        private async void ajouterCour(object sender, RoutedEventArgs e)
        {
            string cour = CourLabel.Text;
            string msg = "Bien Ajouté";
            // check already exist
            string query = @"SELECT * FROM Cours WHERE designation='" + cour + "'";
            ISQLiteStatement stmt = con.Prepare(query);
            if (stmt.Step() == SQLiteResult.ROW)
            {
                msg = "Cet Cour déjà existe";
            }
            else
            {
                // add it in db
                query = @"INSERT INTO Cours(designation) VALUES('"+cour+"')";
                stmt = con.Prepare(query);
                stmt.Step();

                // add it in table
                ListCours.Clear();
                win_loaded(sender,e);
            }

            // show reaction
            MessageDialog dialog = new MessageDialog(msg);
            await dialog.ShowAsync();

        }
    }
}
