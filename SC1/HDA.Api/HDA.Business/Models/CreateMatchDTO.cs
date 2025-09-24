using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDA.Business.Models
{
    public class CreateMatchDTO
    {
        public string Libelle { get; set; } = string.Empty;

        public string Createur { get; set; } = string.Empty;

        public bool InscriptionsOuvertes { get; set; } = false;
    }
}
