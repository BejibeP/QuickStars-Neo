using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDA.Business.Models
{
    public class UpdateRepasOfJoueurMatchDTO
    {
        public int IdJoueur { get; set; }
        public string FormuleRepas { get; set; } = string.Empty;
    }
}
