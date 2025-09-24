namespace HDA.Business.Models
{
    public class ReservationDto
    {
        public long Id { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public string Motif { get; set; } = string.Empty;
        public bool Prive { get; set; }
    }
}