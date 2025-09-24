using HDA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDA.Domain.Interfaces
{
    public interface IMatchRepository
    {
        Task<int> CreateMatch(MatchSoccer match);
        Task<MatchSoccer?> GetOpenMatch();
        Task<MatchSoccer> GetLastMatch();
        Task<MatchSoccer?> GetMatchById(int id);
        Task<IEnumerable<MatchSoccer>> GetAllMatch();
        Task<IEnumerable<MatchSoccer>> GetAllClosedMatch();
        Task CloseMatch(MatchSoccer matchAFermer);
        Task OpenMatch(MatchSoccer matchAOuvrir);

        Task<bool> DeleteMatch(int id);

        //Task<bool> ResetMatchs();

    }
}
