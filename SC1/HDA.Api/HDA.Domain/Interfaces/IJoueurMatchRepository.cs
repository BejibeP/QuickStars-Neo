using HDA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDA.Domain.Interfaces
{
    public interface IJoueurMatchRepository
    {
        Task<int> AddJoueurToMatch(JoueursMatch joueurMatch);

        Task<bool> RemoveJoueurToMatch(int idMatch, int idJoueur);
        Task<bool> UpdateRepasOfJoueurMatch(JoueursMatch joueurMatch);

        Task<ICollection<JoueursMatch>> GetJoueursFromMatch(int id);
    }
}
