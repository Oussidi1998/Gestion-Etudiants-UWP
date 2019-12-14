using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.Models
{
    class Cour
    {
        private int id_cour;
        private string intitulé;
        private int nombre_des_etudiant;
        private int nombre_des_absences;

        public Cour(int id_cour, string intitulé, int nombre_des_etudiant, int nombre_des_absences)
        {
            Id_cour = id_cour;
            Intitulé = intitulé;
            Nombre_des_etudiant = nombre_des_etudiant;
            Nombre_des_absences = nombre_des_absences;
        }

        public int Id_cour { get => id_cour; set => id_cour = value; }
        public string Intitulé { get => intitulé; set => intitulé = value; }
        public int Nombre_des_etudiant { get => nombre_des_etudiant; set => nombre_des_etudiant = value; }
        public int Nombre_des_absences { get => nombre_des_absences; set => nombre_des_absences = value; }
    }
}
