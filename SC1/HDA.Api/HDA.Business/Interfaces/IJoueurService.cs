using HDA.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDA.Business.Interfaces
{
    public interface IJoueurService
    {
        Task<IEnumerable<JoueurDTO>> GetAllJoueurs();
        Task<int> GetJoueurByMail(string mail);

        /// <returns>0 si ok, 1 si le joueur existe déjà, -1 si erreur</returns>
        Task<int> AddJoueur(AddJoueurToBDDDTO dto);

        Task<bool> AddListJoueurs(string joueurs);

        /// <returns>0 ok, 1 si mail déjà utilisé, -1 si joueur n'existe pas, -2 MAJ échoué</returns>
        Task<int> UpdateJoueur(UpdateJoueurDTO dto);
        Task<bool> DeleteJoueur(int id);
    }
}
