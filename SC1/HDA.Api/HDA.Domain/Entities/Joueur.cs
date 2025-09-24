using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDA.Domain.Entities
{
    public class Joueur
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;

        public DateTime DateAjout { get; set; }
        public string Mail { get; set; } = string.Empty;


        public IEnumerable<JoueursMatch> JoueursMatchs { get; set; } = new List<JoueursMatch>();
    }
}