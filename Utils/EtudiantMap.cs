using App2.Models;
using CsvHelper.Configuration;

namespace App.Utils
{
    class EtudiantMap: ClassMap<Etudiant>
    {
        public EtudiantMap()
        {
            Map(m => m.Cne).Index(0).Name("CNE");
            Map(m => m.Cin).Index(1).Name("CIN");
            Map(m => m.Prenom).Index(2).Name("Prenom");
            Map(m => m.Nom).Index(3).Name("Nom");
            Map(m => m.Adress).Index(4).Name("Adresse");
            Map(m => m.Sexe).Index(5).Name("Sexe");
            Map(m => m.Date_naissance).Index(6).Name("Date de Naissance");
            Map(m => m.Filiere).Index(7).Name("Filiere");
            Map(m => m.Phone).Index(8).Name("Telephone");
        }
    }
}
