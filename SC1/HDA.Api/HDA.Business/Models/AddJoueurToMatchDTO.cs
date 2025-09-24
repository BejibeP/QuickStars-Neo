using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDA.Business.Models
{
    public class AddJoueurToMatchDTO
    {
        public string Mail { get; set; } = string.Empty;
        public int IdSoccer { get; set; }

        public string FormuleRepas { get; set; } = string.Empty;
    }
}
