namespace WSConvertisseur.Models
{
    public class Devise
    {
        private int id;
        private string nomDevise;
        private double taux;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string NomDevise
        {
            get { return nomDevise; }
            set { nomDevise = value; }
        }

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
