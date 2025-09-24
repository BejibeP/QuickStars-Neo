using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using MsgKit.Enums;
using MsgKit;
using HDA.Business;
using HDA.Business.Interfaces;
using HDA.Business.Models.Mail;
using HDA.Domain.Identity;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using MessageImportance = MsgKit.Enums.MessageImportance;

namespace HDA.Api.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Roles = IdentityRoles.AdminName)]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailSoccerService _mailSoccerService;
        private readonly IJoueurService _joueurService;
        public MailController(IMailSoccerService mailSoccerService, IJoueurService joueurService)
        {
            _mailSoccerService=mailSoccerService;
            _joueurService=joueurService;
        }

        [HttpPost]
        public async Task<IActionResult> SendMail(MailInfoDTO dto)
        {
            ////SOLUTION 1 : pb avec office ? "Could not load file or assembly 'office, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c'. Le fichier spécifié est introuvable."
            //Microsoft.Office.Interop.Outlook.Application objOutlook = new Microsoft.Office.Interop.Outlook.Application();

            //// Creating a new Outlook message from the Outlook Application instance
            //Microsoft.Office.Interop.Outlook.MailItem msgInterop = (Microsoft.Office.Interop.Outlook.MailItem)
            //    (objOutlook.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem));

            //// Set recipient information
            //var destinataires = await _mailSoccerService.MailToAndCC();
            //msgInterop.To = destinataires.To;
            //msgInterop.CC = destinataires.Cc;

            //// Set the message subject
            //msgInterop.Subject = dto.Subject;

            //// Set some HTML text in the HTML body
            //msgInterop.HTMLBody = dto.Body;

            //// Save the MSG file in local disk
            //string strMsg = @"C:\Users\ndelepierre\soccer.msg";
            //msgInterop.SaveAs(strMsg, Microsoft.Office.Interop.Outlook.OlSaveAsType.olMSG);

            //return Ok();


            ////SOLUTION 2 : Mailkit
            //var message = new MailMessage();

            ////récupère l'ensemble des mails inscrits
            //var joueurs = await _joueurService.GetAllJoueurs();


            //foreach (var joueur in joueurs)
            //{
            //    if (joueur.Mail.Contains("@soprasteria.com"))
            //    {
            //        message.To.Add(joueur.Mail);
            //        //message.To.Add(MailboxAddress.Parse(joueur.Mail));
            //    }
            //    else
            //    {
            //        message.CC.Add(joueur.Mail);
            //        //message.Cc.Add(MailboxAddress.Parse(joueur.Mail));
            //    }
            //}

            ////message.From.Add(MailboxAddress.Parse("nais.delepierre@soprasteria.com"));
            //message.From = new MailAddress("nais.delepierre@soprasteria.com");
            //message.Subject = dto.Subject;
            //message.Body = dto.Body;

            //// Save it in local disk
            //message.WriteTo(new FileStream(@$"C:\Users\ndelepierre\{dto.FileName}.msg", FileMode.Create)) ;

            //return Ok();




            ////SOLUTION 3 : ASPOSE.EMAIL

            //// Create an instance of the Aspose.Email.MailMessage class
            //Aspose.Email.MailMessage msg = new Aspose.Email.MailMessage();

            //// Set recipients information
            //var destinataires = await _mailSoccerService.MailToAndCC();
            //msg.To = destinataires.To;
            //msg.CC = destinataires.Cc;

            //// Set the subject
            //msg.Subject = dto.Subject;

            //// Set HTML body
            //msg.HtmlBody = dto.Body;

            //// Save it in local disk
            //string strMsg = @"C:\soccer.msg";
            ////msg.Save(strMsg, Aspose.Email.SaveOptions.DefaultHtml);

            //MapiMessage outlookMsg = MapiMessage.FromMailMessage(msg);
            //outlookMsg.Save(strMsg);

            //return Ok();




            //SOLUTION 4 : MSGKIT

            //get destinataires
            var destinataires = await _mailSoccerService.MailToAndCC();

            using (var email = new Email(
                new Sender("", "HDA Planner"),
                 "Prochain match HDA", true, false)
                 )
            {
                foreach (var mail in destinataires.To)
                {
                    email.Recipients.AddTo(mail);
                };
                foreach (var mail in destinataires.Cc)
                {
                    email.Recipients.AddCc(mail);
                };
                email.Subject = dto.Subject;
                email.BodyHtml = dto.Body;
                
                //get path for download folder and save .msg file in it
                string downloadsPath = KnownFolders.GetPath(KnownFolder.Downloads);
                email.Save(@$"{downloadsPath}\{dto.FileName}.msg");

                // Show the E-mail
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = @$"{downloadsPath}\{dto.FileName}.msg",
                    UseShellExecute = true,
                });
                
                return Ok();

            }
        }

        [HttpPost("JoueursMatch")]
        public async Task<IActionResult> SendMailJoueursMatch(MailInfoDTO dto)
        {

            //get destinataires
            var destinataires = await _mailSoccerService.MailsMatchToAndCC();

            using (var email = new Email(
                new Sender("", "HDA Planner"),
                 "Prochain match HDA", true, false)
                 )
            {
                foreach (var mail in destinataires.To)
                {
                    email.Recipients.AddTo(mail);
                };
                foreach (var mail in destinataires.Cc)
                {
                    email.Recipients.AddCc(mail);
                };
                email.Subject = dto.Subject;
                email.BodyHtml = dto.Body;

                //get path for download folder and save .msg file in it
                string downloadsPath = KnownFolders.GetPath(KnownFolder.Downloads);
                email.Save(@$"{downloadsPath}\{dto.FileName}.msg");

                // Show the E-mail
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = @$"{downloadsPath}\{dto.FileName}.msg",
                    UseShellExecute = true,
                });

                return Ok();

            }
        }

        [HttpGet]
        public async Task<IActionResult> GetListMailJoueurs()
        {
            var mails = await _mailSoccerService.MailToAndCC();
            if (mails == null) { return BadRequest(); }
            var sortie = new MailToCcDTO()
            {
                To = string.Concat(mails.To),
                Cc = string.Concat(mails.Cc)
            };
            return Ok(sortie);
        }

        [HttpGet("matchOuvert")]
        public async Task<IActionResult> GetListMailMatchJoueurs()
        {
            var mails = await _mailSoccerService.MailsMatchToAndCC();
            if (mails == null) { return BadRequest(); }

            var sortie = new MailToCcDTO()
            {
                To = string.Concat(mails.To),
                Cc = string.Concat(mails.Cc)
            };
            return Ok(sortie);
        }
    }
}
