namespace HDA.Domain.Entities
{
    public class ResasZero : BaseEntity
    {
        public string DateDeb { get; set; }
        public string DateFin { get; set; }
        public string HeureDeb { get; set; }
        public string HeureFin { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Motif { get; set; }
        public string Prive { get; set; }
    }
}