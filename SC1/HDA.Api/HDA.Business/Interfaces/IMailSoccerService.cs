using HDA.Business.Models.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDA.Business.Interfaces
{
    public interface IMailSoccerService
    {
        Task<MailToCcListsDTO> MailToAndCC();

        Task<MailToCcListsDTO> MailsMatchToAndCC();
    }
}
