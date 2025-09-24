namespace HDA.Domain.Entities
{
    public class MembreZero : BaseEntity
    {
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string Batiment { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string Active { get; set; }
    }
}