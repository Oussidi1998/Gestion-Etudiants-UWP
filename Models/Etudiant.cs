using SQLitePCL;
namespace App2.Models
{
    public class Etudiant
    {
        private string cne;
        private string cin;
        private string nom;
        private string prenom;
        private string adress;
        private string sexe;
        private string date_naissance;
        private string phone;
        private string filiere;

        public Etudiant(string cne, string prenom, string nom, string adress, string sexe, string date_naissance, string filiere, string phone, string cin)
        {
            Cne = cne;
            Prenom = prenom;
            Nom = nom;
            Adress = adress;
            Sexe = sexe;
            Date_naissance = date_naissance;
            Filiere = filiere;
            Phone = phone;
            Cin = cin;
        }


        public string Cne { get => cne; set => cne = value; }
        public string Cin { get => cin; set => cin = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Filiere { get => filiere; set => filiere = value; }
        public string Sexe { get => sexe; set => sexe = value; }
        public string Adress { get => adress; set => adress = value; }
        public string Date_naissance { get => date_naissance; set => date_naissance = value; }
        public string Phone { get => phone; set => phone = value; }

        public override string ToString()
        {
            return Cne + " -- " + Nom;
        }
    }
}