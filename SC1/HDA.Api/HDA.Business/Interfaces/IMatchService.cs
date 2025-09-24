using HDA.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDA.Business.Interfaces
{
    public interface IMatchService
    {

        Task<IEnumerable<MatchDTO>> GetAllMatch();

        Task<IEnumerable<MatchDTO>> GetAllClosedMatch();
        Task<MatchDTO?> CreateMatch(CreateMatchDTO dto);

        Task<MatchDTO> GetOrAssignOpenMatch();

        Task<MatchDTO?> OpenMatch(int id);

        Task<bool> DeleteMatch(int id);

        //Task<bool> ResetMatchs();

        //Task<bool> ModifyMatch(CreateMatchDTO dto);

        //Task<MatchDTO> GetMatchById(int id);


    }
}
