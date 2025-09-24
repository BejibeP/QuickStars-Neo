using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDA.Business.Models.Mail
{
    public class ImportJoueursDTO
    {
        public string Cc { get; set; } = string.Empty;
        public string To { get; set;} = string.Empty;

        public string From { get; set; } = string.Empty;
    }
}
