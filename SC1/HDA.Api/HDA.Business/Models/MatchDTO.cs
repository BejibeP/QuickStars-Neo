using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDA.Business.Models
{
    public class MatchDTO
    {
        public int Id { get; set; }
        public string Libelle { get; set; } = string.Empty;

        public bool InscriptionsOuvertes { get; set; } = false;

        //Nom de l'admin qui a créé le match
        public string Createur { get; set; } = string.Empty;
    }
}
