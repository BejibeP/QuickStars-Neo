using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDA.Business.Models.Mail
{
    public class MailToCcDTO
    {
        public string To { get; set; } = string.Empty;
        public string Cc { get; set; } = string.Empty;
    }
}
