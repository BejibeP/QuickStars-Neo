using HDA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDA.Domain.Interfaces
{
    public interface IJoueurRepository
    {
        Task<IEnumerable<Joueur>> GetAllJoueurs();
        Task<int> GetJoueurByMail(string email);

        Task<Joueur?> GetJoueurById(int id);
        Task<int> AddJoueur(Joueur joueur);
        Task<bool> UpdateJoueur(Joueur joueur);
        Task<bool> DeleteJoueur(int id);
    }
}
