using HDA.Business.Interfaces;
using HDA.Business.Models;
using HDA.Domain.Entities;
using HDA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDA.Business.Services
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;
        public MatchService(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }
        public async Task<IEnumerable<MatchDTO>> GetAllMatch()
        {
            var list = await _matchRepository.GetAllMatch();
            var listSortie = list.Select(item => new MatchDTO()
            {
                Id = item.Id,
                Createur = item.Createur,
                Libelle = item.Libelle,
                InscriptionsOuvertes = item.InscriptionsOuvertes,
            });
            return listSortie;
        }

        public async Task<IEnumerable<MatchDTO>> GetAllClosedMatch()
        {
            var list = await _matchRepository.GetAllClosedMatch();
            var listSortie = list.Select(item => new MatchDTO()
            {
                Id = item.Id,
                Createur = item.Createur,
                Libelle = item.Libelle,
                InscriptionsOuvertes = item.InscriptionsOuvertes,
            });
            return listSortie;
        }

        /// <summary>
        /// La création d'un match ouvert ferme l'ancien match ouvert
        /// </summary>
        public async Task<MatchDTO?> CreateMatch(CreateMatchDTO dto)
        {
            if (dto == null) return null;

            if (dto.InscriptionsOuvertes == true)
            {
                var matchOuvert = await _matchRepository.GetOpenMatch();
                if (matchOuvert != null)
                {
                    await _matchRepository.CloseMatch(matchOuvert);
                }
            };

            var match = new MatchSoccer() { Libelle = dto.Libelle, InscriptionsOuvertes = dto.InscriptionsOuvertes, Createur = dto.Createur };
            int id = await _matchRepository.CreateMatch(match);
            
            if(id == 0) return null;

            return new MatchDTO() { Libelle = dto.Libelle, InscriptionsOuvertes = dto.InscriptionsOuvertes, Createur = dto.Createur };
        }

        /// <summary>
        /// Récupère le match ouvert, l'assigne au dernier créé s'il n'existe pas
        /// </summary>
        public async Task<MatchDTO> GetOrAssignOpenMatch()
        {
            //on récupère le match ouvert
            var matchOuvert = await _matchRepository.GetOpenMatch();

            //s'il n'existe pas on prend le match avec l'id le plus élevé (dernier créé) pour l'ouvrir
            if (matchOuvert == null)
            {
                matchOuvert = await _matchRepository.GetLastMatch();
                var ouvert = await OpenMatch(matchOuvert.Id);
            };
            var matchOuvertDTO = new MatchDTO()
            {
                Libelle = matchOuvert.Libelle,
                Createur = matchOuvert.Createur,
                InscriptionsOuvertes = matchOuvert.InscriptionsOuvertes,
                Id = matchOuvert.Id,
            };
            return matchOuvertDTO;
        }

        public async Task<MatchDTO?> OpenMatch(int id)
        {
            var matchAOuvrir = await _matchRepository.GetMatchById(id);
            if (matchAOuvrir == null) return null;

            //ferme le match actuellement ouvert
            var matchOuvert = await _matchRepository.GetOpenMatch();
            if (matchOuvert != null)
            {
                await _matchRepository.CloseMatch(matchOuvert);
            }

            await _matchRepository.OpenMatch(matchAOuvrir);

            return new MatchDTO() { Libelle = matchAOuvrir.Libelle, InscriptionsOuvertes = matchAOuvrir.InscriptionsOuvertes };
        }

        public async Task<bool> DeleteMatch(int id)
        {
            return await _matchRepository.DeleteMatch(id);
        }
    }
}
