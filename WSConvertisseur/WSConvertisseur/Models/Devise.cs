using System.ComponentModel.DataAnnotations;

namespace WSConvertisseur.Models
{
    public class Devise
    {
        private int id;
        private string nomDevise;
        private double taux;

        [Required]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [Required]
        public string NomDevise
        {
            get { return nomDevise; }
            set { nomDevise = value; }
        }

        [Required]
        public double Taux
        {
            get { return taux; }
            set { taux = value; }
        }

        public Devise()
        {
               
        }

        public Devise(int id, string nomDevise, double taux)
        {
            Taux = taux;
            NomDevise = nomDevise;
            Id = id;
        }
    }
}
