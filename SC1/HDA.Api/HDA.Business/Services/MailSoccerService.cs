using HDA.Business.Interfaces;
using HDA.Business.Models.Mail;
using HDA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HDA.Business.Services
{
    public class MailSoccerService : IMailSoccerService
    {
        private readonly IJoueurRepository _joueurRepository;
        private readonly IJoueurMatchRepository _joueurMatchRepository;
        private readonly IMatchService _matchService;
        public MailSoccerService(IJoueurRepository joueurRepository, IJoueurMatchRepository joueurMatchRepository, IMatchService matchService) 
        {
            _matchService = matchService;
            _joueurRepository = joueurRepository;
            _joueurMatchRepository = joueurMatchRepository;
        }
        // récupère les adresses mail inscrites et les concatène dans un string : les joueurs avec une adresse sopra dans le TO et les autres dans le CC
        public async Task<MailToCcListsDTO> MailToAndCC()
        {
            var joueurs = await _joueurRepository.GetAllJoueurs();

            ICollection<string> joueursSopra = new List<string>();
            ICollection<string> joueursExt = new List<string>();

            foreach (var joueur in joueurs)
            {
                if (joueur.Mail.Contains("@soprasteria.com"))
                {
                    joueursSopra.Add($"{joueur.Nom} {joueur.Prenom} <{joueur.Mail}>; ");
                }
                else
                {
                    joueursExt.Add($"{joueur.Nom} {joueur.Prenom} <{joueur.Mail}>; ");
                }
            }

            return new MailToCcListsDTO { To = joueursSopra, Cc = joueursExt };
        }

        public async Task<MailToCcListsDTO> MailsMatchToAndCC()
        {
            var match = await _matchService.GetOrAssignOpenMatch();
            var joueurs = await _joueurMatchRepository.GetJoueursFromMatch(match.Id);

            ICollection<string> joueursTo = new List<string>();
            ICollection<string> joueursCc = new List<string>();

            var nbJoueurs = joueurs.Count / 10; //quotien de la division euclidienne du nombre de joueur par 10: nombre de terrains
            int nb = 1;

            foreach (var joueur in joueurs)
            {
                if (nb <= nbJoueurs*10)
                {
                    joueursTo.Add($"{joueur.Joueur.Nom} {joueur.Joueur.Prenom} <{joueur.Joueur.Mail}>; ");
                }
                else 
                {
                    joueursCc.Add($"{joueur.Joueur.Nom} {joueur.Joueur.Prenom} <{joueur.Joueur.Mail}>; ");
                }
                nb++;
            }

            return new MailToCcListsDTO { To = joueursTo, Cc = joueursCc };
        }

    }
}
