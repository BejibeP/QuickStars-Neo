using HDA.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDA.Business.Interfaces
{
    public interface IJoueurMatchService
    {
        /// <returns>0 si ok, -1 si échec de l'ajout, 1 si rien n'est entré, 2 si le joueur n'existe pas, 3 si le joueur est déjà inscrit</returns>
        Task<int> AddJoueurToMatch(AddJoueurToMatchDTO dto);

        Task<bool> RemoveJoueurToMatch(int idJoueur);

        Task<bool> UpdateRepasOfJoueurMatch(UpdateRepasOfJoueurMatchDTO joueurMatchDTO);
        Task<ICollection<JoueurMatchDTO>?> GetJoueursFromMatch(int id);
    }
}
