using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDA.Domain.Entities
{
    public class FormuleRepas
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public bool EstDisponible { get; set; } = true;
    }
}
