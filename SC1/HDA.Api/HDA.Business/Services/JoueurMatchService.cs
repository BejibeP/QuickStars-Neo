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
    public class JoueurMatchService : IJoueurMatchService
    {
        private readonly IJoueurMatchRepository _repository;
        private readonly IJoueurRepository _joueurRepository;
        private readonly IMatchService _matchService;
        public JoueurMatchService(IJoueurMatchRepository joueurMatchRepository, IJoueurRepository joueurRepository, IMatchService matchService)
        {
            _repository = joueurMatchRepository;
            _joueurRepository = joueurRepository;
            _matchService = matchService;
        }


        /// <returns>0 si ok, -1 si échec de l'ajout, 1 si le joueur n'existe pas, 2 si le joueur est déjà inscrit</returns>
        public async Task<int> AddJoueurToMatch(AddJoueurToMatchDTO dto)
        {
            var id = await _joueurRepository.GetJoueurByMail(dto.Mail);
            if (id == 0) return 1;

            //vérifier que le joueur n'est pas déjà inscrit
            var list = await _repository.GetJoueursFromMatch(dto.IdSoccer);
            foreach (var item in list)
            {
                if (item.JoueurId == id) return 2;
            }
            if (string.IsNullOrEmpty(dto.FormuleRepas)) { dto.FormuleRepas = "Sans Formule"; }
            var joueur = new JoueursMatch() 
            { 
                JoueurId = id, 
                MatchSoccerId = dto.IdSoccer, 
                HeureDeReponse = DateTime.Now, 
                FormuleRepas = dto.FormuleRepas,
            };

            var sortie = await _repository.AddJoueurToMatch(joueur);
            if (sortie == 0) return -1;
            return 0;
        }
        public async Task<bool> RemoveJoueurToMatch(int idJoueur)
        {
            var match = await _matchService.GetOrAssignOpenMatch();
            var b = await _repository.RemoveJoueurToMatch(match.Id, idJoueur);
            return b;
        }
        public async Task<ICollection<JoueurMatchDTO>?> GetJoueursFromMatch(int id)
        {
            var list = await _repository.GetJoueursFromMatch(id);
            if (list == null) return null;
            var listdto = new List<JoueurMatchDTO>();
            foreach (var item in list)
            {
                var dto = new JoueurMatchDTO()
                {
                    IdJoueur = item.JoueurId,
                    Nom = item.Joueur.Nom,
                    Prenom = item.Joueur.Prenom,
                    Email = item.Joueur.Mail,
                    FormuleRepas = item.FormuleRepas,
                    HeureDeReponse = item.HeureDeReponse,
                };
                listdto.Add(dto);
            }
            return listdto;
        }

        public async Task<bool> UpdateRepasOfJoueurMatch(UpdateRepasOfJoueurMatchDTO joueurMatchDTO)
        {
            if (joueurMatchDTO.IdJoueur <= 0 || joueurMatchDTO.FormuleRepas == null) { return false; }
            var match = await _matchService.GetOrAssignOpenMatch();
            var joueurMatch = new JoueursMatch()
            {
                Id = joueurMatchDTO.IdJoueur,
                MatchSoccerId = match.Id,
                FormuleRepas = joueurMatchDTO.FormuleRepas
            };

            var b = await _repository.UpdateRepasOfJoueurMatch(joueurMatch);
            return b;
        }

    }
}
