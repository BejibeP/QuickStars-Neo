using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDA.Business.Models
{
    public class JoueurDTO
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;

        public DateTime DateAjout { get; set; }
        public string Mail { get; set; } = string.Empty;
    }
}
