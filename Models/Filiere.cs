using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.Models
{
    class Filiere
    {
        private int id_filiere;
        private string nom_filiere;
        private int nombre_des_etudiants;
        private int nombre_des_absences;

        public Filiere(int id_filiere, string nom_filiere, int nombre_Des_Etudiant, int nombre_des_absences)
        {
            Id_filiere = id_filiere;
            Nom_filiere = nom_filiere;
            Nombre_Des_Etudiants = nombre_Des_Etudiant;
            Nombre_des_absences = nombre_des_absences;
        }

        public int Id_filiere { get => id_filiere; set => id_filiere = value; }
        public string Nom_filiere { get => nom_filiere; set => nom_filiere = value; }
        public int Nombre_Des_Etudiants { get => nombre_des_etudiants; set => nombre_des_etudiants = value; }
        public int Nombre_des_absences { get => nombre_des_absences; set => nombre_des_absences = value; }
    }
}
