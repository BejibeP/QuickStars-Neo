using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDA.Business.Models.Mail
{
    public class MailToCcListsDTO
    {
        // format mail1@mail.com; mail2@mail.com; ...
        public ICollection<string> To { get; set; } = new List<string>();
        public ICollection<string> Cc { get; set; } = new List<string>();
    }
}
