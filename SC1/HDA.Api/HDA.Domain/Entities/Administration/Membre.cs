namespace HDA.Domain.Entities
{
    public class Membre : BaseEntity
    {
        public string Prenom { get; set; } = string.Empty;
        public string Nom { get; set; } = string.Empty;
        public string Batiment { get; set; } = string.Empty;
        public string Tel { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Active { get; set; }
    }
}