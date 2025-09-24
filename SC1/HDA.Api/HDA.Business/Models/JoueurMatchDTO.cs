using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDA.Business.Models
{
    public class JoueurMatchDTO
    {
        public int IdJoueur { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime HeureDeReponse { get; set; }
        public string FormuleRepas { get; set; } = string.Empty;

    }
}
