using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDA.Domain.Entities
{
    public class JoueursMatch
    {
        public int Id { get; set; }
        public DateTime HeureDeReponse { get; set; }
        public string FormuleRepas { get; set; } = string.Empty;



        public int JoueurId { get; set; }
        public Joueur Joueur { get; set; } = null!;


        public int MatchSoccerId { get; set; }
        public MatchSoccer MatchSoccer { get; set; } = null!;
    }
}
